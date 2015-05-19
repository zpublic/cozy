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
using CozyKxlol.Kxlol.Extends;

namespace CozyKxlol.Kxlol.Object
{
    class CozyCircle : CozyNode, IMoveAble, IControlAble
    {
        #region Property

        public Vector2 Position { get; set; }
        
        public Color ColorProperty { get; set; }

        private float _Radius = 0.0f;
        public float Radius 
        { 
            get
            {
                return _Radius;
            }
            set
            {
                _Radius     = value;
                MaxSpeed    = BaseSpeed + FloatSpeed / Radius;
            }
        }

        private bool _Changed = false;
        public bool Changed
        {
            get
            {
                return _Changed;
            }
            set
            {
                _Changed = value;
            }
        }

        #region Move

        enum EnableMoveTag : int
        {
            LeftTag,
            RightTag,
            UpTag,
            DownTag,
        }

        // IMoveAble

        private Vector2 _Speed = Vector2.Zero;
        public Vector2 Speed
        {
            get
            {
                return _Speed;
            }
            set
            {
                _Speed = value.ClampWithLength(MaxSpeed);
            }
        }

        // v = 20 + 200 / S
        public const float BaseSpeed    = 20.0f;
        public const float FloatSpeed   = 200.0f;

        #region Direction

        private static Vector2 _Up = Vector2.Zero;
        public static Vector2 Up
        {
            get
            {
                return _Up;
            }

            private set
            {
                _Up = value;
            }
        }

        private static Vector2 _Down = Vector2.Zero;
        public static Vector2 Down
        {
            get
            {
                return _Down;
            }
            private set
            {
                _Down = value;
            }
        }

        private static Vector2 _Left = Vector2.Zero;
        public static Vector2 Left
        {
            get
            {
                return _Left;
            }
            private set
            {
                _Left = value;
            }
        }

        private static Vector2 _Right = Vector2.Zero;
        public static Vector2 Right
        {
            get
            {
                return _Right;
            }
            private set
            {
                _Right = value;
            }
        }

        #endregion

        private float _MaxSpeed = 0.0f;
        public float MaxSpeed
        {
            get
            {
                return _MaxSpeed;
            }
            private set
            {
                _MaxSpeed   = value;
                Up          = new Vector2(0.0f, -MaxSpeed);
                Down        = new Vector2(0.0f, MaxSpeed);
                Left        = new Vector2(-MaxSpeed, 0.0f);
                Right       = new Vector2(MaxSpeed, 0.0f);
            }
        }

        private float _LinearDamping;
        public float LinearDamping
        {
            get
            {
                return _LinearDamping;
            }
            set
            {
                _LinearDamping = MathHelper.Clamp(value, 0.0f, 1.0f);
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
            ColorProperty   = Color.Black;
        }

        public CozyCircle(Vector2 pos, float radius, Color color)
        {
            Position        = pos;
            Radius          = radius;
            ColorProperty   = color;
        }

        public CozyCircle(Vector2 pos, float radius, Color color, float borderSize)
            : this(pos, radius, color)
        {
            HasBorder       = true;
            BorderSize      = borderSize;
        }

        public override void Update(GameTime gameTime)
        {
            UpdateKeysState(gameTime);

            float timeDelta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (IsMoving || LinearDamping > 0.0f)
            {
                Move(gameTime);
            }
            else
            {
                Speed = Vector2.Zero;
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (HasBorder)
            {
                spriteBatch.DrawCircle(Position, Radius + BorderSize, (int)Radius, Color.Black, BorderSize);
            }
            spriteBatch.DrawCircle(Position, Radius, (int)Radius, ColorProperty, Radius);
        }

        // IMoveAble
        public void Move(GameTime gameTime)
        {
            float timeDelta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Position += timeDelta * LinearDamping * Speed;
            if(IsMoving)
            {
                LinearDamping += MathHelper.Lerp(0.0f, 1.0f, timeDelta);
            }
            else
            {
                LinearDamping += MathHelper.Lerp(0.0f, -1.0f, timeDelta);
            }
            Changed = true;
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
                Speed += Vector2.Lerp(Vector2.Zero, Up, speedOffset);
            }
            else if (QueryKeyState(EnableMoveTag.DownTag))
            {
                Speed += Vector2.Lerp(Vector2.Zero, Down, speedOffset);
            }
            if (QueryKeyState(EnableMoveTag.LeftTag))
            {
                Speed += Vector2.Lerp(Vector2.Zero, Left, speedOffset);
            }
            else if (QueryKeyState(EnableMoveTag.RightTag))
            {
                Speed += Vector2.Lerp(Vector2.Zero, Right, speedOffset);
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

        public void OnKeyReleased(object sender, KeyboardEventArgs e)
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
