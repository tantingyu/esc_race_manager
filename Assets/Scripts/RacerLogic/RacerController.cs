using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RacerLogic.RacerAssets;

public class RacerController : MonoBehaviour
{
    //TODO: set from PlayerRacer/spawner
    //character speed
    //public float moveSpeed;
    public float maxHp=150;
    public float maxSt=150;
    public float hp;
    public float st;
    public CommandMenuManger menuManger;
    //public float jumpForce;

    //2D physics effect
    private Rigidbody2D myRigidbody;
    private Animator myAnimator;
    private bool commandExecuted;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0)
        {
            //lose screen
            Destroy(gameObject);
        }
        else
        {
            //myRigidbody.velocity = new Vector2(moveSpeed, myRigidbody.velocity.y); //vector means point like(x,y)
            myAnimator.SetFloat("Speed", myRigidbody.velocity.x);
            if (commandExecuted)
            {
                gameObject.GetComponent<SpriteRenderer>().color=Color.red;
                //invalidated swipe here
                //gameObject.GetComponent<Animator>().Play("commandHere");
                //after animation
                commandExecuted = false;
            }
        }
    }

    public void runCommand(Command command)
    {
        commandExecuted = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)  //The first time the game obj touches a trigger
    {
        //check trap type here
        Debug.Log("Trigger Detected");
        hp -= 100;
        menuManger.OnHpChange(-100);
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
