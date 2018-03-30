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

    public float hp;
    public float st;

    public Racer playerRacer;
    public RacerController playerController;

    public GameObject HUD;
    public GameObject instanceHUD;

    public Image hpBar;
    public Image stBar;

    [SerializeField]
    private Button[] commandButtons = new Button[3];

    private bool onCooldown = false;
    public bool commandExecuted;


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
            //playerController.SetCurrentPos(playerNumber, 0);

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
            //lose screen
            Destroy(gameObject);
        }
    }

    private void OnClick(int index)
    {
        Debug.Log("Command initiated: " + index);
        float stCost = playerRacer.commands[index].stCost;

        if (st >= stCost)   //stamina check
        {
            //Invoke("PostCooldown", 3f);
            Invoke("PostCooldown", playerRacer.commands[index].commandLength);
            playerController.CmdRunCmd(index);  // hook to player instance
            SetButtonsEnabled(false);   // invalidate all button inputs

            st -= stCost;
            stBar.fillAmount -= stCost / maxHp;
        }
        else
        {
            //no stamina exception
        }
    }

    void PostCooldown()
    {
        SetButtonsEnabled(true);

        //set parameters back to initial after command excuted
        playerController.commandExecuted = false;
        playerController.moveSpeed = playerController.defaultSpeed;
        playerController.offGround = false;
        playerController.changePosition = false;

    }

    //Updated by player when collision occurs
    public void OnHpChange(float change)
    {
        if (change < 0)
        {
            //damage ui animation
            //damage ui sfx
        }
        else
        {
            //heal sfx
        }
        hpBar.fillAmount += change / maxHp;
        Debug.Log("Hp changed: " + hp);
    }

    //Updated to player when command is clicked
    public void OnStaminaChange()
    {

    }

    void SetButtonsEnabled(bool enable)
    {
        foreach (Button button in commandButtons)
            button.interactable = enable;
    }
}
