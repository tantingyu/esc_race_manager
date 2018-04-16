using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SharedCanvas : MonoBehaviour {

    GameObject[] cautionImg = new GameObject[3];
    GameObject deathPanel;

    private int currentCautionLane = 0;
    private TrapSpawner behindTTrapSpawner;

    bool playersLoaded = false;
    bool gameOver = false;
    int numPlayersAlive;

    float scoreTimer = 0;
    Text scoreDisplay;
    Text levelDisplay;

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

        scoreDisplay = transform.GetChild(3).gameObject.GetComponent<Text>();
        levelDisplay = transform.GetChild(4).gameObject.GetComponent<Text>();
        deathPanel = transform.GetChild(5).gameObject;
        deathPanel.SetActive(false);
    }

    void Update()
    {
        numPlayersAlive = GameObject.FindGameObjectsWithTag("Player").Length;
        if (!playersLoaded)
        {
            if (numPlayersAlive == 0)
            {
                return;
            }
            else
            {
                playersLoaded = true;
            }
        }

        if (!gameOver)
        {
            scoreTimer += Time.deltaTime;
            scoreDisplay.text = string.Format("Score: {0:f0}", scoreTimer * 100);
        }
        
        if (numPlayersAlive == 0)
        {
            Text highScore = deathPanel.transform.GetChild(1).gameObject.GetComponent<Text>();
            highScore.text = string.Format("{0:f0}", scoreTimer * 100);
            deathPanel.SetActive(true);
            gameOver = true;
        }
    }

    public void OnCaution(int lane)
    {
        //To change if multilane 
        currentCautionLane = lane;
        cautionImg[lane].SetActive(true);
        Invoke("OffCaution", 3f);
    }

    public void OnGameOver()
    {
        deathPanel.SetActive(true);
    }

    void OffCaution()
    {
        cautionImg[currentCautionLane].SetActive(false);
    }
}
