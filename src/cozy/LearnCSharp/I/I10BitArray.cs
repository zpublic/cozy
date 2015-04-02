using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;

namespace cozy.LearnCSharp.I
{
    class I10BitArray
    {
        public static void Cozy()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("-----------------------------------------------");

            //BitArray
            //管理位值的压缩数组，该值表示为布尔值，其中 true 表示位是打开的 (1)，false 表示位是关闭的 (0)。            

            var bitArray = new BitArray(new byte[] {1, 2, 3, 4, 5, 6, 7, 8, 9});
            Console.WriteLine("Count：{0}",bitArray.Count);
            Console.WriteLine("Length:{0}",bitArray.Length);

            PrintValues(bitArray, 8);
        }

        /// <summary>
        /// 打印值
        /// </summary>
        /// <param name="myList">BitArray集合</param>
        /// <param name="myWidth">指定每行显示的个数</param>
        static void PrintValues(IEnumerable myList, int myWidth)
        {
            var i = myWidth;
            foreach (var obj in myList)  //迭代一列数
            {
                if (i <= 0) {
                    i = myWidth;
                    Console.WriteLine();
                }
                i--;
                Console.Write("{0,7}", obj);//显示第0个数据obj，占7个符号的位置
            }
            Console.WriteLine();
        }
    }
}
