using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CozyKxlol.Kxlol.Interface
{
    public enum MoveDirection : int 
    {
        Left,
        Right,
        Up,
        Down,
        LeftUp,
        LeftDown,
        RightUp,
        RightDown,
    }
    public interface ITileMoveable
    {
        void Move(MoveDirection dire);
    }
}
