using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDestroy : MonoBehaviour {
    public float laserTTL;
    private float timer;
    private Animator anim;
    private float delay = 0.3f;
    public AudioClip laserSound;

    // Use this for initialization
    void Start () {
        //timer = laserTTL;
        anim = GetComponent<Animator>();
        SoundManager.instance.PlayLoop(laserSound);
	}
	
	// Update is called once per frame
	void Update () {
        laserTTL -= Time.deltaTime;
		if (laserTTL<0)
        {
            SoundManager.instance.loopSource.Stop();
            anim.SetTrigger("EndLaser");
            Destroy(gameObject, anim.GetCurrentAnimatorStateInfo(0).length + delay);
        }
	}
}
