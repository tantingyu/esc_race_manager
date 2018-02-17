using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TrapLogic;
using TrapLogic.TrapAssets;

namespace Race.UI.TrapMenu
{
    public class TrapMenuManager : MonoBehaviour
    {
        [SerializeField]
        private Button[] trapButtons = new Button[3];
        //private List<Button> trapButtons;


        [HideInInspector]
        public int trapCursor = -1;

        [SerializeField]
        [Tooltip("Insert Money display GUI game object here")]
        private Text moneyGUI;
        [Tooltip("Sets countdown for gold increment in seconds.")]
        public float goldTimer = 5f;
        private float counter = 0;
        public float startGold = 1000f;
        [SerializeField]
        private float goldIncrement = 1f;



        //private static TrapMenuManager _instance;
        //internal static TrapMenuManager instance {
        //    get
        //    {
        //        if (_instance == null)
        //            throw new System.NullReferenceException("Trap Menu Manager is not initialized");
        //        return _instance;
        //    }

        //    private set
        //    {
        //        if (_instance != null)
        //            throw new System.InvalidOperationException("Trap Menu Manager already exists!");
        //        _instance = value;
        //    }
        //}

        //Singleton for unity gameObject
        private void Awake()
        {
            //TrapMenuManager.instance = this;
            //for(int i=0; i < PlayerTraps.Instance.active.Length; i++)
            //{
            //    Trap playerTrap = PlayerTraps.Instance.active[i];
            //}
        }

        void Start()
        {
            PlayerTraps pt = PlayerTraps.Instance;
            for (int i = 0; i < trapButtons.Length; i++)
            {
                trapButtons[i].GetComponent<Image>().sprite = Resources.Load<Sprite>(PlayerTraps.Instance.active[i].sprite);
                int capturedIterator = i;
                trapButtons[i].onClick.AddListener(() => SetSelection(capturedIterator));
            }
        }

        // Update is called once per frame
        void Update()
        {
            counter += Time.deltaTime;
            if (counter >= goldTimer)
            {
                startGold += goldIncrement;
                moneyGUI.text = string.Format("Funds:\n{0}",startGold);
                counter = 0;
            }
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

        public bool CheckCooldown(int index)
        {
            return trapButtons[index].GetComponent<TrapButton>().cooldown;
        }

        public void StartCooldown(int index)
        {
            trapButtons[index].GetComponent<TrapButton>().StartCooldown();
        }

        public void ResetCooldown(int index)
        {
            trapButtons[index].GetComponent<TrapButton>().ResetCooldown();
        }

        public void setTrap(Vector2 position)
        {

        }
    }
}
