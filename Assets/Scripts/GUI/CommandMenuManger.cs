using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RacerLogic;

public class CommandMenuManger : MonoBehaviour {

    [SerializeField]
    private Button[] commandButtons = new Button[3];
    public GameObject player;
    public Image hpBar;
    public Image stBar;

    private bool onCooldown = false;
    private RacerController playerController;

    // Use this for initialization
    void Start () {
        //PlayerCommands cmds = PlayerCommands.Instance;
        playerController = player.GetComponent<RacerController>();
        for (int i = 0; i < commandButtons.Length; i++)
        {
            commandButtons[i].GetComponent<Image>().sprite = Resources.Load<Sprite>(PlayerRacer.Instance.commands[i].sprite);
            int capturedIterator = i;
            commandButtons[i].onClick.AddListener(() => OnClick(capturedIterator));
        }
    }

    private void OnClick(int index)
    {
        Debug.Log("Command initiated: " + index);
        float stCost = PlayerRacer.Instance.commands[index].stCost;

        if (playerController.st >= stCost)   //stamina check
        {
            Invoke("PostCooldown", 3f);
            playerController.runCommand(index);  // hook to player instance
            SetButtonsEnabled(false);   // invalidate all button inputs
            
            playerController.st -= stCost;
            stBar.fillAmount -= stCost / playerController.maxHp;
        }
        else
        {
            //no stamina exception
        }
    }

    void PostCooldown()
    {
        SetButtonsEnabled(true);
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
        hpBar.fillAmount += change/playerController.maxHp;
    }

    //Updated to player when command is clicked
    public void OnStaminaChange()
    {
        
    }

    void SetButtonsEnabled(bool enable)
    {
        foreach (Button button in commandButtons)
            button.interactable=enable;
    }
}
