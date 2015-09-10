using CozyAdventure.Game.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyAdventure.Game.Logic.Fight
{
    public class NormalFight
    {
        FollowerCollect PartyA;
        FollowerCollect PartyB;
        event FightEventDelegate ProcessEvent;

        // 当前进攻方
        int CurAttack;

        List<int> AllHp;
        List<int> CurHp;
        List<int> Damage;

        private NormalFight() { }
        public NormalFight(FollowerCollect partyA, FollowerCollect partyB, FightEventDelegate process)
        {
            PartyA = partyA;
            PartyB = partyB;
            ProcessEvent = process;
            Damage = new List<int>()
            {
                FollowerCollectLogic.GetAttack(partyA),
                FollowerCollectLogic.GetAttack(partyB)
            };
            AllHp = new List<int>(Damage.ToArray());
            CurHp = new List<int>(Damage.ToArray());
            CurAttack = Damage[0] >= Damage[1] ? 0 : 1;
        }

        // 调用一次，进行一步战斗，直到返回Over的FightEvent
        public void Fight()
        {
            if (!FightOver())
            {
                if (CurAttack == 0)
                {
                    CurHp[1] -= Damage[0];
                    if (CurHp[1] < 0)
                        CurHp[1] = 0;
                }
                else
                {
                    CurHp[0] -= Damage[1];
                    if (CurHp[0] < 0)
                        CurHp[0] = 0;
                }
                FightAttackEvent e = new FightAttackEvent();
                ProcessFightEvent(EnumFightEvent.Attack, e);
                CurAttack = (CurAttack == 0) ? 1 : 0;
            }
        }

        bool FightOver()
        {
            if (CurHp[0] == 0)
            {
                FightOverEvent e = new FightOverEvent()
                {
                    WinParty = 1,
                    LoseParty = 0
                };
                ProcessFightEvent(EnumFightEvent.Over, e);
                return true;
            }
            else if (CurHp[1] == 0)
            {
                FightOverEvent e = new FightOverEvent()
                {
                    WinParty = 0,
                    LoseParty = 1
                };
                ProcessFightEvent(EnumFightEvent.Over, e);
                return true;
            }
            return false;
        }

        void ProcessFightEvent(EnumFightEvent eventType, object e)
        {
            if (ProcessEvent != null)
                ProcessEvent(eventType, e);
        }
    }
}
