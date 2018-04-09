using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterNSeconds : MonoBehaviour {

    public float n;
    private float timer;

	// Use this for initialization
	void Start () {
        timer = n;
	}
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            Destroy(this.gameObject);
        }
	}
}
