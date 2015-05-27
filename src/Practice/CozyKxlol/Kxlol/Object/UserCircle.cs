using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using CozyKxlol.Kxlol.Extends;
using CozyKxlol.Engine;

namespace CozyKxlol.Kxlol.Object
{
    public class UserCircle : CozyCircle
    {
        private string _Name;
        public string Name 
        { 
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
                if(TextLabel != null)
                {
                    TextLabel.Text = Name;
                }
            }
        }

        public CozyLabel TextLabel;

        public UserCircle(Vector2 pos, int radius, uint color, float borderSize, string name)
            :base(pos, radius, color.ToColor(), borderSize)
        {
            Name = name;
            TextLabel = new CozyLabel(name, Color.WhiteSmoke);
            TextLabel.Position = ContentSize / 2;
            this.AddChind(TextLabel);
        }
    }
}
