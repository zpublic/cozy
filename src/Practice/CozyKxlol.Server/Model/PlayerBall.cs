using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CozyKxlol.Server.Model.Interface;

namespace CozyKxlol.Server.Model
{
    public struct PlayerBall : ICircle
    {
        public const int DefaultPlayerRadius        = 15;
        public const int DefaultPlayerNoFoodRadius  = 65;
        public const int DefaultPlayerMaxRadius     = 100;
        public float X { get; set; }
        public float Y { get; set; }
        public uint Color { get; set; }
        public string Name { get; set; }

        private int _Radius;
        public int Radius 
        { 
            get
            {
                return _Radius;
            }
            set
            {
                _Radius = value < DefaultPlayerRadius ? value : 
                    (value < DefaultPlayerMaxRadius ? value : DefaultPlayerMaxRadius);
            }
        }
    }
}
