using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RacerLogic.RacerAssets;
using RacerLogic;

//TODO: merge with PlayerRacer or create player from it depending on networking
public class RacerController : MonoBehaviour
{
    public float maxHp=150;
    public float maxSt=150;
    public float hp;
    public float st;
    public CommandMenuManger menuManger;

    //2D physics effect
    private Racer playerRacer;
    //private Rigidbody2D myRigidbody;
    private bool commandExecuted;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
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
            //anim.SetFloat("Speed", myRigidbody.velocity.x);   no more speed for this game
            if (commandExecuted)
            {
                //invalidated swipe here
                //after animation
                commandExecuted = false;
            }
        }
    }

    public void runCommand(int commandIndex)
    {
        commandExecuted = true;
        //anim.SetTrigger(animHash[commandIndex]);
        anim.SetTrigger("Jump");

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
