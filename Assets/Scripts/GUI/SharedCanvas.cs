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
    public AudioClip cautionSound;

    float delayDeathScreen = 3.0f;

    // declare delegate
    delegate void SetActiveMethod(int lane);
    // initialize list of concrete methods
    List<SetActiveMethod> setActive = new List<SetActiveMethod>();
    
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

        // add instances of concrete methods
        setActive.Add(new SetActiveMethod(OnCaution));
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
        else if (delayDeathScreen <= 0)
        {
            deathPanel.SetActive(true);
        }
        else
        {
            delayDeathScreen -= Time.deltaTime;
        }
        
        if (numPlayersAlive == 0)
        {
            Text highScore = deathPanel.transform.GetChild(1).gameObject.GetComponent<Text>();
            highScore.text = string.Format("{0:f0}", scoreTimer * 100);
            gameOver = true;
        }
    }

    public void UpdateObserver(int eventType, int lane)
    {
        // call concrete method by list index (event type)
        Debug.Log("Observer has received notification.");
        setActive[eventType](lane);
    }


    public void OnCaution(int lane)
    {
        //To change if multilane 
        Debug.Log("OnCaution called.");
        currentCautionLane = lane;
        cautionImg[lane].SetActive(true);
        SoundManager.instance.PlayLoop(cautionSound);
        Invoke("OffCaution", 3f);
    }

    public void OnGameOver()
    {
        deathPanel.SetActive(true);
    }

    void OffCaution()
    {
        cautionImg[currentCautionLane].SetActive(false);
        SoundManager.instance.loopSource.Stop();
    }
}
