using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyDungeon.Game.Component.Card.Enum
{
    public enum RoleCardLevel : int
    {
        LevelInvalid = 0,
        Level1 = 1,
        Level2,
        Level3,
        Level4,
        Level5,
        Level6,
    };

    public class CardLevel
    {
        static Dictionary<RoleCardLevel, string> RoleCardLevelNameMap = new Dictionary<RoleCardLevel, string>()
        {
            { RoleCardLevel.LevelInvalid, "无效" },
            { RoleCardLevel.Level1, "普通" },
            { RoleCardLevel.Level2, "优秀" },
            { RoleCardLevel.Level3, "精英" },
            { RoleCardLevel.Level4, "领袖" },
            { RoleCardLevel.Level5, "传说" },
            { RoleCardLevel.Level6, "史诗" },
        };

        public static string RoleCardLevelName(RoleCardLevel level)
        {
            string name = RoleCardLevelNameMap[level];
            if (name == null || name.Count() == 0)
            {
                return "无效";
            }
            return name;
        }
    }
}
