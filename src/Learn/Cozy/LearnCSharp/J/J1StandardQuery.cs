using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cozy.LearnCSharp.J
{
    class J1StandardQuery
    {
        public static void Cozy()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("-----------------------------------------------");

            ///> LINQ一览
            Details.StandardQuery.LINQQuery();
            Details.StandardQuery.ExtensionMethods();
            Details.StandardQuery.DeferredQuery();

            // 筛选
            // 用索引筛选
            // 类型筛选
            // 符合的from子句
            // 排序
            // 分组
            // 对嵌套的对象分组
            // 内连接
            // 左外连接
            // 组连接
            // 集合方法
            // 合并
            // 分区
            // 聚合操作符
            // 转换操作符
            // 生成操作符
        }
    }
}
