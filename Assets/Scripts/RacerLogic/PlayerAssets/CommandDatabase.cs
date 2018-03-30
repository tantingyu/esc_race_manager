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
                changeSpeed: 0,
                commandLength: 2,
                offGround: true,
                changePosition: new int[] {0,2} //(lane,block)

            );

        public static Command Sprint = new Command
            (
                name: "Sprint",
                sprite: "Sprites/Commands/Sprint",
                //description: "Deals 10 damage and stuns target.",
                
                objectCreate: "empty",
                stCost: 12,
                changeSpeed: 0,
                commandLength: 1,
                offGround: true,
                changePosition: new int[] {0,0}


            );

        public static Command Neigh = new Command
            (
                name: "Neigh",
                sprite: "Sprites/Commands/Jump",
                //description: "Deals 10 damage and stuns target.",
                objectCreate: "empty",
                stCost: 12,
                changeSpeed: 0,
                commandLength: 1,
                offGround: true,
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
                offGround: true,
                changePosition: new int[] {0,0}

            );

        public static Command PlayerTrap2 = new Command
           (
               name: "Rocket",
               sprite: "Sprites/Traps/Rocket",
               //description: "Deals 10 damage and stuns target.",
               
               objectCreate: "Rocket",
               stCost: 0,
               changeSpeed: 0,
               commandLength: 1,
               offGround: true,
               changePosition: new int[] {0,0}

           /*abilityEffect: new AbilityEffect[]
                       {
                       }*/
           );
    }
}
