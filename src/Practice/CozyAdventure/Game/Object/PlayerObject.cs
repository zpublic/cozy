using CozyAdventure.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyAdventure.Game.Object
{
    public class PlayerObject
    {
        private PlayerObject() { }
        public static readonly PlayerObject Instance = new PlayerObject();

        public int Token { get; set; } = 0;
        public Player Self { get; } = new Player();
    }
}
