using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CozyKxlol.Kxlol.Impl
{
    interface IMoveAble
    {
        Vector2 Direction
        {
            get;
            set;
        }

        bool IsMoving
        {
            get;
            set;
        }

        void Move(GameTime gameTime);
    }
}
