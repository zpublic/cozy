using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CozyKxlol.Server.Model.Impl;

namespace CozyKxlol.Server.Model
{
    public struct PlayerBall : ICircle
    {
        public const int DefaultPlayerRadius = 15;
        public float X { get; set; }
        public float Y { get; set; }
        public int Radius { get; set; }
        public uint Color { get; set; }
        public string Name { get; set; }
    }
}
