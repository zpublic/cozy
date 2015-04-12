using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CozySql.Configure
{
    // 最后打开的接口
    interface ILastOpen
    {
        // 添加一个信息
        void Add(SqlInfo info);

        // 查看某个信息是否在容器中
        bool Exists(SqlInfo info);

        // 删除某个信息
        bool Remove(SqlInfo info);

        // 按索引删除某个信息
        void RemoveAt(int index);

        // 按索引获取某个信息
        SqlInfo GetElemt(int index);

        // 将某个信息放在最前方
        void UpdateToTop(int index);

        // 最大保存的条数
        int MaxSize
        {
            get;
            set;
        }
    }

    // 实现接口
    class LastOpen : ILastOpen
    {
        public void Add(SqlInfo info)
        {
            if(LastOpenList.Count > MaxSize)
            {
                LastOpenList.RemoveAt(LastOpenList.Count - 1);
            }
            LastOpenList.Add(info);
        }

        public bool Remove(SqlInfo info)
        {
            return LastOpenList.Remove(info);
        }

        public void RemoveAt(int index)
        {
            LastOpenList.RemoveAt(index);
        }

        public bool Exists(SqlInfo info)
        {
            return LastOpenList.Contains(info);
        }

        public SqlInfo GetElemt(int index)
        {
            return LastOpenList[index];
        }

        public void UpdateToTop(int index)
        {
            SqlInfo topInfo = LastOpenList[index];
            LastOpenList.RemoveAt(index);
            LastOpenList.Insert(0, topInfo);
        }

        private int maxsize;
        public int MaxSize
        {
            get
            {
                return maxsize;
            }
            set
            {
                // 当maxsize 小于等于0 设置为1 
                if(maxsize <= 0)
                {
                    maxsize = 1;
                }
                else
                {
                    maxsize = value;
                }
            }
        }

        // 容器
        private List<SqlInfo> LastOpenList = new List<SqlInfo>();
    }
}