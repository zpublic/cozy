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

        public Vector2 Position { get; set; }
        public float Radius { get; set; }
        public Color ColorProperty { get; set; }

        #region Move

        enum EnableMoveTag : int
        {
            LeftTag,
            RightTag,
            UpTag,
            DownTag,
        }

        // IMoveAble
        private float _MoveDamping = 0.0f;
        public float MoveDamping
        {
            get
            {
                return _MoveDamping;
            }
            set
            {
                _MoveDamping = (value > 1.0f ? 1.0f : (value < 0.0f ? 0.0f : value));
            }
        }

        private Vector2 _Direction = new Vector2();
        public Vector2 Direction
        {
            get
            {
                return _Direction;
            }
            set
            {
                _Direction = value;
                if(_Direction != Vector2.Zero)
                    _Direction.Normalize();
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

        #region Border
        private bool _HasBorder = false;
        public bool HasBorder
        {
            get
            {
                return _HasBorder;
            }
            set
            {
                _HasBorder = value;
            }
        }
        private float _BorderSize = 0.0f;
        public float BorderSize
        {
            get
            {
                return _BorderSize;
            }
            set
            {
                _BorderSize = value;
            }
        }
        #endregion

        #endregion

        public CozyCircle()
        {
            Position        = Vector2.Zero;
            Radius          = 0.0f;
            ColorProperty   = Color.Black;
            Direction       = Vector2.Zero;
        }

        public CozyCircle(Vector2 pos, float radius, Color color)
        {
            Position        = pos;
            Radius          = radius;
            ColorProperty   = color;
            Direction       = Vector2.Zero;
        }
        public CozyCircle(Vector2 pos, float radius, Color color, Vector2 dire)
            : this(pos, radius, color)
        {
            Direction       = dire;
        }

        public CozyCircle(Vector2 pos, float radius, Color color, Vector2 dire, float borderSize)
            :this(pos, radius, color, dire)
        {
            HasBorder = true;
            BorderSize = borderSize;
        }

        static Random RandomMaker = new Random();
        public static Color RandomColor()
        {
            ushort color = (ushort)RandomMaker.Next(0xffffff);
            return new Color(color & 0xFF0000, color & 0x00FF00, color & 0x0000FF);
        }

        public static Vector2 RandomPosition()
        {
            Point MaxSize = CozyDirector.Instance.WindowSize;
            return new Vector2(RandomMaker.Next(MaxSize.X), RandomMaker.Next(MaxSize.Y));
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
                Direction = Vector2.Zero;
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if(HasBorder)
            {
                spriteBatch.DrawCircle(Position, Radius + BorderSize, (int)Radius, Color.Black, BorderSize);
            }
            spriteBatch.DrawCircle(Position, Radius, (int)Radius, ColorProperty, Radius);
        }

        // IMoveAble
        public void Move(GameTime gameTime)
        {
            float timeDelta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Position += Direction * timeDelta * Speed * MoveDamping;
            if(IsMoving)
            {
                MoveDamping += timeDelta;
            }
            else
            {
                MoveDamping -= timeDelta;
            }
        }

        public bool CanEat(CozyCircle circle)
        {
            if (circle == this) return false;
            Vector2 distanceVector = Position - circle.Position;
            float distance = distanceVector.Length();
            return Radius > (distance + circle.Radius);
        }

        #region KeyEvent

        private void UpdateKeysState(GameTime gameTime)
        {
            float speedOffset = (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (QueryKeyState(EnableMoveTag.UpTag))
            {
                Direction -= Vector2.Lerp(Vector2.Zero, Vector2.UnitY, speedOffset);
            }
            else if (QueryKeyState(EnableMoveTag.DownTag))
            {
                Direction += Vector2.Lerp(Vector2.Zero, Vector2.UnitY, speedOffset);
            }
            if (QueryKeyState(EnableMoveTag.LeftTag))
            {
                Direction -= Vector2.Lerp(Vector2.Zero, Vector2.UnitX, speedOffset);
            }
            else if (QueryKeyState(EnableMoveTag.RightTag))
            {
                Direction += Vector2.Lerp(Vector2.Zero, Vector2.UnitX, speedOffset);
            }
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
                    break;
                case Keys.S:
                    SetKeyState(EnableMoveTag.DownTag, false);
                    break;
                case Keys.A:
                    SetKeyState(EnableMoveTag.LeftTag, false);
                    break;
                case Keys.D:
                    SetKeyState(EnableMoveTag.RightTag, false);
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

        #endregion
    }
}
