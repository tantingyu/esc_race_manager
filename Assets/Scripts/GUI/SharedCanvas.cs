using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SharedCanvas : MonoBehaviour {

    GameObject[] cautionImg = new GameObject[3];
    private int currentCautionLane = 0;
    private TrapSpawner behindTTrapSpawner;

    // Use this for initialization
    void Start () {
        for (int i = 0; i < cautionImg.Length; i++)
        {
            cautionImg[i] = transform.GetChild(i).gameObject;
            cautionImg[i].SetActive(false);
        }

        //BehindTrapSpawner will inform player if something cute spawns
        behindTTrapSpawner = GameObject.Find("TrapSpawnManager/BehindTrapSpawner").GetComponent<TrapSpawner>();
        behindTTrapSpawner.AddObserver(this);
    }
	
    public void OnCaution(int lane)
    {
        //To change if multilane 
        currentCautionLane = lane;
        cautionImg[lane].SetActive(true);
        Invoke("OffCaution", 3f);
    }

    void OffCaution()
    {
        cautionImg[currentCautionLane].SetActive(false);
    }
}
