using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Pinata : NetworkBehaviour {

    [SerializeField]
    private float moveSpeed = 2f;
    private Vector3 initMovePos = new Vector3();
    private int state = 0;

    [SerializeField]
    private GameObject projectile;
    [SerializeField]
    private float projectileTime=1f;
    private float p_timer;

	// Use this for initialization
	void Start () {
        p_timer = projectileTime;
	}
	
	// Update is called once per frame
	void Update () {
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

    [Command]
    public void CmdSpawnProjectile(GameObject projectile)
    {
        Quaternion rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
        GameObject p_spawn = Instantiate(projectile, transform.position, rotation);
        NetworkServer.Spawn(p_spawn);
    }
}
