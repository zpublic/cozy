using CozyAdventure.Game.Logic.Fight;
using CozyAdventure.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyAdventure.Game.Logic
{
    public class FightLogic
    {
        public static NormalFight CreateANormalFight(FollowerCollect partyA, FollowerCollect partyB, FightEventDelegate process)
        {
            var f = new NormalFight(partyA, partyB, process);
            return f;
        }
    }
}
