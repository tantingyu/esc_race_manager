using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentObject : MonoBehaviour {

    private float backgroundSpeed = -5f;

    // Use this for initialization
    void Start () {
        GetComponent<Rigidbody2D>().velocity=new Vector2(backgroundSpeed,0);
	}
}
