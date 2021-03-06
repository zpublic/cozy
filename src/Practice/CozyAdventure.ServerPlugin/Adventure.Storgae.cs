﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyAdventure.ServerPlugin.Storgae;

namespace CozyAdventure.ServerPlugin
{
    public static class AdventurePluginDB
    {
        public static readonly UserDB User = new UserDB();

        public static readonly CustomerInfoDB Customer = new CustomerInfoDB();

        public static readonly PlayerFollowerDB PlayerFollower = new PlayerFollowerDB();

        public static readonly FollowerDB Follower = new FollowerDB();
    }
}
