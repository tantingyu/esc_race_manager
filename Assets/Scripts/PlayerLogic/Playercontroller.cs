using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playercontroller : MonoBehaviour
{

    //character speed
    //public float moveSpeed;
    public float Hp;
    //public float jumpForce;

    //2D physics effect
    //Use this for initialization
    private Rigidbody2D myRigidbody;
    private Animator myAnimator;

    void Start()
    {
        //search the player
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //myRigidbody.velocity = new Vector2(moveSpeed, myRigidbody.velocity.y); //vector means point like(x,y)
        myAnimator.SetFloat("Speed", myRigidbody.velocity.x);

        if (Hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)  //The first time the game obj touches a trigger
    {
        Debug.Log("Trigger Detected");
        Hp = Hp - 100;
    }

    /*
    private void OnTriggerStay2D(Collider2D collision)  //When you are in the frame, the trap is triggerred
    {
        Debug.Log("You Enterred the trap! Trigger Detected");
    }

    private void OnTriggerExit2D(Collider2D collision) //When you leave the frame, the trap is triggerred
    {
        Debug.Log("You left the trap! Trigger Detected");
    }
    */
}
