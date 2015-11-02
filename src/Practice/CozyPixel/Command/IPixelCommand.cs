using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyPixel.Command
{
    public interface IPixelCommand
    {
        void Do();
        void Undo();
    }
}
