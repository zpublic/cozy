using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyGod.Game.GameConfig.ConfigAttribute;

namespace CozyGod.Game.GameConfig
{
    public sealed class ConfigObject
    {
        [CozyConfig]
        public string ContentPath { get; set; }
    }
}
