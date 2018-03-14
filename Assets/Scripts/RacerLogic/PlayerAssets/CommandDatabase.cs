using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RacerLogic.RacerAssets
{
    public class CommandDatabase
    {
        public static Command Jump = new Command
            (
                name: "Jump",
                sprite: "Sprites/Commands/Jump",
                description: "Jump...",
                stCost: 10,
                abilityEffect: new AbilityEffect[]
                            {
                            }
            );

        public static Command Sprint = new Command
            (
                name: "Sprint",
                sprite: "Sprites/Commands/Sprint",
                description: "Deals 10 damage and stuns target.",
                stCost: 12,
                abilityEffect: new AbilityEffect[]
                            {
                            }
            );

        public static Command Neigh = new Command
            (
                name: "Neigh",
                sprite: "Sprites/Commands/Jump",
                description: "Deals 10 damage and stuns target.",
                stCost: 12,
                abilityEffect: new AbilityEffect[]
                            {
                            }
            );
    }
}
