using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CozyKxlol.Engine;
using CozyKxlol.Kxlol.Impl;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CozyKxlol.Kxlol.Object
{
    class CozyCircle : CozyNode, IMoveAble
    {
        #region Property

        public Vector2 Position { get; set; }
        public float Radius { get; set; }
        public Color ColorProperty { get; set; }

        // IMoveAble
        private Vector2 _Direction;
        public Vector2 Direction
        {
            get
            {
                return _Direction;
            }
            set
            {
                _Direction = value;
                if (Math.Abs(_Direction.X) < 0.00005f && Math.Abs(_Direction.Y) < 0.00005f)
                {
                    IsMoveing = false;
                }
                else
                {
                    IsMoveing = true;
                }
            }
        }

        private bool _IsMoveing;
        public bool IsMoveing
        {
            get
            {
                return _IsMoveing;
            }
            set
            {
                _IsMoveing = value;
            }
        }
        // IMoveAble

        #endregion

        public CozyCircle()
        {
            Position        = new Vector2(0.0f, 0.0f);
            Radius          = 0.0f;
            ColorProperty   = Color.Black;
            Direction       = new Vector2(0.0f, 0.0f);
        }

        public CozyCircle(Vector2 pos, float radius, Color color)
        {
            Position        = pos;
            Radius          = radius;
            ColorProperty   = color;
            Direction       = new Vector2(0.0f, 0.0f);
        }
        public CozyCircle(Vector2 pos, float radius, Color color, Vector2 dire)
            : this(pos, radius, color)
        {
            Direction       = dire;
        }

        public override void Update(GameTime gameTime)
        {
            if (IsMoveing)
                Move(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawCircle(Position, Radius, 20, ColorProperty, Radius);
        }

        // IMoveAble
        public void Move(GameTime gameTime)
        {
            float timeDelta     = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Position            += Direction * timeDelta;
        }

        static Random ColorRandom = new Random();

        public static Color RandomColor()
        {
            ushort color = (ushort)ColorRandom.Next(0xffffff);
            return new Color(color & 0xFF0000, color & 0x00FF00, color & 0x0000FF);
        }

        public bool CanEat(CozyCircle circle)
        {
            if (circle == this) return false;
            Vector2 distanceVector = Position - circle.Position;
            float distance = distanceVector.Length();
            return Radius > (distance + circle.Radius);
        }
    }
}
