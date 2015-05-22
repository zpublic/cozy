using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CozyKxlol.Server.Model
{
    public struct PlayerBall
    {
        public const int DefaultPlayerRadius = 15;
        public float X { get; set; }
        public float Y { get; set; }
        public int Radius { get; set; }
        public uint Color { get; set; }
        public string Name { get; set; }
    }
}
