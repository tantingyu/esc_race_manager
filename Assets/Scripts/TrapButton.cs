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
        private float cooldownTime = 10f;
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
            cdImg.fillAmount -= Time.deltaTime/cooldownTime;
        }

        //void OnMouseDown()
        //{
        //    if (cooldown == false)
        //    {
        //        //if notSelected
        //        //if (money >= Trap.cost)
        //        {
        //            StartCooldown();
        //        }
        //        //else insufficient funds error
        //    }
        //}

        public void StartCooldown()
        {
            buttonImg.color = Color.gray;
            cdImg.fillAmount = 1f;
            Invoke("ResetCooldown", 5.0f);
            cooldown = true;
        }

        public void ResetCooldown()
        {
            cooldown = false;
            cdImg.fillAmount = 0f;
            //trigger hightlight animation here
        }
    }
}