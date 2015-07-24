using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CozyKxlol.Server.Model.Interface;

namespace CozyKxlol.Server.Model
{
    public struct FixedBall : ICircle
    {
        public const int DefaultPlayerRadius = 5;
        public float X { get; set; }
        public float Y { get; set; }
        public int Radius { get; set; }
        public uint Color { get; set; }
    }
}
