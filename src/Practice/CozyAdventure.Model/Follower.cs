using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyAdventure.Model
{
    public class Follower
    {
        public int Id { get; set; }

        // 名字
        public string Name { get; set; }

        // 描述
        public string Desc { get; set; }

        // 基础战力
        public int BasicAttack { get; set; } = 1;

        // 成长细数
        public float GrowRatio { get; set; } = 1.0f;

        // 星级
        public int MaxStar { get; set; } = 0;
        public int CurStar { get; set; } = 0;

        // 等级
        public int CurLevel { get; set; } = 0;

        // 阶层
        public int CurRank { get; set; } = 0;

        // 头像
        public string Avatar { get; set; }

        /// <summary>
        /// 是否在战斗中
        /// </summary>
        public bool IsFighting { get; set; }

        /// <summary>
        /// 玩家的每一个随从都有一个ID
        /// </summary>
        public int ObjectId { get; set; }
    }
}
