using CocosSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyAdventure.View.Scene
{
    public class RegistScene : CCScene
    {
        public RegistScene() : base(AppDelegate.SharedWindow)
        {
            AddChild(new RegistUiLayer());
        }
    }
}
