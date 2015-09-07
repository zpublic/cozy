using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyPoker.Engine.Model;

namespace CozyPoker.Engine.Method
{
    public class CardCollectMethod_Lua : CardCollectMethod
    {
        private string script_;
        public CardCollectMethod_Lua(string script)
        {
            script_ = script;
        }

        public CardCollect Run()
        {
            CardCollect cc = new CardCollect();
            return cc;
        }
    }
}
