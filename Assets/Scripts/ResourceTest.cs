using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
        gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Commands/Jump");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
