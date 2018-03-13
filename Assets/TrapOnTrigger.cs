using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapOnTrigger : MonoBehaviour
{

    private Rigidbody2D myRigidbody;
    private Animator myAnimator;
    // Use this for initialization
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
