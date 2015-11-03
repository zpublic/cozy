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
            string name = Console.ReadLine();
            int time = int.Parse(Console.ReadLine());
            BoredApi.Save(name, time);
            BoredApi.GetRank(time);
            BoredApi.QueryRank();
            Console.WriteLine(name + ":" + time);
        }
    }
}
