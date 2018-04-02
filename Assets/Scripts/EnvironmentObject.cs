using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentObject : MonoBehaviour {

    private bool seen = false;
    private float backgroundSpeed = -5f;
    private Renderer rend;

    // Use this for initialization
    void Start () {
        GetComponent<Rigidbody2D>().velocity=new Vector2(backgroundSpeed,0);
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        if (rend.isVisible)
            seen = true;
        if (seen && !rend.isVisible)
            Destroy(gameObject);
    }
}
