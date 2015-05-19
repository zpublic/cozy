using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CozyKxlol.Server.Model;
using CozyKxlol.Network.Msg;

namespace CozyKxlol.Server
{
    
    public class FixedBallManager
    {
        private const uint DefaultMaxSize = 20;
        private uint _MaxSize = DefaultMaxSize;
        public uint MaxSize
        {
            get
            {
                return _MaxSize;
            }
            set
            {
                _MaxSize = value;
            }
        }

        private static uint _FixedBallId = 1;
        public static uint FixedBallId
        {
            get
            {
                return _FixedBallId++;
            }
        }

        Dictionary<uint, FixedBall> FixedDictionary = new Dictionary<uint, FixedBall>();

        public static Random _RandomMaker = new Random();
        public static Random RandomMaker
        {
            get
            {
                return _RandomMaker;
            }
        }
        public void Update()
        {
            while(FixedDictionary.Count < MaxSize)
            {
                Add();
            }
        }

        
        public class FixedCreateArgs : EventArgs
        {
            public uint BallId { get; set; }
            public FixedBall Ball{ get; set;}
            public FixedCreateArgs(uint id, FixedBall ball)
            {
                BallId = id;
                Ball = ball;
            }
        }
        public event EventHandler<FixedCreateArgs> FixedCreateMessage;

        private void Add()
        {
            uint id = FixedBallId;
            var ball = new FixedBall();
            ball.X = RandomMaker.Next(800);
            ball.Y = RandomMaker.Next(600);
            ball.Color = CustomColors.Colors[RandomMaker.Next(10)];
            FixedDictionary[id] = ball;
            FixedCreateMessage(this, new FixedCreateArgs(id, ball));
        }

        public void Remove(uint id)
        {
            FixedDictionary.Remove(id);
        }

        public List<KeyValuePair<uint, FixedBall>> ToList()
        {
            return FixedDictionary.ToList();
        }
    }
}
