using CozyPoker.Engine.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyPoker.Engine.Method
{
    public interface DealMethod
    {
        CardCollect Run(CardCollect cc);
    }
}
