using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyCrawler.AngryPowman
{
    public class LoginResult
    {
        public int r { get; set; }

        public int errcode { get; set; }

        public KeyValuePair<string, string> data { get; set; }

        public string msg { get; set; }
    }
}
