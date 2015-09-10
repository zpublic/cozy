using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyAdventure.Game.Model
{
    public class Player
    {
        private Player() { }
        public static readonly Player Instance = new Player();

        public int Token { get; set; } = 0;
    }
}
