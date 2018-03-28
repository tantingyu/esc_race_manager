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
                            string objectCreate,
                            float stCost,
                            AbilityEffect[] abilityEffect
                        )
        {
            this.name = name;
            this.sprite = sprite;
            this.description = description;
            this.stCost = stCost;
            this.abilityEffect = abilityEffect;
            this.objectCreate = objectCreate;
        }

        public string name { get; private set; }
        public string objectCreate { get; private set; }
        public string sprite { get; private set; }
        public string description { get; private set; }
        public float stCost { get; private set; }
        private readonly AbilityEffect[] abilityEffect;
    }
}