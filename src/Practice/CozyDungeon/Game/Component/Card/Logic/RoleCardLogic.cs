using CozyDungeon.Game.Component.Card.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyDungeon.Game.Component.Card.Logic
{
    public class RoleCardLogic
    {
        public static void Attack(RoleCard attacker, RoleCard defender)
        {
            RoleCardAttack.Attack(attacker, defender);
        }

        public static bool IsAlive(RoleCard role)
        {
            return role.HP > 0;
        }
    }
}
