using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTrap : MonoBehaviour {

    public float damage = 20;
    public int movePosition = 0;
    public float speed = -7.4f;
    public bool ground = true;

    private Rigidbody2D myRigidbody;
    private Animator anim;
    private Renderer rend;
    private float delay = 0.4f;
    private bool seen = false;
    //private RacerController playerController;
    //public GameObject player;
    // Use this for initialization
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myRigidbody.velocity = new Vector2(speed, myRigidbody.velocity.y);
        anim = GetComponent<Animator>();
        rend = GetComponent<Renderer>();
        //playerController = player.GetComponent<RacerController>();
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

        if (!collision.gameObject.GetComponent<RacerController>().offGround)
        {
            anim.SetTrigger("collide");
            Destroy(gameObject, anim.GetCurrentAnimatorStateInfo(0).length + delay);
        }
    }
}
