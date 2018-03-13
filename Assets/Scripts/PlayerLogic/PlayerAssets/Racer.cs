using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RacerLogic.RacerAssets
{
    public class Racer
    {
        internal Racer
                (
                    string name,
                    string sprite,
                    float hp,
                    float st,
                    float speed,
                    Command[] commands
                )
        {
            this.name = name;
            this.sprite = sprite;
            this.hp = hp;
            this.st = st;
            this.speed = speed;
            this.commands = commands;
        }

        public string name { get; private set; }
        public string sprite { get; private set; }
        public float hp { get; private set; }
        public float st { get; private set; }
        public float speed { get; private set; }
        //private readonly AbilityTrigger abilityTrigger;
        public readonly Command[] commands;
    }
}
