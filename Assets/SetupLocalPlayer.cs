using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using RacerLogic.RacerAssets;

public class SetupLocalPlayer : NetworkBehaviour
{
    static Racer[] Racers = new Racer[] { RacerDatabase.p1, RacerDatabase.p2, RacerDatabase.p3 };

    [SyncVar]
    public string playerName;
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

    public float stRegenTimer;

    public Racer playerRacer;
    public RacerController playerController;

    public GameObject HUD;
    public GameObject instanceHUD;

    public Image hpBar;
    public Image stBar;

    [SerializeField]
    private Button[] commandButtons = new Button[3];


    void Start()
    {
        playerRacer = Racers[playerNumber];
        maxHp = playerRacer.hp;
        maxSt = playerRacer.st;
        hp = maxHp;
        st = maxSt;

        if (isLocalPlayer)
        {
            instanceHUD = Instantiate(HUD, transform.position, Quaternion.identity);

            playerController = GetComponent<RacerController>();
            playerController.enabled = true;
            playerController.SetCurrentPos(playerNumber, 0);

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
        }
    }

    void Update()
    {
        if (hp <= 0)
        {
            Destroy(gameObject);
        }

        stRegenTimer -= Time.deltaTime;
        if (stRegenTimer <= 0)
        {
            OnReplenish(2);
            stRegenTimer = 3;
        }

        // update hp and st bars
        if (isLocalPlayer)
        {
            OnChangedHp(hp);
            OnChangedSt(st);
        }
    }

    private void OnClick(int index)
    {
        // Debug.Log("Command initiated: " + index);
        float stCost = playerRacer.commands[index].stCost;
        // stamina check
        if (st >= stCost) 
        {
            playerController.CmdRunCmd(index);

            // activate cooldown timer
            float cooldown = playerRacer.commands[index].commandLength;
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

    void OnChangedHp(float hp)
    {
        hpBar.fillAmount = hp / maxHp;
    }

    void OnChangedSt(float hp)
    {
        stBar.fillAmount = st / maxSt;
    }

    void PostCooldown()
    {
        SetButtonsEnabled(true);
        // set parameters back to initial after command excuted
        playerController.commandExecuted = false;
        playerController.moveSpeed = playerController.defaultSpeed;
        playerController.offGround = false;
        playerController.changePosition = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "trap")
        {
            float damage = collision.gameObject.GetComponent<BaseTrap>().damage;
            OnDamage(damage);
        }
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

    void SetButtonsEnabled(bool enable)
    {
        foreach (Button button in commandButtons)
            button.interactable = enable;
    }
}
