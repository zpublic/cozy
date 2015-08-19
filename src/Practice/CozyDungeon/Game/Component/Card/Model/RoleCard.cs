using CozyDungeon.Game.Component.Card.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyDungeon.Game.Component.Card.Model
{
    public class RoleCard : BaseCard
    {
        // 生命值
        public int HP { get; set; } = 1;

        // 物理攻击力
        public int ATK { get; set; } = 1;

        // 物理防御
        public int DEF { get; set; } = 0;

        // 五行元素
        public FiveLine Element { get; set; } = FiveLine.Gold;

        // 卡牌等级
        public RoleCardLevel Level { get; set; } = RoleCardLevel.LevelInvalid;
    }
}
