using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Race.UI.TrapMenu;

namespace Race.UI.TrapMenu
{

    public class TrapButton : MonoBehaviour
    {

        private bool cooldown = false;
        private int selectedTrap = -1;
        private Image buttonImg;
        private Image cdImg;

        void Awake()
        {
            buttonImg = this.GetComponent<Image>();
            cdImg = GetComponentInChildren<Image>();
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            cdImg.fillAmount -= Time.deltaTime;
        }

        void OnMouseDown()
        {
            if (cooldown == false)
            {
                //if (money >= Trap.cost)
                {
                    buttonImg.color = Color.gray;
                    cdImg.fillAmount = 1f;
                    Invoke("ResetCooldown", 5.0f);
                    cooldown = true;
                }
                //else insufficient funds error
            }
        }

        void ResetCooldown()
        {
            cooldown = false;
            //trigger hightlight animation here
        }
    }
}