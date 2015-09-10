using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyAdventure.Game.Logic.Fight
{
    public delegate void FightEventDelegate(EnumFightEvent eventType, object e);

    public enum EnumFightEvent
    {
        Attack,
        Over,
    }

    public class FightAttackEvent
    {
        // 0表示第一方，1表示第二方，依此类推
        public int AttackParty { get; set; } = 0;
        // 造成伤害
        public int Damage { get; set; }
        // 总血量
        public List<int> AllHp { get; set; }
        // 剩余血量
        public List<int> CurHp { get; set; }
    }

    public class FightOverEvent
    {
        public int WinParty { get; set; } = 0;
        public int LoseParty { get; set; } = 1;
    }
}
