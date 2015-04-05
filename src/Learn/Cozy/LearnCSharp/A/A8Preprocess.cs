
// 定义Debug
#define DEBUG

// 删除定义Debug
#undef DEBUG

// 定义noDebug
#define NODEBUG

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cozy.LearnCSharp.A
{
    class A8Preprocess
    {
        public static void Cozy()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("-----------------------------------------------");

            Define();
            Warning_and_error();
            Region_And_Endregion();

        }

        public static void Define()
        {

            // 判断定义
#if DEBUG
            Console.WriteLine("Define Debug");
#elif NODEBUG
            Console.WriteLine("Define noDebug");
#else
            Console.WriteLine("noDefine");
#endif
        }

        public static void Warning_and_error()
        {
            // 警告定义和错误定义

//#warning "define warning";        // 编译时产生警告
//#error "define error";            // 编译时产生错误

        }

        public static void Region_And_Endregion()
        {
            // 帮助布局

            #region WRITE_LINE
            Console.WriteLine("region");
            #endregion

        }
    }
}
