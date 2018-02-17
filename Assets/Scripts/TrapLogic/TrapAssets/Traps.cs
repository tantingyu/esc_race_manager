using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrapLogic.TrapAssets
{
    public static class Traps
    {
        public static Trap Beartrap = new Trap
            (
                name: "Beartrap",
                sprite: "Traps/beartrap",
                description: "Deals 10 damage and stuns target.",
                cost: 10,
                cooldown: 3,
                trapTrigger: null,
                trapEffect: new TrapEffect[]
                            {
                            }
            );

        public static Trap Caltrops = new Trap
            (
                name: "Caltrops",
                sprite: "Traps/caltrops",
                description: "Deals 10 damage and slows target enemy.",
                cost: 10,
                cooldown: 3,
                trapTrigger: null,
                trapEffect: new TrapEffect[]
                            {
                            }
            );

        public static Trap Springboard = new Trap
            (
                name: "Springboard",
                sprite: "Traps/springboard",
                description: "Launches target 3 squares.",
                cost: 10,
                cooldown: 3,
                trapTrigger: null,
                trapEffect: new TrapEffect[]
                            {
                            }
            );
    }
}
