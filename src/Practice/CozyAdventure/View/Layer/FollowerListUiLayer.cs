using System;
using CocosSharp;
using CozyAdventure.View.Layer;
using CozyAdventure.Public.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CozyAdventure.View.Layer
{
    public class FollowerListUiLayer : CCLayer
    {
        public FollowerListUiLayer()
        {
            var listlable = new CCLabel("佣兵列表", "微软雅黑", 14)
            {
                Position = new CCPoint(92, 20),
                Color = CCColor3B.Black
            };
            AddChild(listlable, 100);
            for (int i = 1; i <= 12; i++)
            {
                for (int a = 92; a <= 543; a += 148)
                {
                    for (int b = 52; b <= 289; b += 119)
                    {
                        var Follower = new CozySampleButton(a, b, 120, 100)
                        {
                            Text = "佣兵" + i,
                            FontSize = 14
                        };
                        AddChild(Follower, 100);
                    }
                }
            }
            int c = 20;
            int d = 30;
            var AllFollower = new CCLabel("总数" + c + "/" + d, "微软雅黑", 14)
            {
                Position = new CCPoint(92, 420),
                Color = CCColor3B.White,
            };
            AddChild(AllFollower, 100);
            int e = 20;
            int f = 30;
            var PageNumber = new CCLabel(e + "/" + f, "微软雅黑", 14)
            {
                Position = new CCPoint(389, 420),
                Color = CCColor3B.Black
            };
            AddChild(PageNumber, 100);
            var LastPage = new CozySampleButton(473, 410, 78, 36)
            {
                Text = "上一页",
                FontSize = 14
            };
            AddChild(LastPage, 100);
            var NextPage = new CozySampleButton(585, 410, 78, 36)
            {
                Text = "下一页",
                FontSize = 14
            };
            AddChild(NextPage, 100);
        }
    }
}
