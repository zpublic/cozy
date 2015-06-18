using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CozyKxlol.Kxlol.Extends;
using CozyKxlol.Kxlol.Object;
using CozyKxlol.Network.Msg;
using Microsoft.Xna.Framework;

namespace CozyKxlol.Kxlol.Scene
{
    public partial class BallGameSceneLayer
    {
        private void OnLoginRsp(MsgBase b)
        {
            var selfMsg     = (Msg_AgarLoginRsp)b;
            Uid             = selfMsg.Uid;
            int X_Size      = selfMsg.Width;
            int Y_Size      = selfMsg.Height;
            MapSize         = new Point(X_Size, Y_Size);

            string RdName   = "TestName-" + RandomMaker.NextString(5);
            var bornMsg     = new Msg_AgarBorn();
            bornMsg.UserId  = Uid;
            bornMsg.Name    = RdName;
            Name            = RdName;

            client.SendMessage(bornMsg);
        }

        private void OnFixedBall(MsgBase b)
        {
            var selfMsg     = (Msg_AgarFixedBall)b;
            uint id         = selfMsg.BallId;
            if (selfMsg.Operat == Msg_AgarFixedBall.Add)
            {
                var food    = new DefaultFoodCircle(
                    new Vector2(selfMsg.X, selfMsg.Y),
                    selfMsg.Radius, selfMsg.Color);

                FoodList[id] = food;
                this.AddChind(food, FoodZOrder);
            }
            else if (selfMsg.Operat == Msg_AgarFixedBall.Remove)
            {
                CozyCircle food = FoodList[id];
                this.RemoveChild(food);
                FoodList.Remove(id);
            }
        }


        private void OnPlayerInfo(MsgBase b)
        {
            var selfMsg     = (Msg_AgarPlayInfo)b;
            uint id         = selfMsg.UserId;
            if (selfMsg.Operat == Msg_AgarPlayInfo.Add)
            {
                var player = new DefaultUserCircle(
                    new Vector2(selfMsg.X, selfMsg.Y),
                    selfMsg.Radius,
                    selfMsg.Color,
                    selfMsg.Name);

                CircleList[id] = player;
                this.AddChind(player, OtherPlayerZOrder);
            }
            else if (selfMsg.Operat == Msg_AgarPlayInfo.Remove)
            {
                if (!CircleList.ContainsKey(id)) return;
                var player = CircleList[id];
                this.RemoveChild(player);
                CircleList.Remove(id);
            }
            else if (selfMsg.Operat == Msg_AgarPlayInfo.Changed)
            {
                if (!CircleList.ContainsKey(id)) return;
                uint tag    = selfMsg.Tag;
                var player  = CircleList[id];
                if (GameMessageHelper.Is_Changed(tag, GameMessageHelper.POSITION_TAG))
                {
                    player.Position = new Vector2(selfMsg.X, selfMsg.Y);
                }
                if (GameMessageHelper.Is_Changed(tag, GameMessageHelper.RADIUS_TAG))
                {
                    player.Radius = selfMsg.Radius;
                }
                if (GameMessageHelper.Is_Changed(tag, GameMessageHelper.COLOR_TAG))
                {
                    player.ColorProperty = selfMsg.Color.ToColor();
                }
                if (GameMessageHelper.Is_Changed(tag, GameMessageHelper.NAME_TAG))
                {
                    player.Name = selfMsg.Name;
                }
            }
        }

        private void OnFixBallPack(MsgBase b)
        {
            var selfMsg = (Msg_AgarFixBallPack)b;
            foreach (var obj in selfMsg.FixedList)
            {
                uint fid = obj.Item1;
                var food = new DefaultFoodCircle(new Vector2(obj.Item2, obj.Item3), obj.Item4, obj.Item5);
                FoodList[fid] = food;
                this.AddChind(food, FoodZOrder);
            }
        }

        private void OnPlayInfoPack(MsgBase b)
        {
            var selfMsg = (Msg_AgarPlayInfoPack)b;
            foreach (var obj in selfMsg.PLayerList)
            {
                uint pid = obj.Item1;
                var player = new DefaultUserCircle(
                    new Vector2(obj.Item2, obj.Item3),
                    obj.Item4,
                    obj.Item5,
                    obj.Item6);

                CircleList[pid] = player;
                this.AddChind(player, OtherPlayerZOrder);
            }
        }

        private void OnSelf(MsgBase b)
        {
            var selfMsg = (Msg_AgarSelf)b;

            if (selfMsg.Operat == Msg_AgarSelf.Born)
            {
                float x = selfMsg.X;
                float y = selfMsg.Y;
                int r   = selfMsg.Radius;
                uint c  = selfMsg.Color;

                DefaultRadius = r;
                Player = new DefaultUserCircle(new Vector2(x, y), r, c, Name);

                this.AddChind(Player, PlayerZOrder);

                if (ScoreShow != null)
                    ScoreShow.Text = String.Format("Score : {0}", Player.Radius - DefaultRadius);
            }
            else if (selfMsg.Operat == Msg_AgarSelf.GroupUp)
            {
                Player.Radius = selfMsg.Radius;
                if (ScoreShow != null)
                    ScoreShow.Text = String.Format("Score : {0}", Player.Radius - DefaultRadius);
            }
            else if (selfMsg.Operat == Msg_AgarSelf.Dead)
            {
                this.RemoveChild(Player);
                Player = null;

                // 玩家重生
                string RdName   = "TestName-" + RandomMaker.NextString(5);
                var bornMsg     = new Msg_AgarBorn();
                bornMsg.UserId  = Uid;
                bornMsg.Name    = RdName;
                Name            = RdName;

                client.SendMessage(bornMsg);
            }
        }

        private void OnMarkListPack(MsgBase b)
        {
            var selfMsg = (Msg_AgarMarkListPack)b;
            MarkList.Clear();

            foreach (var obj in selfMsg.MarkList)
            {
                if (CircleList.ContainsKey(obj.Key))
                {
                    MarkList.Add(new KeyValuePair<string, int>(CircleList[obj.Key].Name, obj.Value));
                }
                else if (obj.Key == Uid && Player != null)
                {
                    MarkList.Add(new KeyValuePair<string, int>(Player.Name, obj.Value));
                }
            }
            // 暂时只接受数据不显示
        }
    }
}
