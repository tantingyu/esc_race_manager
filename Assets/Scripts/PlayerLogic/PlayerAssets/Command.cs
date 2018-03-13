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
                            string description,
                            string specialAbility,
                            float speed,
                            AbilityEffect[] abilityEffect
                        )
        {
            this.name = name;
            this.sprite = sprite;
            this.description = description;
            this.specialAbility = specialAbility;
            this.speed = speed;
            this.abilityEffect = abilityEffect;
        }

        public string name { get; private set; }
        public string sprite { get; private set; }
        public string description { get; private set; }
        public string specialAbility { get; private set; }
        public float speed { get; private set; }
        private readonly AbilityEffect[] abilityEffect;
    }
}