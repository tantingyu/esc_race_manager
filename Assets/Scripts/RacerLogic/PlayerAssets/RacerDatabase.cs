﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RacerLogic.RacerAssets
{
    public static class RacerDatabase
    {
        public static Racer p1 = new Racer
            (
                name: "pipi",
                sprite: "Players/pipi",
                hp: 150,
                st: 50,
                speed: 10,
                commands: new Command[]
                            {
                                CommandDatabase.Jump,
                                CommandDatabase.Shield,
                                CommandDatabase.Doge
                            }
            );

        public static Racer p2 = new Racer
            (
                name: "lala",
                sprite: "Players/lala",
                hp: 100,
                st: 100,
                speed: 12,
                commands: new Command[]
                            {
                                CommandDatabase.Sprint,
                                CommandDatabase.Doge,
                                CommandDatabase.Jump
                            }
            );

        public static Racer p3 = new Racer
            (
                name: "bobo",
                sprite: "Traps/bobo",
                hp: 50,
                st: 150,
                speed: 12,
                commands: new Command[]
                            {
                                CommandDatabase.Doge,
                                CommandDatabase.Jump,
                                CommandDatabase.Sprint
                            }
            );
    }
}

