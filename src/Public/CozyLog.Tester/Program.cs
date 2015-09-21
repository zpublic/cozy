using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyLog.Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            Log.InitializeWith<Log4NetLog>();

            "Main".Log().Info(() => "This is a logging message");

            Foo foo = new Foo();
            foo.Bar();
        }

        public class Foo
        {
            public void Bar()
            {
                this.Log().Error(() => "This is a logging message from the Bar method of Foo");
            }
        }
    }
}
