using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SheildScript : MonoBehaviour
{

    //private Rigidbody2D myRigidbody;
    private float shieldDurability = 100;
    public AudioClip sheildSound;

    //private RacerController playerController;
    //public GameObject player;
    // Use this for initialization
    void Start()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.PlayOneShot(sheildSound);
    }

    // Update is called once per frame
    void Update()
    {
        if (shieldDurability <= 0) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //trigger animation state

        if (collision.gameObject.tag == "trap")
        {
            
            //anim.SetTrigger("collide");
            //Destroy(gameObject, anim.GetCurrentAnimatorStateInfo(0).length + delay);
            float damage = collision.gameObject.GetComponent<BaseTrap>().damage;
            shieldDurability -= damage;
            Debug.Log("AT field Strength: "+shieldDurability);
        }
    }
}
