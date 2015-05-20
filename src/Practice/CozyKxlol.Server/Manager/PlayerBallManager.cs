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
            public uint PlayerId { get; set; }
            public PlayerExitArgs(uint id)
            {
                PlayerId = id;
            }
        }

        public event EventHandler<PlayerExitArgs> PlayerExitMessage;

        public void Add(uint id, PlayerBall player)
        {
            PlayerDictionary[id] = player;
        }

        public void Remove(uint id)
        {
            PlayerDictionary.Remove(id);
            PlayerExitMessage(this, new PlayerExitArgs(id));
        }

        public void Change(uint id, PlayerBall newBall)
        {
            PlayerDictionary[id] = newBall;
        }

        public List<KeyValuePair<uint, PlayerBall>> ToList()
        {
            return PlayerDictionary.ToList();
        }
    }
}
