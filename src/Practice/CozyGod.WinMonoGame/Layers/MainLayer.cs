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
            var sprite = CozySprite.Create("main.jpg");
            sprite.Position = CozyDirector.Instance.WindowSize.ToVector2() / 2;
            this.AddChind(sprite);
        }
    }
}
