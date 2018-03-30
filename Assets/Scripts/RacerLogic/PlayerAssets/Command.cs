using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RacerLogic.RacerAssets
{
    public class Command
    {
        internal Command
                        (
                            string name,
                            string sprite,
                            //string description,
                            string objectCreate,
                            float stCost,
                            float changeSpeed,
                            float commandLength,
                            bool offGround,
                            int[] changePosition
                            //AbilityEffect[] abilityEffect
                        )
        {
            this.name = name;
            this.sprite = sprite;
            //this.description = description;
            //this.abilityEffect = abilityEffect;
            this.objectCreate = objectCreate;
            this.stCost = stCost;
            this.changeSpeed = changeSpeed;
            this.commandLength = commandLength;
            this.offGround = offGround;
            this.changePosition = changePosition;
        }

        public string name { get; private set; }
        public string sprite { get; private set; }
        public string objectCreate { get; private set; }
        //public string description { get; private set; }
        public float stCost { get; private set; }
        public float changeSpeed { get; private set; }
        public float commandLength { get; private set; }
        public bool offGround { get; private set; }
        public int[] changePosition { get; private set; }
        //private readonly AbilityEffect[] abilityEffect;
    }
}