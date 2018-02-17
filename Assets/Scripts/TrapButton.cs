using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Race.UI.TrapMenu;

namespace Race.UI.TrapMenu
{

    public class TrapButton : MonoBehaviour
    {

        public bool cooldown { get; private set; }
        private float cooldownTime = 3f;
        private Image buttonImg;
        private GameObject child;
        private Image cdImg;

        void Awake()
        {
            cooldown = false;
            buttonImg = this.GetComponent<Image>();
            child = this.transform.GetChild(0).gameObject;
            cdImg = child.GetComponent<Image>();
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (cooldown)
            {
                Debug.Log("cdTimer:" + cdImg.fillAmount);
                cdImg.fillAmount -= Time.deltaTime / cooldownTime;
            }
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
            Debug.Log("cdImg:" + cdImg.fillAmount);
            Invoke("ResetCooldown", cooldownTime);
            cooldown = true;
        }

        public void ResetCooldown()
        {
            buttonImg.color = Color.white;
            cooldown = false;
            cdImg.fillAmount = 0f;
            //trigger ready animation here
        }
    }
}