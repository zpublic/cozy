using Cozy.Game.Manager;
using CozyAdventure.Game.Object;
using CozyAdventure.Model;
using CozyAdventure.Protocol.Msg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyAdventure.Game.Logic
{
    public class FollowerCollectLogic
    {
        public static int GetAttack(FollowerCollect fc)
        {
            int attack = 0;
            foreach (var i in fc.Followers)
            {
                attack += FollowerLogic.GetAttack(i);
            }
            return attack;
        }

        // 血量等于攻击力
        public static int GetHp(FollowerCollect fc)
        {
            return GetAttack(fc);
        }

        public static void GoFight(Follower follower)
        {
            var msg = new FightMessage()
            {
                PlayerId    = PlayerObject.Instance.Self.PlayerId,
                ObjectId    = follower.ObjectId,
                FightType   = FightMessage.GoToFight,
            };
            MessageManager.SendMessage("Client.Send", msg);
        }

        public static void GoRest(Follower follower)
        {
            var msg = new FightMessage()
            {
                PlayerId    = PlayerObject.Instance.Self.PlayerId,
                ObjectId    = follower.ObjectId,
                FightType   = FightMessage.GoToRest,
            };
            MessageManager.SendMessage("Client.Send", msg);
        }

        public static void HireFollower(Follower follower)
        {
            var msg = new HireFollowerMessage()
            {
                PlayerId    = PlayerObject.Instance.Self.PlayerId,
                FollowerId  = { follower.Id },
            };
            MessageManager.SendMessage("Client.Send", msg);
        }

        public static void HireFollower(List<Follower> follower)
        {
            var msg = new HireFollowerMessage()
            {
                PlayerId    = PlayerObject.Instance.Self.PlayerId,
                FollowerId  = follower.Select(x => x.Id).ToList(),
            };
            MessageManager.SendMessage("Client.Send", msg);
        }
    }
}
