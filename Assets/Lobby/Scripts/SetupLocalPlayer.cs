using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using RacerLogic.RacerAssets;

public class SetupLocalPlayer : NetworkBehaviour
{
    static Racer[] Racers = new Racer[] { RacerDatabase.p1, RacerDatabase.p2, RacerDatabase.p3 };

    [SyncVar]
    public string playerName;
    [SyncVar]
    public Color playerColor;
    [SyncVar]
    public int racerIdx;
    [SyncVar]
    public int playerNumber;

    public float maxHp;
    public float maxSt;
    // [SyncVar(hook = "OnChangedHp")]
    public float hp;
    // [SyncVar(hook = "OnChangedSt")]
    public float st;

    public float hpRegenTimer = 0.3f;
    public float stRegenTimer = 0.3f;
    public float laserDmgTimer = 0.1f;

    // public float scoreTimer = 0;
    // public Text scoreDisplay;
    // public Text levelDisplay;

    public Racer playerRacer;
    public RacerController playerController;
    public RacerSpawner playerSpawner;

    public GameObject HUD;
    public GameObject instanceHUD;

    public Image hpBar;
    public Image stBar;

    [SerializeField]
    private Button[] commandButtons = new Button[3];
    private GameObject[] cautionImg = new GameObject[3];
    private int currentCautionLane = 0;
    private TrapSpawner behindTTrapSpawner;
    private Animator anim;

    void Start()
    {
        Debug.Log("Player Number: " + playerNumber);
        // for local testing only
        if (playerNumber == 0)
        {
            playerNumber = 2;
            playerColor = Color.white;
        }

        playerRacer = Racers[playerNumber - 1];
        maxHp = playerRacer.hp;
        maxSt = playerRacer.st;
        hp = maxHp;
        st = maxSt;

        if (isLocalPlayer)
        {
            // enable local player controller
            playerController = GetComponent<RacerController>();
            playerController.enabled = true;
            playerController.SetCurrentPos(playerNumber, 0);

            // enable local player spawner
            playerSpawner = GetComponent<RacerSpawner>();
            playerSpawner.enabled = true;

            // instantiate HUD and configure elements
            instanceHUD = Instantiate(HUD, transform.position, Quaternion.identity);

            for (int i = 0; i < commandButtons.Length; i++)
            {
                commandButtons[i] = instanceHUD.transform.GetChild(i + 4).gameObject.GetComponent<Button>();
                commandButtons[i].GetComponent<Image>().sprite = Resources.Load<Sprite>(playerRacer.commands[i].sprite);
                int capturedIterator = i;
                commandButtons[i].onClick.AddListener(() => OnClick(capturedIterator));
            }

            hpBar = instanceHUD.transform.GetChild(2).gameObject.GetComponent<Image>();
            stBar = instanceHUD.transform.GetChild(3).gameObject.GetComponent<Image>();
            hpBar.fillAmount = 1;
            stBar.fillAmount = 1;

            // scoreDisplay = instanceHUD.transform.GetChild(9).gameObject.GetComponent<Text>();
            // levelDisplay = instanceHUD.transform.GetChild(10).gameObject.GetComponent<Text>();
            
            for (int i = 0; i < cautionImg.Length; i++)
            {
                cautionImg[i] = instanceHUD.transform.GetChild(i + 12).gameObject;
                cautionImg[i].SetActive(false);
            }

        }

        anim = GetComponent<Animator>();
        Renderer[] rends = GetComponentsInChildren<Renderer>();
        foreach (Renderer r in rends)
            r.material.color = playerColor;
    }

    void Update()
    {
        if (hp <= 0)
        {
            Destroy(gameObject);
            // SceneManager.LoadScene(0);
        }
       
        stRegenTimer -= Time.deltaTime;
        if (stRegenTimer <= 0)
        {
            if (st < maxSt)
                OnReplenish(1);
            stRegenTimer = 0.3f;
        }

        // scoreTimer += Time.deltaTime;
        // update score and level displays, hp and st bars
        if (isLocalPlayer)
        {
            // scoreDisplay.text = string.Format("Score: {0:f0}", scoreTimer * 100);
            OnChangedHp(hp);
            OnChangedSt(st);
        }
    }

    private void OnClick(int idx)
    {
        float stCost = playerRacer.commands[idx].stCost;
        // stamina check
        if (st >= stCost)
        {
            playerController.commandExecuted = true;
            RunCommand(idx);

            // activate cooldown timer
            float cooldown = playerRacer.commands[idx].commandLength;
            Invoke("PostCooldown", cooldown);

            // disable buttons and deduct stamina
            SetButtonsEnabled(false);
            OnCommand(stCost);
        }
        else
        {
            Debug.Log("Insufficient stamina!");
            // no stamina exception
        }
    }

    void PostCooldown()
    {
        // set parameters to initial after command execution
        playerController.commandExecuted = false;
        playerController.moveSpeed = playerController.defaultSpeed;
        playerController.offGround = false;
        playerController.changePosition = false;
        SetButtonsEnabled(true);
    }

    void SetButtonsEnabled(bool enable)
    {
        foreach (Button button in commandButtons)
            button.interactable = enable;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "trap")
        {
            if (!playerController.offGround)
            {
                float damage = collision.gameObject.GetComponent<BaseTrap>().damage;
                OnDamage(damage);
            }
        }
        if (collision.tag == "Player")
            playerController.playerCollision = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "heal")
        {
            hpRegenTimer -= Time.deltaTime;
            if (hpRegenTimer <= 0)
            {
                if (hp < maxHp)
                    OnHeal(2);
                hpRegenTimer = 0.3f;
            }
        }
        else if (collision.tag == "laser")
        {
            laserDmgTimer -= Time.deltaTime;
            if (laserDmgTimer < 0)
            {
                hp -= 2;
            }
        }
    }

    void OnChangedHp(float hp)
    {
        hpBar.fillAmount = hp / maxHp;
    }

    void OnChangedSt(float hp)
    {
        stBar.fillAmount = st / maxSt;
    }

    public void OnDamage(float damage)
    {
        // damage ui animation
        // damage ui sfx
        hp -= damage;
    }

    public void OnHeal(float heal)
    {
        // heal ui sfx
        hp += heal;
    }

    public void OnCommand(float cost)
    {
        st -= cost;
    }

    public void OnReplenish(float amount)
    {
        st += amount;
    }

    void RunCommand(int idx)
    {
        Command command = playerRacer.commands[idx];
        anim.SetTrigger(command.name);

        if (command.objectCreate != "")
        {
            if (command.name == "Shield")
                playerSpawner.CmdSpawnShield();
            if (command.name == "Heal")
                playerSpawner.CmdSpawnHeal();
        }

        float changeSpeed = command.changeSpeed;
        if (changeSpeed != 0)
            playerController.moveSpeed = changeSpeed;

        bool offGround = command.offGround;
        if (offGround)
            playerController.offGround = true;

        int moveVertical = command.changePosition[0];
        int moveHorizontal = command.changePosition[1];

        if (moveHorizontal != 0 || moveVertical != 0)
        {
            int tempLane = playerController.currentLane + moveVertical;
            int tempBlock = playerController.currentBlock + moveHorizontal;

            bool isValid = tempLane >= 0 && tempLane <= 2
                && tempBlock >= 0 && tempBlock <= 11
                && !playerController.playerCollision;
            if (isValid)
            {
                playerController.targetLane = tempLane;
                playerController.targetBlock = tempBlock;
                playerController.changePosition = true;
            }
        }
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