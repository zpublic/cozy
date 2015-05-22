using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CozyKxlol.Network.Msg
{
    public static class GameMessageHelper
    {

        public const uint NONE_TAG = 0x00000000U;
        public const uint POSITION_TAG = 0x00000001U;
        public const uint RADIUS_TAG = 0x00000002U;
        public const uint COLOR_TAG = 0x00000004U;
        public const uint NAME_TAG = 0x00000008U;
        public const uint ALL_TAG = 0xFFFFFFFFU;

        public static bool Is_Changed(uint source, uint mask)
        {
            return (source & mask) != 0;
        }
    }
}
