using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerLogic.PlayerAssets
{
    public static class Players
    {
        public static Player p1 = new Player
            (
                name: "pipi",
                sprite: "Players/pipi",
                description: "Deals 10 damage and stuns target.",
                specialability: "accelerate",
                speed: 10,
                abilityEffect: new AbilityEffect[]
                            {
                            }
            );

        public static Player p2 = new Player
            (
                name: "lala",
                sprite: "Players/lala",
                description: "Deals 10 damage and stuns target.",
                specialability: "punch",
                speed: 12,
                abilityEffect: new AbilityEffect[]
                            {
                            }
            );

        public static Player p3 = new Player
            (
                name: "bobo",
                sprite: "Traps/bobo",
                description: "Deals 10 damage and stuns target.",
                specialability: "jump",
                speed: 12,
                abilityEffect: new AbilityEffect[]
                            {
                            }
            );
    }
}

