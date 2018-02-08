using TrapLogic.TrapAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrapLogic
{
    public sealed class PlayerTraps
    {
        public static PlayerTraps playerTraps;
        public Trap[] active;
        
        private PlayerTraps()
        {

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
