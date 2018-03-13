﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommandButton : MonoBehaviour {

    public bool cooldown { get; private set; }
    private float cooldownTime = 3f;    //TODO: send cd time from manager
    private Image buttonImg;
    private GameObject child;
    private Image cdImg;

    void Awake()
    {
        cooldown = false;
        buttonImg = this.GetComponent<Image>();
        child = this.transform.GetChild(0).gameObject;
        cdImg = child.GetComponent<Image>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartCooldown()
    {
        buttonImg.color = Color.gray;
        cdImg.fillAmount = 1f;
        Debug.Log("cdImg:" + cdImg.fillAmount);
        Invoke("ResetCooldown", cooldownTime);
        cooldown = true;
    }

    public void ResetCooldown()
    {
        buttonImg.color = Color.white;
        cooldown = false;
        cdImg.fillAmount = 0f;
        //trigger ready animation here
    }
}
