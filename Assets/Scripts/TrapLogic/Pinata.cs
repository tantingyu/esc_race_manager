using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Pinata : NetworkBehaviour {

    public AudioClip pinataMusic;
    [SerializeField]
    private float hp = 100f;
    private float playerDamage = 50;
    [SerializeField]
    private float moveSpeed = 2f;
    [SerializeField]
    private Vector3 initMovePos = new Vector3();
    private int state = 0;

    [SerializeField]
    private GameObject projectile;
    private Animator anim;
    [SerializeField]
    private float projectileTime=1f;
    private float p_timer;

	// Use this for initialization
	void Start () {
        p_timer = projectileTime;
        //GameObject player = GetComponent<SetupLocalPlayer>();
        anim = GetComponent<Animator>();
        SoundManager.instance.musicSource.PlayOneShot(pinataMusic);
    }
	
	// Update is called once per frame
	void Update () {

        if (hp <= playerDamage)
        {
            anim.SetTrigger("IsDead");
            Destroy(gameObject, anim.GetCurrentAnimatorStateInfo(0).length + 1.3f);
            //Destroy(gameObject);
            Debug.Log("The pinata is killed by our hero!");
        }

        p_timer -= Time.deltaTime;
        
        switch (state) {
            case 0:
                if (transform.position != initMovePos)
                {
                    float step = moveSpeed * Time.deltaTime;
                    transform.position = Vector3.MoveTowards(transform.position, initMovePos, step);
                }
                else
                {
                    //step
                    state = 1;
                }
                break;
            case 1:
                if (p_timer < 0) {
                    //get angle from x,y
                    CmdSpawnProjectile(projectile);
                    p_timer = projectileTime;
                }
                break;
        }
	}

    private void OnTriggerEnter2D(Collider2D collisionObj)
    {
        Debug.Log("collision with pinata");
        if (collisionObj.gameObject.tag == "Player")
        {

            if (collisionObj.gameObject.GetComponent<SetupLocalPlayer>().playerController.attack)
            {
                
                hp -= playerDamage;
                SoundManager.instance.musicSource.Stop();
                Debug.Log("Pinata HP: " + hp);
            }
        }
    }

    [Command]
    public void CmdSpawnProjectile(GameObject projectile)
    {
        Quaternion rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
        GameObject p_spawn = Instantiate(projectile, transform.position, rotation);
        NetworkServer.Spawn(p_spawn);
    }
}
