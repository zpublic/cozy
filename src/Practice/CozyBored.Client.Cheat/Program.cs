using CozyBored.Client.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyBored.Client.Cheat
{
    class Program
    {
        static void Main(string[] args)
        {
            string aname = Console.ReadLine();
            double atime = double.Parse(Console.ReadLine());
            BoredApi.Save(new Core.Model.BoredModel
            {
                name = aname,
                time = atime
            });
            BoredApi.QueryRank();
            BoredApi.GetRank(1);
            Console.WriteLine(aname + ":" + atime);
        }
    }
}
