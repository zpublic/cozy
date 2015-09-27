using System;
using CocosSharp;
using CozyAdventure.Public.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyAdventure.Game.Object;
using CozyAdventure.Model;
using CozyAdventure.View.Sprite;
using CozyAdventure.Game.Logic;
using CozyAdventure.Protocol.Msg;
using Cozy.Game.Manager;
using CozyNetworkProtocol;
using CozyAdventure.Protocol;

namespace CozyAdventure.View.Layer
{
    public class FollowerListUiLayer : CCLayer
    {
        private FollowerCollect FollowerList { get; set; }

        private int Page { get; set; }

        private int CurPage { get; set; }

        private List<FollowerSprite> SpriteList { get; set; } = new List<FollowerSprite>();

        private CCLabel AllFollower { get; set; }

        private CCLabel PageNumber { get; set; }

        private CozySampleButton LastPageButton { get; set; }

        private CozySampleButton NextPageButton { get; set; }

        private CozySampleListView[] InnerList { get; set; }

        private FollowerDetailSprite ShowDetail { get; set; }

        private List<CCEventListenerTouchOneByOne> ListenerList { get; set; } = new List<CCEventListenerTouchOneByOne>();

        public FollowerListUiLayer()
        {
            var listlable = new CCLabel("佣兵列表", "微软雅黑", 14)
            {
                Position    = new CCPoint(100, 420),
                Color       = CCColor3B.White
            };

            FollowerList    = PlayerObject.Instance.Self.AllFollower;
            Page            = (FollowerList.Followers.Count + 8) / 9;

            AddChild(listlable, 100);

            foreach (var obj in FollowerList.Followers)
            {
                var fs = new FollowerSprite(obj, true);
                this.AddChild(fs);
                SpriteList.Add(fs);
            }

            var listview = new CozySampleListView()
            {
                ContentSize = new CCSize(600, 330),
                HasBorder   = true,
                Position    = new CCPoint(100, 60)
            };
            this.AddChild(listview);

            InnerList = new CozySampleListView[3];
            for (int i = 0; i < 3; ++i)
            {
                InnerList[i] = new CozySampleListView()
                {
                    ContentSize = new CCSize(200, 330),
                    Orientation = Public.Controls.Enum.ControlOrientation.Vertical,
                };
                listview.AddItem(new CozySampleListViewItemNode(InnerList[i]));
            }

            AllFollower = new CCLabel("总数" + 20 + "/" + FollowerList.Followers.Count, "微软雅黑", 14)
            {
                Position    = new CCPoint(92, 37),
                Color       = CCColor3B.White,
            };
            AddChild(AllFollower, 100);

            PageNumber = new CCLabel((CurPage + 1) + "/" + Page, "微软雅黑", 14)
            {
                Position    = new CCPoint(389, 37),
                Color       = CCColor3B.White
            };
            AddChild(PageNumber, 100);

            ShowDetail = new FollowerDetailSprite()
            {
                Position    = new CCPoint(100, 100),
                AnchorPoint = CCPoint.Zero,
                Visible     = false,
            };
            this.AddChild(ShowDetail, 201);

            LastPageButton = new CozySampleButton(473, 17, 78, 36)
            {
                Text        = "上一页",
                FontSize    = 14,
                OnClick     = () => PrevPage(),
            };
            NextPageButton = new CozySampleButton(585, 17, 78, 36)
            {
                Text        = "下一页",
                FontSize    = 14,
                OnClick     = () => NextPage(),
            };
            AddChild(NextPageButton, 100);
            AddChild(LastPageButton, 100);
            this.AddEventListener(LastPageButton.EventListener);
            this.AddEventListener(NextPageButton.EventListener);

            RefreshPage();

            MessageManager.RegisterMessage("Client.Data", OnMessage);
            FramMapLogic.EnterMap(1);
        }

        private void RefreshPage()
        {
            foreach (var obj in SpriteList)
            {
                obj.Visible = false;
            }
            foreach (var list in InnerList)
            {
                list.Clear();
            }
            foreach(var obj in ListenerList)
            {
                this.RemoveEventListener(obj);
            }
            ListenerList.Clear();

            int count = CurPage * 9;
            for (int i = 0; i < 3; ++i)
            {
                for (int j = 0; j < 3; ++j)
                {
                    if (count < FollowerList.Followers.Count)
                    {
                        int index = count;

                        var item = new CozySampleListViewItemNode(SpriteList[index])    
                        {
                            MarginTop       = 5,
                            MarginBottom    = 5,
                        };

                        var button  = new CozySampleButton(item.ContentSize.Width, item.ContentSize.Height)
                        {
                            OnClick = () =>
                            {
                                if(!ShowDetail.Visible)
                                {
                                    ShowDetail.CurrFollower = SpriteList[index].BindFollower;
                                    ShowDetail.Visible      = true;
                                }
                            },
                        };
                        item.AddChild(button);
                        this.AddEventListener(button.EventListener);
                        ListenerList.Add(button.EventListener);

                        SpriteList[index].Visible = true;
                        InnerList[i].AddItem(item);
                    }
                    count++;
                }
            }
            PageNumber.Text = (CurPage + 1) + "/" + Page;
        }

        private void NextPage()
        {
            CurPage = CurPage == Page - 1 ? Page - 1 : CurPage + 1;
            RefreshPage();
        }

        private void PrevPage()
        {
            CurPage = CurPage == 0 ? 0 : CurPage - 1;
            RefreshPage();
        }

        private void OnMessage(object obj)
        {
            var msg = (MessageBase)obj;
            if (msg.Id == (uint)MessageId.Farm.FarmIncomeMessage)
            {
                OnFarmIncommingMessage((FarmIncomeMessage)msg);
            }
            else if (msg.Id == (uint)MessageId.Farm.GotoResultMessage)
            {
                OnGotoResultMessage((GotoResultMessage)msg);
            }

        }

        private void OnFarmIncommingMessage(FarmIncomeMessage msg)
        {
            int i = 42;
        }

        private void OnGotoResultMessage(GotoResultMessage msg)
        {
            int i = 42;
        }
    }
}
