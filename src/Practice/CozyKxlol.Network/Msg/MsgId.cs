using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CozyKxlol.Network.Msg
{
    public static class MsgId
    {
        // 0 - 10000 保留

        // 10001 - 20000 游戏相关
        // 10001 - 10100 聊天
        // 发送消息给所有人
        public const int ChatToAll = 10001;
        // 发送消息给指定用户
        public const int ChatToPlayer = 10002;
        // 你发送的用户不存在
        public const int ChatNotFindPlayer = 10003;
        // 10101 - 10200 agar.io
        // 玩家小球信息同步
        public const int AgarSync = 10101;
        // 小球被吃了
        public const int AgarDied = 10102;
        // 可被吃小球产生
        public const int AgarNpc = 10103;

        // 20001 - 40000 游戏扩展
        
        // 40001+ 保留
    }
}
