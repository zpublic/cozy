using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyMail.Core
{
    public class CozyEmailSender
    {
        public static bool Send(string from, string to, string subject, string body)
        {
            return QiHe.CodeLib.Net.EmailSender.Send(from, to, subject, body);
        }
    }
}
