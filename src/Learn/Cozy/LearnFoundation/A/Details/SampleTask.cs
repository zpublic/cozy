using System;
using System.Diagnostics;
using System.Threading;

namespace Cozy.LearnFoundation.A
{
    public class StateObject
    {
        private int state = 5;
        private object sync = new object();

        public void ChangeState(int loop)
        {
//            lock (sync)
            {
                if (state == 5)
                {
                    state++;
                    //Trace.Assert(state == 6, "Race condition occurred after " + loop + " loops");
                }
                state = 5;
            }
        }
    }

    public class SampleTask
    {
        //internal static int a;
        //private static Object sync = new object();

        public SampleTask()
        {

        }

        public void RaceCondition(object o)
        {
            Trace.Assert(o is StateObject, "o must be of type StateObject");
            StateObject state = o as StateObject;

            int i = 0;
            while (true)
            {
                // lock (state) // no race condition with this lock
                {
                    state.ChangeState(i++);
                }
            }

        }

        public SampleTask(StateObject s1, StateObject s2)
        {
            this.s1 = s1;
            this.s2 = s2;
        }

        StateObject s1;
        StateObject s2;


        public void Deadlock1()
        {
            int i = 0;
            while (true)
            {
                lock (s1)
                {
                    lock (s2)
                    {
                        s1.ChangeState(i);
                        s2.ChangeState(i++);
                        Console.WriteLine("still running, {0}", i);
                    }
                }
            }

        }

        public void Deadlock2()
        {
            int i = 0;
            while (true)
            {
                lock (s2)
                {
                    lock (s1)
                    {
                        s1.ChangeState(i);
                        s2.ChangeState(i++);
                        Console.WriteLine("still running, {0}", i);
                    }
                }
            }
        }

    }
}
