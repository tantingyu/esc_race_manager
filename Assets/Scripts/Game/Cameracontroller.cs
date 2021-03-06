﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public RacerController player;

    //know where the position of the player is, store it in a vector
    private Vector3 lastPlayerPosition;
    private float distanceToMove;

	// Use this for initialization
	void Start () {
        player = FindObjectOfType<RacerController>();
        lastPlayerPosition = player.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        if (player!=null) {
            distanceToMove = player.transform.position.x - lastPlayerPosition.x;

            //let the camera move together with the player, doesn't need to change the y and z value
            transform.position = new Vector3(transform.position.x + distanceToMove, transform.position.y, transform.position.z);

            lastPlayerPosition = player.transform.position;
        }
	}
}
