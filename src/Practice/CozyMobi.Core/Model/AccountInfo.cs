using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CozyMobi.Core.Model
{
    class AccountInfo
    {
        private static AccountInfo inst = new AccountInfo();

        public static AccountInfo Instance
        {
            get
            { 
                return inst;
            }
        }

        public String Cookie { set; get; }
    }
}
