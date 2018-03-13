using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerLogic.PlayerAssets
{
    public class Player
    {
        internal Player
                (
                    string name,
                    string sprite,
                    string description,
                    string specialability,
                    float speed,
                    //AbilityTrigger abilityTrigger,
                    AbilityEffect[] abilityEffect
                )
        {
            this.name = name;
            this.sprite = sprite;
            this.description = description;
            this.specialability = specialability;
            this.speed = speed;
            //this.abilityTrigger = abilityTrigger;
            this.abilityEffect = abilityEffect;
        }

        public string name { get; private set; }
        public string sprite { get; private set; }
        public string description { get; private set; }
        public string specialability { get; private set; }
        public float speed { get; private set; }
        //private readonly AbilityTrigger abilityTrigger;
        private readonly AbilityEffect[] abilityEffect;
    }
}
