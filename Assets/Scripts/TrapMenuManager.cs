using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TrapLogic;

namespace Race.UI.TrapMenu
{
    public class TrapMenuManager : MonoBehaviour
    {
        public Button[] trapButtons = new Button[3];
        [HideInInspector]
        public static int trapCursor = -1;

        // Use this for initialization
        void Start()
        {
            PlayerTraps pt = PlayerTraps.Instance;
            for (int i = 0; i < trapButtons.Length; i++)
            {
                //trapButtons[i].GetComponent < Image >= PlayerTraps.Instance.active[i].sprite;
                int capturedIterator = i;
                trapButtons[i].onClick.AddListener(() => SetSelection(capturedIterator));
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void SetSelection(int index)
        {
            Debug.Log("Index: "+index);
            if (trapCursor != index)
            {
                trapCursor = index;
                StartCooldown(index);   //test
            }
            else
            {
                trapCursor = -1;   //deselect
                
            }
        }

        public void StartCooldown(int index)
        {
            trapButtons[index].GetComponent<TrapButton>().StartCooldown();
        }

        public void ResetCooldown(int index)
        {
            trapButtons[index].GetComponent<TrapButton>().ResetCooldown();
        }
    }
}
