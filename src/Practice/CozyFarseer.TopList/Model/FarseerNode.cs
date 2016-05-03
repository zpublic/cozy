namespace CozyFarseer.TopList.Model
{
    public class FarseerNode
    {
        /// <summary>
        /// 预言PID
        /// </summary>
        public int pid { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        public int uid { get; set; }

        /// <summary>
        /// 用户昵称
        /// </summary>
        public string nick { get; set; }

        /// <summary>
        /// 成功率
        /// </summary>
        public double accuracy { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string avatar { get; set; }

        /// <summary>
        /// 已弃用
        /// </summary>
        public int tag { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string content { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        public int time { get; set; }

        /// <summary>
        /// 赞数
        /// </summary>
        public int likes { get; set; }

        /// <summary>
        /// 股票代码
        /// </summary>
        public string stock { get; set; }

        /// <summary>
        /// 股票名称
        /// </summary>
        public string sname { get; set; }

        /// <summary>
        /// 目标过期时间
        /// </summary>
        public int expires { get; set; }

        /// <summary>
        /// 过期倒计时
        /// </summary>
        public int countdown { get; set; }

        /// <summary>
        /// 目标类型  0具体看涨 1具体看跌 3纯看涨 4纯看跌
        /// </summary>
        public int ttype { get; set; }

        /// <summary>
        /// 预言价格
        /// </summary>
        public double tvalue { get; set; }

        /// <summary>
        /// 预言状态 0 进行中 1 成功 2 失败
        /// </summary>
        public int tstatus { get; set; }
    }
}
