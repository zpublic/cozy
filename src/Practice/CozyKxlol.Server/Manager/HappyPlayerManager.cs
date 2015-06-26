﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CozyKxlol.Server.Model;

namespace CozyKxlol.Server.Manager
{
    public class HappyPlayerManager
    {
        Dictionary<uint, HappyPlayer> PlayerDictionary = new Dictionary<uint, HappyPlayer>();


        public void Add(uint id, HappyPlayer player)
        {
            PlayerDictionary[id] = player;
        }

        public void Remove(uint id)
        {
            if(PlayerDictionary.ContainsKey(id))
            {
                PlayerDictionary.Remove(id);
            }
        }

        public HappyPlayer Get(uint id)
        {
            if(PlayerDictionary.ContainsKey(id))
            {
                return PlayerDictionary[id];
            }
            return new HappyPlayer();
        }

        public List<KeyValuePair<uint, HappyPlayer>> ToList()
        {
            return PlayerDictionary.ToList();
        }
    }
}
