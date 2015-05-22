using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CozyKxlol.Server.Model;
using CozyKxlol.Network.Msg;

namespace CozyKxlol.Server.Manager
{
    
    public class FixedBallManager
    {
        private const uint DefaultMaxSize   = 20;
        private uint _MaxSize               = DefaultMaxSize;
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

        private static uint _FixedBallId    = 1;
        public static uint FixedBallId
        {
            get
            {
                return _FixedBallId++;
            }
        }

        Dictionary<uint, FixedBall> FixedDictionary = new Dictionary<uint, FixedBall>();

        public static Random _RandomMaker           = new Random();
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
                FixedBall ball = RandomBall();
                Add(ball);
            }
        }

        
        public class FixedCreateArgs : EventArgs
        {
            public uint BallId { get; set; }
            public FixedBall Ball{ get; set;}
            public FixedCreateArgs(uint id, FixedBall ball)
            {
                BallId  = id;
                Ball    = ball;
            }
        }
        public class FixedRemoveArgs : EventArgs
        {
            public uint BallId { get; set; }
            public FixedRemoveArgs(uint id)
            {
                BallId = id;
            }
        }

        public event EventHandler<FixedCreateArgs> FixedCreateMessage;
        public event EventHandler<FixedRemoveArgs> FixedRemoveMessage;

        public static FixedBall RandomBall()
        {
            var ball    = new FixedBall();
            ball.X      = RandomMaker.Next(800);
            ball.Y      = RandomMaker.Next(600);
            ball.Radius = FixedBall.DefaultPlayerRadius;
            ball.Color  = CustomColors.RandomColor;
            return ball;
        }

        private void Add(FixedBall ball)
        {
            uint id     = FixedBallId;
            FixedDictionary[id] = ball;
            FixedCreateMessage(this, new FixedCreateArgs(id, ball));
        }

        public void Remove(uint id)
        {
            FixedDictionary.Remove(id);
            FixedRemoveMessage(this, new FixedRemoveArgs(id));
        }

        public List<KeyValuePair<uint, FixedBall>> ToList()
        {
            return FixedDictionary.ToList();
        }
    }
}
