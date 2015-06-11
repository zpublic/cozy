using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CozyKxlol.Engine.Tiled.Impl;

namespace CozyKxlol.Engine.Tiled
{
    public class CozyTiledDataLoader : ICozyLoader
    {
        public void Load(CozyTiledData Data)
        {
            if(Data != null)
            {
                // read tiled data;
                TestData(Data);
            }
        }

        private void TestData(CozyTiledData Data)
        {
            Data.Modify(0, 0, 1);
            Data.Modify(1, 1, 2);
            Data.Modify(2, 2, 1);
        }
    }
}
