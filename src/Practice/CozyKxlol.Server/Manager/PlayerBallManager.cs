using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CozyKxlol.Server.Model;

namespace CozyKxlol.Server.Manager
{
    public class PlayerBallManager
    {
        Dictionary<uint, PlayerBall> PlayerDictionary = new Dictionary<uint, PlayerBall>();

        public class PlayerExitArgs : EventArgs
        {
            public uint UserId { get; set; }
            public PlayerExitArgs(uint id)
            {
                UserId = id;
            }
        }
        public event EventHandler<PlayerExitArgs> PlayerExitMessage;

        public class PlayerDeadArgs : EventArgs
        {
            public uint UserId { get; set; }
            public PlayerDeadArgs(uint id)
            {
                UserId = id;
            }
        }
        public event EventHandler<PlayerDeadArgs> PlayerDeadMessage;

        public void Add(uint id, PlayerBall player)
        {
            PlayerDictionary[id] = player;
        }

        public void Remove(uint id)
        {
            if(PlayerDictionary.ContainsKey(id))
            {
                PlayerDictionary.Remove(id);
                PlayerExitMessage(this, new PlayerExitArgs(id));
            }
        }

        public void Change(uint id, PlayerBall newBall)
        {
            if (PlayerDictionary.ContainsKey(id))
            {
                PlayerDictionary[id] = newBall;
            }
        }

        public void Dead(uint id)
        {
            if (PlayerDictionary.ContainsKey(id))
            {
                PlayerDictionary.Remove(id);
                PlayerDeadMessage(this, new PlayerDeadArgs(id));
            }
        }

        public PlayerBall Get(uint id)
        {
            return PlayerDictionary[id];
        }

        public bool IsContain(uint id)
        {
            return PlayerDictionary.ContainsKey(id);
        }

        public List<KeyValuePair<uint, PlayerBall>> ToList()
        {
            return PlayerDictionary.ToList();
        }
    }
}
