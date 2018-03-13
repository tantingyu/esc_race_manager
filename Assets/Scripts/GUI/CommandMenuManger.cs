using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommandMenuManger : MonoBehaviour {

    [SerializeField]
    private Button[] commandButtons = new Button[3];
    
    // Use this for initialization
    void Start () {
        //PlayerCommands cmds = PlayerCommands.Instance;
        for (int i = 0; i < commandButtons.Length; i++)
        {
            //commandButtons[i].GetComponent<Image>().sprite = Resources.Load<Sprite>(PlayerCommands.Instance.active[i].sprite);
            int capturedIterator = i;
            commandButtons[i].onClick.AddListener(() => OnClick(capturedIterator));
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnClick(int index)
    {
        Debug.Log("Index: " + index);
        if (true)   //change to stamina check
        {
            // invalidate all button inputs?
            StartCooldown(index);   //test
        }
        else
        {
            //no stamina
        }
    }

    public bool CheckCooldown(int index)
    {
        return commandButtons[index].GetComponent<CommandButton>().cooldown;
    }

    public void StartCooldown(int index)
    {
        commandButtons[index].GetComponent<CommandButton>().StartCooldown();
    }

    public void ResetCooldown(int index)
    {
        commandButtons[index].GetComponent<CommandButton>().ResetCooldown();
    }
}
