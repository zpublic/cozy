using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CozyKxlol.Server.Model
{
    public struct PlayerBall
    {
        public const float DefaultPlayerRadius = 15.0f;
        public float X { get; set; }
        public float Y { get; set; }
        public float Radius { get; set; }
        public uint Color { get; set; }
    }
}
