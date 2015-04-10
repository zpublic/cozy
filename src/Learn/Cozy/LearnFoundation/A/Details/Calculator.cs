using System;
using System.Threading;
using System.Threading.Tasks;

namespace Cozy.LearnFoundation.A
{
    public class Calculator
    {
        private ManualResetEventSlim mEvent;
        private CountdownEvent cEvent;

        public int Result { get; private set; }

        public Calculator(ManualResetEventSlim ev)
        {
            this.mEvent = ev;
        }
        public Calculator(CountdownEvent ev)
        {
            this.cEvent = ev;
        }

        public void Calculation(int x, int y)
        {
            Console.WriteLine("Task {0} starts calculation", Task.CurrentId);
            Thread.Sleep(new Random().Next(300));
            Result = x + y;

            // signal the event—completed!
            Console.WriteLine("Task {0} is ready", Task.CurrentId);
            mEvent.Set();
            // cEvent.Signal();
        }
    }

}
