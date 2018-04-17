using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop : MonoBehaviour {

    public float damage = 20;
    public int movePosition = 0;
    public Vector2 velocity = new Vector2(-5.3f, 0);
    public bool ground = true;

    private Animator anim;
    private Renderer rend;
    private bool seen = false;
    private float delay = 0.3f;

    // Use this for initialization
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = velocity;
        anim = GetComponent<Animator>();
        rend = GetComponent<Renderer>();
        // playerController = player.GetComponent<RacerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rend.isVisible)
            seen = true;
        if (seen && !rend.isVisible)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //trigger animation state

        if (collision.tag == "shield")
        {
            //SoundManager.instance.PlaySingle(baseTrapSound);
            anim.SetTrigger("collide");
            Destroy(gameObject, anim.GetCurrentAnimatorStateInfo(0).length + delay);
        }


        if (collision.tag == "Player")
        {
            if (collision.gameObject.GetComponent<RacerController>().attack)
            {
                //SoundManager.instance.PlaySingle(baseTrapSound);
                //anim.SetTrigger("Death");
                Destroy(gameObject);//, anim.GetCurrentAnimatorStateInfo(0).length + delay);
                //Network.Destroy(gameObject);
            }
        }
    }
}
