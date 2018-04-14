using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public float rotationSpeed;
    public float speed;
    
    private Transform target;

    private Rigidbody2D rigidBody2D;
    private Animator anim;
    private Renderer rend;
    private bool seen = false;
    private float delay = 0.4f;
    public AudioClip missileLaunchSound;
    public AudioClip missileExplosionSound;

    // Use this for initialization
    void Start () {
        rigidBody2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        rend = GetComponent<Renderer>();

        SoundManager.instance.PlaySingle(missileLaunchSound);
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Player");
        if (gos.Length > 0)
        {
            int rand = Random.Range(0, gos.Length);
            target = gos[rand].GetComponent<Transform>();
        }
        else Destroy(this.gameObject);
    }
	
	// Update is called once per frame
	void Update ()
    {

        if (target == null) explode();
        else
        {
            if (rend.isVisible)
                seen = true;
            if (seen && !rend.isVisible)
                Destroy(gameObject);
            
            //rotate towards target
            Vector3 vectorToTarget = target.position - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * rotationSpeed);

            //move towards target
            Vector3 dir = transform.rotation * Vector2.right;
            float step = speed * Time.deltaTime;
            transform.position += dir * step;
        }
    }

    public static Vector2 RadianToVector2(float radian)
    {
        return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
    }

    public static Vector2 DegreeToVector2(float degree)
    {
        return RadianToVector2(degree * Mathf.Deg2Rad);
    }

    void explode()
    {
        SoundManager.instance.PlaySingle(missileExplosionSound);
        anim.SetTrigger("collide");
        Destroy(gameObject, anim.GetCurrentAnimatorStateInfo(0).length + delay);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        explode();
    }

    
}
