using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace QiHe.CodeLib.Net.Dns
{
    public enum QueryType
    {
        /// <summary>
        /// a host address
        /// </summary>
        Address = 1,

        /// <summary>
        /// an authoritative name server
        /// </summary>
        NameServer = 2,

        //	MD    = 3,  a mail destination (Obsolete - use MX)
        //	MF    = 5,  a mail forwarder (Obsolete - use MX)

        /// <summary>
        /// the canonical name for an alias
        /// </summary>
        CanonicalName = 5,

        /// <summary>
        /// marks the start of a zone of authority
        /// </summary>
        StartOfAuthorityZone = 6,

        //	MB    = 7,  a mailbox domain name (EXPERIMENTAL)
        //	MG    = 8,  a mail group member (EXPERIMENTAL)
        //  MR    = 9,  a mail rename domain name (EXPERIMENTAL)
        //	NULL  = 10, a null RR (EXPERIMENTAL)

        /// <summary>
        /// a well known service description
        /// </summary>
        WellKnownService = 11,

        /// <summary>
        /// PTR - a domain name pointer
        /// </summary>
        Pointer = 12,

        /// <summary>
        /// HINFO - host information
        /// </summary>
        HostInfo = 13,

        /// <summary>
        /// MINFO - mailbox or mail list information
        /// </summary>
        MailInfo = 14,

        /// <summary>
        /// MX - mail exchange
        /// </summary>
        MailExchange = 15,

        /// <summary>
        /// TXT - text strings
        /// </summary>
        Text = 16,

        /// <summary>
        /// UnKnown
        /// </summary>
        UnKnown = 9999,
    }

    public enum QueryClass
    {
        /// <summary>
        /// the Internet system
        /// </summary>
        Internet = 1,

        //CSNET = 2, //the CSNET class (Obsolete - used only for examples in some obsolete RFCs)

        /// <summary>
        ///  the Chaos system
        /// </summary>
        Chaos = 3,

        /// <summary>
        ///  Hesiod [Dyer 87]
        /// </summary>
        Hesiod = 4,
    }

    public class DnsQuery
    {
        public string DomainName;

        public QueryType QueryType;

        public QueryClass QueryClass;

        public DnsQuery(string domainName, QueryType qtype)
        {
            DomainName = domainName;
            QueryType = qtype;
            QueryClass = QueryClass.Internet;
        }

        public DnsQuery(string domainName, QueryType qtype, QueryClass qclass)
        {
            DomainName = domainName;
            QueryType = qtype;
            QueryClass = qclass;
        }
    }
}
