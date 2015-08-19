using CozyDungeon.Game.Component.Card.Enum;
using CozyDungeon.Game.Component.Card.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyDungeon.Game.Component.Card.Logic
{
    class RoleCardAttack
    {
        static Dictionary<FiveLine, FiveLine> KeMap = new Dictionary<FiveLine, FiveLine>()
        {
            { FiveLine.Gold, FiveLine.Wood },
            { FiveLine.Wood, FiveLine.Earth },
            { FiveLine.Earth, FiveLine.Water },
            { FiveLine.Water, FiveLine.Fire },
            { FiveLine.Fire, FiveLine.Gold },
        };
        static bool Ke(FiveLine fl1, FiveLine fl2)
        {
            return KeMap[fl1] == fl2;
        }

        static Dictionary<FiveLine, FiveLine> ShenMap = new Dictionary<FiveLine, FiveLine>()
        {
            { FiveLine.Gold, FiveLine.Water },
            { FiveLine.Water, FiveLine.Wood },
            { FiveLine.Wood, FiveLine.Fire },
            { FiveLine.Fire, FiveLine.Earth },
            { FiveLine.Earth, FiveLine.Gold },
        };
        static bool Shen(FiveLine fl1, FiveLine fl2)
        {
            return ShenMap[fl1] == fl2;
        }

        public static void Attack(RoleCard attacker, RoleCard defender)
        {
            int damage = attacker.ATK - defender.DEF;
            if (damage > 0)
            {
                defender.HP -= damage;
            }
            // 依据五行相生相克
            // 金攻击木或者木攻击金，木都会受到1点伤害。
            // 金攻击水或者水攻击金，水都会回复1点生命，
            if (Ke(attacker.Element, defender.Element))
            {
                defender.HP--;
            }
            else if (Ke(defender.Element, attacker.Element))
            {
                attacker.HP--;
            }
            if (Shen(attacker.Element, defender.Element))
            {
                defender.HP++;
            }
            else if (Shen(defender.Element, attacker.Element))
            {
                attacker.HP++;
            }
        }
    }
}
