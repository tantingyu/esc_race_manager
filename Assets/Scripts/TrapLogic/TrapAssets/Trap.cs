using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TrapLogic.TrapAssets
{
    public class Trap
    {
        internal Trap
                (
                    string name,
                    string sprite,
                    string description,
                    float cost,
                    float cooldown,
                    TrapTrigger trapTrigger,
                    TrapEffect[] trapEffect
                )
        {
            this.name = name;
            this.sprite = sprite;
            this.description = description;
            this.cost = cost;
            this.cooldown = cooldown;
            this.trapTrigger = trapTrigger;
            this.trapEffect = trapEffect;
        }

        public string name { get; private set; }
        public string sprite { get; private set; }
        public string description { get; private set; }
        public float cost { get; private set; }
        public float cooldown { get; private set; }
        private readonly TrapTrigger trapTrigger;
        private readonly TrapEffect[] trapEffect;
    }
}
