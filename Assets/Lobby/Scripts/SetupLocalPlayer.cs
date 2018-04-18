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

    public Racer playerRacer;
    public RacerController playerController;
    public RacerSpawner playerSpawner;

    public GameObject HUD;
    public GameObject instanceHUD;
    public SpriteRenderer indicator;

    public Image hpBar;
    public Image stBar;

    public AudioClip horseNeigh;
    public AudioClip jumpSound;
    public AudioClip deathSound;

    [SerializeField]
    private Button[] commandButtons = new Button[3];
    private Animator anim;
    private LineRenderer linerend;
    private bool dead = false;

    void Start()
    {
        Debug.Log("Player Number: " + playerNumber);
        // for local testing only
        if (playerNumber == 0 || playerNumber == 1)
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

            indicator = Instantiate(transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>(), 
                    new Vector2(transform.position.x + 1.0f, transform.position.y + 1.5f), Quaternion.identity);
            indicator.transform.parent = gameObject.transform;
            indicator.color = playerColor;
        }

        anim = GetComponent<Animator>();
        linerend = GetComponent<LineRenderer>();
        linerend.enabled = false;

        Renderer[] rends = GetComponentsInChildren<Renderer>();
        foreach (Renderer r in rends)
            r.material.color = playerColor;
    }

    void Update()
    {
        if (hp <= 0)
        {
            TriggerDeath();
        }

        stRegenTimer -= Time.deltaTime;
        if (stRegenTimer <= 0)
        {
            if (st < maxSt)
                OnReplenish(1);
            stRegenTimer = 0.5f;
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
        playerController.attack = false;
        linerend.enabled = false;
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
                if (playerController.attack)
                {
                    damage /= 2;
                }
                OnDamage(damage);
            }
        }

        if (collision.tag == "prop")
        {
            if (hp < maxHp && playerController.attack)
                OnHeal(30);
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
                    OnHeal(5);
                hpRegenTimer = 0.5f;
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

    void TriggerDeath()
    {
        if (!dead)
        {
            dead = true;
            anim.SetTrigger("Death");
            AudioSource audio = GetComponent<AudioSource>();
            audio.PlayOneShot(deathSound);
            Destroy(gameObject, anim.GetCurrentAnimatorStateInfo(0).length + 1.3f);
            // SceneManager.LoadScene(0);
        }
    }

    public void OnDamage(float damage)
    {
        // damage ui animation
        anim.SetTrigger("Damaged");
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
        AudioSource audio1 = GetComponent<AudioSource>();
        audio1.PlayOneShot(horseNeigh);
        Command command = playerRacer.commands[idx];
        anim.SetTrigger(command.name);

        if (command.name == "Dash")
        {
            linerend.enabled = true;
            playerController.attack = true;
        }

        if (command.objectCreate != "")
        {
            if (command.name == "Shield")
                playerSpawner.CmdSpawnShield();
            if (command.name == "Heal")
                playerSpawner.CmdSpawnHeal();
        }

        float changeSpeed = command.changeSpeed;
        if (changeSpeed != 0)
        {
            playerController.moveSpeed = changeSpeed;
            AudioSource audio2 = GetComponent<AudioSource>();
            audio2.PlayOneShot(jumpSound);
        }
            

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
}