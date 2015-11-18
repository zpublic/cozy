using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyKxlol.Engine;
using CozyGod.WinMonoGame.Layers;

namespace CozyGod.WinMonoGame.Scenes
{
    public class MainScene : CozyScene
    {
        public MainScene()
        {
            Init();
        }

        private void Init()
        {
            var layer = new MainLayer();
            this.AddChind(layer);
        }
    }
}
