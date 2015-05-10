using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CozyKxlol.Network.Common.Msg
{
    public static class MsgId
    {
        // 0 - 10000 保留

        // 10001 - 20000 游戏相关
        // 10001 - 10100 聊天
        public const int ChatToAll = 10001;
        public const int ChatToPlayer = 10002;
        public const int ChatNotFindPlayer = 10003;

        // 20001 - 40000 游戏扩展
        
        // 40001+ 保留
    }
}
