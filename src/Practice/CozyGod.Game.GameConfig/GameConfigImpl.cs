using CozyGod.Game.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyGod.Game.GameConfig
{
    public class GameConfigImpl : IGameConfig
    {
        public string GetContentPath()
        {
            return @"g:\code\cozy\src\Practice\CozyGod.Content\";
        }
    }
}
