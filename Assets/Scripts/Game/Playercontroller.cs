using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playercontroller : MonoBehaviour {

    //character speed
    public float moveSpeed;
    //public float jumpForce;

    //2D physics effect
    // Use this for initialization
    private Rigidbody2D myRigidbody;
    private Animator myAnimator;

	void Start () {
        //search the player
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        myRigidbody.velocity = new Vector2(moveSpeed, myRigidbody.velocity.y); //vector means point like(x,y)
        myAnimator.SetFloat("Speed", myRigidbody.velocity.x);
	}
}
