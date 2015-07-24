using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CozyKxlol.Kxlol.Interface
{
    interface IMoveAble
    {
        Vector2 Speed
        {
            get;
            set;
        }

        bool IsMoving
        {
            get;
        }

        void Move(GameTime gameTime);
    }
}
