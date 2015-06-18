using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;
using CozyKxlol.Network.Msg;
using CozyKxlol.Server.Model;
using CozyKxlol.Server.Model.Impl;

namespace CozyKxlol.Server
{
    public partial class Program
    {
        public static bool CanEat(ICircle player, ICircle ball)
        {
            float X_Distance    = player.X - ball.X;
            float Y_Distance    = player.Y - ball.Y;
            float Distance      = (float)Math.Sqrt(X_Distance * X_Distance + Y_Distance * Y_Distance);
            return player.Radius > (Distance + ball.Radius);
        }

        public static bool UpdateFood(uint id, ref PlayerBall ball)
        {
            bool FoodRemoveFlag = false;
            foreach (var obj in FixedBallMgr.ToList())
            {
                if (CanEat(ball, obj.Value))
                {
                    FoodRemoveFlag = true;
                    if (ball.Radius < PlayerBall.DefaultPlayerNoFoodRadius)
                        ball.Radius++;
                    FixedBallMgr.Remove(obj.Key);
                }
            }
            if (FoodRemoveFlag)
            {
                FixedBallMgr.Update();
            }
            return FoodRemoveFlag;
        }

        public static bool UpdatePlayer(uint id, ref PlayerBall ball)
        {
            bool PlayerDeadFlag = false;
            foreach (var obj in PlayerBallMgr.ToList())
            {
                if (obj.Key != id && CanEat(ball, obj.Value))
                {
                    PlayerDeadFlag = true;
                    ball.Radius += obj.Value.Radius;
                    PlayerBallMgr.Dead(obj.Key);
                }
            }
            return PlayerDeadFlag;
        }

        public static bool UpdateOtherPlayer(uint id, PlayerBall ball, out uint EatId)
        {
            bool PlayerDeadFlag = false;
            uint did            = 0;
            foreach (var obj in PlayerBallMgr.ToList())
            {
                if (obj.Key != id && CanEat(obj.Value, ball))
                {
                    PlayerDeadFlag = true;
                    did = obj.Key;
                    PlayerBallMgr.Dead(id);
                    break;
                }
            }
            EatId = did;
            return PlayerDeadFlag;
        }
    }
}
