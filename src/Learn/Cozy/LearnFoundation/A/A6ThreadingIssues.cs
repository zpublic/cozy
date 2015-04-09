using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cozy.LearnFoundation.A
{
    class A6ThreadingIssues
    {
        public static void Cozy()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("-----------------------------------------------");

            RaceConditions();
            //Deadlock();
        }

        static void RaceConditions()
        {
            var state = new StateObject();
            for (int i = 0; i < 2; i++)
            {
                Task.Run(() => new SampleTask().RaceCondition(state));
            }
        }

        static void Deadlock()
        {
            var s1 = new StateObject();
            var s2 = new StateObject();
            Task.Run(() => new SampleTask(s1, s2).Deadlock1());
            Task.Run(() => new SampleTask(s1, s2).Deadlock2());

            Thread.Sleep(100000);
        }
    }
}
