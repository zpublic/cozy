using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyAdventure.Protocol
{
    public static class MessageId
    {
        // - 00001-10000 预留
        // - 00001-00100 核心消息
        public enum Core : uint
        {
            HeartMessage = 00001,
        }

        // - 10001-20000 内部
        public enum Inner : uint
        {
            RegisterMessage = 10001,
            RegisterResultMessage,
            LoginMessage,
            LoginResultMessage,
        }

        // - 10101-10200 角色信息同步
        public enum User : uint
        {
            PullMessage = 10101,
            PushMessage,
        }

        // - 10201-10300 冒险farm
        public enum Farm : uint
        {
            GotoMapMessage = 10201,
            GotoHomeMessage,
            GotoResultMessage,
            FarmIncomeMessage,
        }

        // - 10301-10400 佣兵相关
        public enum Mercenary : uint
        {
            UpgradeMessage = 10301,
            UpgradeResultMessage,
        }

        // - 10401-10500 礼物和彩蛋
        public enum Gift : uint
        {
            GetGiftMessage = 10401,
            GetGiftResultMessage,
        }
    }
}
