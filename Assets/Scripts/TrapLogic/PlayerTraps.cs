using TrapLogic.TrapAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrapLogic
{
    public sealed class PlayerTraps
    {
        private static PlayerTraps playerTraps = null;
        public Trap[] active=new Trap[3];
        
        private PlayerTraps()
        {
            active[0] = Traps.Beartrap;
            active[1] = Traps.Caltrops;
            active[2] = Traps.Springboard;
        }

        public static PlayerTraps Instance
        {
            get
            {
                if (playerTraps == null)
                {
                    playerTraps = new PlayerTraps();
                }
                return playerTraps;
            }
        }
    }
}
