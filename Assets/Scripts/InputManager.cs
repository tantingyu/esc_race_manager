using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Race.UI.TrapMenu;

namespace Race.UI.PlayerInput
{
    public class InputManager : MonoBehaviour
    {

        [SerializeField]
        private TrapMenuManager trapMenuManager;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            //check for input selection
            //if mouse click on tile
            if (trapMenuManager.trapCursor == -1)
            {
                //nothing selected from trap menu
            }
            else
            {
                //check tile set
            }
        }
    }
}