﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyAdventure.Model
{
    public class Player
    {
        public int PlayerId { get; set; }

        public string User { get; set; }
        public string Nickname { get; set; }
        public int AdventureLevel { get; set; }
        public int FollowerMaximum { get; set; }
        public int PropMaximum { get; set; }
        public long Money { get; set; }
        public long Exp { get; set; }
        public int CurrLevel { get; set; }
        public FollowerCollect AllFollower { get; set; }
        public FollowerCollect FightFollower { get; set; }

        public Player()
        {
            AllFollower     = new FollowerCollect();
            FightFollower   = new FollowerCollect();
        }
    }
}
