using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozySpider.Core.Event
{
    public class ErrorEventArgs : EventArgsBase
    {
        public string ErrorMessage { get; set; }

        public override string Message
        {
            get
            {
                return base.Message + " " + ErrorMessage;
            }
        }

        public ErrorEventArgs(string url, string Message)
            : base(url)
        {

        }
    }
}
