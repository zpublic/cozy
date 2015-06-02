using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CozyKxlol.Engine
{
    public class CozyCallFunc : CozyActionInstant
    {
        public delegate void Func();

        public Func CallBackFunc
        {
            get;
            set;
        }

        public static CozyCallFunc Create(Func func)
        {
            var act             = new CozyCallFunc();
            act.CallBackFunc    = func;
            return act;
        }

        public void Execute()
        {
            if(CallBackFunc != null)
            {
                CallBackFunc();
            }
        }

        public override void Update(float dt)
        {
            Execute();
        }
    }
}
