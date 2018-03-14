using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RacerLogic;

public class CommandMenuManger : MonoBehaviour {

    [SerializeField]
    private Button[] commandButtons = new Button[3];
    public GameObject player;
    private bool onCooldown = false;

    // Use this for initialization
    void Start () {
        //PlayerCommands cmds = PlayerCommands.Instance;
        for (int i = 0; i < commandButtons.Length; i++)
        {
            commandButtons[i].GetComponent<Image>().sprite = Resources.Load<Sprite>(PlayerRacer.Instance.commands[i].sprite);
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
            Invoke("PostCooldown", 3f);
            // hook to player instance
            // invalidate all button inputs?
            //StartCooldown(index);   //test
        }
        else
        {
            //no stamina exception
        }
    }

    void PostCooldown()
    {

    }
}
