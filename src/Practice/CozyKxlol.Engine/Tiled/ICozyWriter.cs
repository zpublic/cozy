using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CozyKxlol.Engine.Tiled
{
    public interface ICozyWriter
    {
        void Write(Stream WriteStream);
    }
}
