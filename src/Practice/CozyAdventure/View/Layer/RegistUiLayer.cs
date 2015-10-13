using CocosSharp;
using Cozy.Game.Manager;
using CozyAdventure.Game.Logic;
using CozyAdventure.Public.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyAdventure.View.Layer
{
    public class RegistUiLayer : CCLayer
    {
        private CCPoint beginPosition;

        private CCTextField NameText { get; set; }
        private CCTextField PassText { get; set; }
        private CCTextField PassRepeatText { get; set; }
        private CCTextField NickNameText { get; set; }

        private List<CCTextField> FiledList { get; set; } = new List<CCTextField>();

        private CozySampleButton CurrButton { get; set; }

        private ButtonEventDispatcher dispatcher { get; set; } = new ButtonEventDispatcher();

        public override void OnEnter()
        {
            base.OnEnter();
            InitUI();
            RegisterEvent();
        }

        public override void OnExit()
        {
            base.OnExit();
            UnregisterEvent();
        }

        private void RegisterEvent()
        {
            MessageManager.RegisterMessage("Message.Register.Success", OnRegisterSuccess);
            MessageManager.RegisterMessage("Message.Register.Failed", OnRegisterFailed);
            dispatcher.AttachListener(this);
        }

        private void UnregisterEvent()
        {
            dispatcher.DetachListener(this);
            MessageManager.UnRegisterMessage("Message.Register.Failed", OnRegisterFailed);
            MessageManager.UnRegisterMessage("Message.Register.Success", OnRegisterSuccess);
        }

        private void InitUI()
        {
            var s = VisibleBoundsWorldspace.Size;

            NameText = new CCTextField("[Name]", "Consolas", 16)
            {
                Position = new CCPoint(s.Width / 2, 400),
                AutoEdit = true,
            };
            AddChild(NameText, 100);
            FiledList.Add(NameText);

            PassText = new CCTextField("[Pass]", "Consolas", 16)
            {
                Position = new CCPoint(s.Width / 2, 350),
                AutoEdit = true,
            };
            AddChild(PassText, 100);
            FiledList.Add(PassText);

            PassRepeatText = new CCTextField("[Pass Repeat]", "Consolas", 16)
            {
                Position = new CCPoint(s.Width / 2, 300),
                AutoEdit = true,
            };
            AddChild(PassRepeatText, 100);
            FiledList.Add(PassRepeatText);

            NickNameText = new CCTextField("[NickName]", "Consolas", 16)
            {
                Position = new CCPoint(s.Width / 2, 250),
                AutoEdit = true,
            };
            AddChild(NickNameText, 100);
            FiledList.Add(NickNameText);

            CurrButton = new CozySampleButton(s.Width / 2, 100, 200, 80)
            {
                Text = "注册账号",
                FontSize = 24,
                OnClick = () => OnRegister()
            };
            AddChild(CurrButton, 100);
            dispatcher.Add(CurrButton);
        }

        protected override void AddedToScene()
        {
            base.AddedToScene();
            var touchListener               = new CCEventListenerTouchOneByOne();

            touchListener.OnTouchBegan = OnTouchBegan;
            touchListener.OnTouchEnded = OnTouchEnded;

            AddEventListener(touchListener);
        }

        private void OnTouchEnded(CCTouch arg1, CCEvent arg2)
        {
            var endPos = arg1.Location;

            foreach (var TrackNode in FiledList)
            {
                OnClickTrackNode(TrackNode, false);
            }

            foreach (var TrackNode in FiledList)
            {
                if (TrackNode.BoundingBox.ContainsPoint(beginPosition) && TrackNode.BoundingBox.ContainsPoint(endPos))
                {
                    OnClickTrackNode(TrackNode, true);
                }
            }
        }

        private void OnClickTrackNode(CCTextField node,bool v)
        {
            if (v && node != null)
            {
                node.Edit();
            }
            else
            {
                if (node != null)
                {
                    node.EndEdit();
                }
            }
        }

        private bool OnTouchBegan(CCTouch arg1, CCEvent arg2)
        {
            beginPosition = arg1.Location;
            return true;
        }

        private void OnRegister()
        {
            string name         = NameText.Text.Trim();
            string pass         = PassText.Text.Trim();
            string passrepeat   = PassRepeatText.Text.Trim();
            string nickname     = NickNameText.Text.Trim();

            if (pass == passrepeat)
            {
                if (CheckInput(name, pass, nickname))
                {
                    UserLogic.Regist(name, pass, nickname);
                    return;
                }
            }

            // TODO check failed
        }

        /// <summary>
        /// 名字与昵称长度大于0 两次密码长度大于6且相等
        /// </summary>
        private bool CheckInput(string name, string pass, string nickname)
        {
            if (pass.Length >= 6 && name.Length > 0 && nickname.Length > 0)
            {
                return true;
            }
            return false;
        }

        private void OnRegisterSuccess()
        {
            CurrButton.Text     = "已注册成功，点击返回";
            CurrButton.OnClick  = new Action(OnRetButton);
        }

        private void OnRegisterFailed()
        {
            
        }

        public void OnRetButton()
        {
            AppDelegate.SharedWindow.DefaultDirector.PopScene();
        }
    }
}
