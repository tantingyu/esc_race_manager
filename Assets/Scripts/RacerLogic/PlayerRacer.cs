using RacerLogic.RacerAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RacerLogic
{
    public sealed class PlayerRacer
    {
        private static PlayerRacer playerRacer = null;
        public Racer racer = null;
        public Command[] commands = null;
        
        private PlayerRacer()
        {
            //get racer index from lobby here
            racer = RacerDatabase.p1;
            commands = racer.commands;
        }

        public static PlayerRacer Instance
        {
            get
            {
                if (playerRacer == null)
                {
                    playerRacer = new PlayerRacer();
                }
                return playerRacer;
            }
        }
    }
}

