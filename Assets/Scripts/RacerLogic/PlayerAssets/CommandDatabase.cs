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
                //description: "Jump...",
                objectCreate: "",
                stCost: 10,
                changeSpeed: 5,
                commandLength: 1,
                offGround: true,
                changePosition: new int[] {0,2} //(lane,block)

            );

        public static Command Dash = new Command
            (
                name: "Dash",
                sprite: "Sprites/Commands/Dash",
                //description: "Deals 10 damage and stuns target.",
                
                objectCreate: "",
                stCost: 12,
                changeSpeed: 10,
                commandLength: 1,
                offGround: true,
                changePosition: new int[] {0,2}


            );

        public static Command Neigh = new Command
            (
                name: "Neigh",
                sprite: "Sprites/Commands/Jump",
                //description: "Deals 10 damage and stuns target.",
                objectCreate: "",
                stCost: 12,
                changeSpeed: 0,
                commandLength: 1,
                offGround: false,
                changePosition: new int[] {0,0}


            );

        //Traps
        public static Command Doge = new Command
            (
                name: "Doge",
                sprite: "Sprites/Traps/Doge",
                //description: "Deals 10 damage and stuns target.",
                
                objectCreate: "Prefabs/TrapSet",
                stCost: 0,
                changeSpeed: 0,
                commandLength: 1,
                offGround: false,
                changePosition: new int[] {0,0}

            );

        public static Command Shield = new Command
           (
               name: "Shield",
               sprite: "Sprites/Commands/Shield",
               //description: "Deals 10 damage and stuns target.",
               
               objectCreate: "Prefabs/Shield",
               stCost: 50,
               changeSpeed: 0,
               commandLength: 1,
               offGround: false,
               changePosition: new int[] {0,0}

           /*abilityEffect: new AbilityEffect[]
                       {
                       }*/
           );

        public static Command Heal = new Command
           (
               name: "Heal",
               sprite: "Sprites/Commands/Heal",
               //description: "Deals 10 damage and stuns target.",

               objectCreate: "Prefabs/Heal",
               stCost: 50,
               changeSpeed: 0,
               commandLength: 1,
               offGround: false,
               changePosition: new int[] { 0, 0 }

           /*abilityEffect: new AbilityEffect[]
                       {
                       }*/
           );
    }
}
