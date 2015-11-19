using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyKxlol.Engine;
using Microsoft.Xna.Framework;

namespace CozyGod.WinMonoGame.Layers
{
    public class MainLayer : CozyLayer
    {
        public MainLayer()
        {
            Init();
        }

        private void Init()
        {
            var sprite = CozySprite.Create(@"Image/main_background.png");
            sprite.Position = CozyDirector.Instance.WindowSize.ToVector2() / 2;
            this.AddChind(sprite);

            var logo = CozySprite.Create(@"Image/main_logo.png");
            logo.Position = CozyDirector.Instance.WindowSize.ToVector2() / 2;
            this.AddChind(logo);

            var enter = CozySprite.Create(@"Image/main_enter_normal.png");
            enter.Position = new Vector2(CozyDirector.Instance.WindowSize.X / 2, 450);
            this.AddChind(enter);
        }
    }
}
