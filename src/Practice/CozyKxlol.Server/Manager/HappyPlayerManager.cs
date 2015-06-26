using System;
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
                if(HappyPlayerQuitMessage != null)
                {
                    HappyPlayerQuitMessage(this, new HappyPlayerQuitArgs(id));
                }
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

        public bool Modify(uint id, HappyPlayer data)
        {
            if(PlayerDictionary.ContainsKey(id))
            {
                PlayerDictionary[id] = data;
                return true;
            }
            return false;
        }

        public List<KeyValuePair<uint, HappyPlayer>> ToList()
        {
            return PlayerDictionary.ToList();
        }

        public class HappyPlayerQuitArgs : EventArgs
        {
            public uint UserId { get; set; }
            public HappyPlayerQuitArgs(uint id)
            {
                UserId = id;
            }
        }
        public event EventHandler<HappyPlayerQuitArgs> HappyPlayerQuitMessage;
    }
}
