using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CozyKxlol.Engine;
using CozyKxlol.Kxlol.Impl;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Starbound.Input;

namespace CozyKxlol.Kxlol.Object
{
    class CozyCircle : CozyNode, IMoveAble, IControlAble
    {
        #region Property

        enum EnableMoveTag : int
        {
            LeftTag,
            RightTag,
            UpTag,
            DownTag,
        }

        public Vector2 Position { get; set; }
        public float Radius { get; set; }
        public Color ColorProperty { get; set; }

        // IMoveAble
        private float MoveDamping = 1.0f;
        private Vector2 _Direction;
        public Vector2 Direction
        {
            get
            {
                return _Direction;
            }
            set
            {
                _Direction = new Vector2(Math.Abs(value.X) > 1.0f ? (value.X > 0.0f ? 1.0f : -1.0f) : value.X,
                                         Math.Abs(value.Y) > 1.0f ? (value.Y > 0.0f ? 1.0f : -1.0f) : value.Y);
            }
        }

        // v = 20 + 200 / S
        public const float BaseSpeed    = 20.0f;
        public const float FloatSpeed   = 200.0f;
        public float Speed
        {
            get
            {
                return BaseSpeed + FloatSpeed / Radius;
            }
        }

        // IMoveAble
        public bool IsMoving
        {
            get
            {
                return MoveEnable[0] || MoveEnable[1] || MoveEnable[2] || MoveEnable[3];
            }
        }

        private bool[] _MoveEnable = new bool[4];
        public bool[] MoveEnable 
        { 
            get
            {
                return _MoveEnable;
            }
        }

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

        static Random ColorRandom = new Random();
        public static Color RandomColor()
        {
            ushort color = (ushort)ColorRandom.Next(0xffffff);
            return new Color(color & 0xFF0000, color & 0x00FF00, color & 0x0000FF);
        }

        public override void Update(GameTime gameTime)
        {
            UpdateKeysState(gameTime);
            if (IsMoving || MoveDamping > 0.0f)
            {
                Move(gameTime);
            }
            else
            {
                Direction = new Vector2();
            }
        }

        private void UpdateKeysState(GameTime gameTime)
        {
            float speedOffset = (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (QueryKeyState(EnableMoveTag.UpTag))
            {
                Direction += new Vector2(0.0f, -speedOffset);
            }
            if (QueryKeyState(EnableMoveTag.DownTag))
            {
                Direction += new Vector2(0.0f, speedOffset);
            }
            if (QueryKeyState(EnableMoveTag.LeftTag))
            {
                Direction += new Vector2(-speedOffset, 0.0f);
            }
            if (QueryKeyState(EnableMoveTag.RightTag))
            {
                Direction += new Vector2(speedOffset, 0.0f);
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawCircle(Position, Radius, 20, ColorProperty, Radius);
        }

        // IMoveAble
        public void Move(GameTime gameTime)
        {
            float timeDelta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Position += Direction * timeDelta * Speed * (IsMoving ? 1.0f : MoveDamping -= timeDelta);
        }

        public bool CanEat(CozyCircle circle)
        {
            if (circle == this) return false;
            Vector2 distanceVector = Position - circle.Position;
            float distance = distanceVector.Length();
            return Radius > (distance + circle.Radius);
        }

        // IControlAble
        public void OnKeyPressd(object sender, KeyboardEventArgs e)
        {
            switch (e.Key)
            {
                case Keys.W:
                    SetKeyState(EnableMoveTag.UpTag, true);
                    break;
                case Keys.S:
                    SetKeyState(EnableMoveTag.DownTag, true);
                    break;
                case Keys.A:
                    SetKeyState(EnableMoveTag.LeftTag, true);
                    break;
                case Keys.D:
                    SetKeyState(EnableMoveTag.RightTag, true);
                    break;
                default:
                    break;
            }
        }

        public void OnKeyResleased(object sender, KeyboardEventArgs e)
        {
            switch (e.Key)
            {
                case Keys.W:
                    SetKeyState(EnableMoveTag.UpTag, false);
                    MoveDamping = 1.0f;
                    break;
                case Keys.S:
                    SetKeyState(EnableMoveTag.DownTag, false);
                    MoveDamping = 1.0f;
                    break;
                case Keys.A:
                    SetKeyState(EnableMoveTag.LeftTag, false);
                    MoveDamping = 1.0f;
                    break;
                case Keys.D:
                    SetKeyState(EnableMoveTag.RightTag, false);
                    MoveDamping = 1.0f;
                    break;
                default:
                    break;
            }
        }

        private bool QueryKeyState(EnableMoveTag tag)
        {
            return MoveEnable[(int)tag];
        }

        private void SetKeyState(EnableMoveTag tag, bool state)
        {
            MoveEnable[(int)tag] = state;
        }
    }
}
