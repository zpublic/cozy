using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CozyKxlol.Network.Msg
{
    public static class MsgId
    {
        ////// 0 - 10000 保留
        public const int Zero = 0;

        ////// 10001 - 20000 游戏相关
        //// 10101 - 10200 帐号相关
        // 注册
        public const int AccountReg = 10101;
        // 登陆
        public const int AccountLogin = 10102;
        // 修改密码
        public const int AccountModifyPassword = 10103;
        // 注册结果
        public const int AccountRegRsp = 10104;
        // 登陆结果
        public const int AccountLoginRsp = 10105;
        // 修改密码结果
        public const int AccountModifyPasswordRsp = 10106;

        //// 11001 - 11100 聊天
        // 发送消息给所有人
        public const int ChatToAll = 11001;
        // 发送消息给指定用户
        public const int ChatToPlayer = 11002;
        // 你发送的用户不存在
        public const int ChatNotFindPlayer = 11003;

        //// 11101 - 11200 agar.io
        // 玩家小球信息同步
        public const int AgarSync = 11101;
        // 小球被吃了
        public const int AgarDied = 11102;
        // 可被吃小球产生
        public const int AgarNpc = 11103;

        // 用户登陆
        public const int AgarLogin = 11104;
        // 小球本身事件
        public const int AgarSelf = 11105;
        // 更新位置颜色和半径
        public const int AgarPlayInfo = 11106;
        // 可被吃小球产生
        public const int AgarFixedBall = 11107;
        // 返回自己的位置
        public const int AgarLoginRsp = 11108;
        // 可被吃小球打包发送
        public const int AgarFixBallPack = 11109;
        // 打包发送玩家信息
        public const int AgarPlayInfoPack = 11110;
        // 玩家出生信息
        public const int AgarBorn = 11111;
        // 分数排行
        public const int AgarMarkListPark = 11112;

        ////// 20001 - 40000 游戏扩展

        ////// 40001+ 保留
    }
}
