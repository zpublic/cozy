using System;
using System.Collections.Generic;
using System.Text;

namespace QiHe.CodeLib.Net.Dns
{
    public class MailExchange
    {
        /// <summary>
        /// The preference given to this RR among others at the same owner.
        /// Lower values are preferred.
        /// </summary>
        public UInt16 Preference;

        /// <summary>
        /// a host willing to act as a mail exchange for the owner name.
        /// </summary>
        public string HostName;
    }
}
