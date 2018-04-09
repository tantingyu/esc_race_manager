using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableTarget : MonoBehaviour {

    float maxHp;
    float currentHp;

    public virtual void setMaxHp(float hp)
    {
        this.maxHp = hp;
    }

    public virtual void setCurrentHp(float hp)
    {
        this.currentHp = hp;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //
    public virtual void SomeFunction() { Debug.Log("Item.SomeFunction"); }
}
