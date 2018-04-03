using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealScript : MonoBehaviour
{
    private Rigidbody2D myRigidbody;
    [HideInInspector]
    public float lifeBackRate = 10;
    private float duration = 5;

    //private RacerController playerController;
    //public GameObject player;
    // Use this for initialization
    void Start()
    {
        //myRigidbody.velocity = new Vector2(speed, myRigidbody.velocity.y);
        Destroy(gameObject, duration);
    }

    // Update is called once per frame
    void Update()
    {
    }
/*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //trigger animation state
        if (collision.gameObject.tag == "Player")
        {
            
            //anim.SetTrigger("collide");
            //Destroy(gameObject, anim.GetCurrentAnimatorStateInfo(0).length + delay);

        }
    }
    */
}
