using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Mail;

namespace QiHe.CodeLib.Net
{
    public class EmailSender
    {
        /// <summary>
        /// Default SMTP Port.
        /// </summary>
        public static int SmtpPort = 25;

        public static bool Send(string from, string to, string subject, string body)
        {
            string domainName = GetDomainName(to);
            IPAddress[] servers = GetMailExchangeServer(domainName);
            foreach (IPAddress server in servers)
            {
                try
                {
                    SmtpClient client = new SmtpClient(server.ToString(), SmtpPort);
                    client.Send(from, to, subject, body);
                    return true;
                }
                catch
                {
                    continue;
                }
            }
            return false;
        }

        public static bool Send(MailMessage mailMessage)
        {
            string domainName = GetDomainName(mailMessage.To[0].Address);
            IPAddress[] servers = GetMailExchangeServer(domainName);
            foreach (IPAddress server in servers)
            {
                try
                {
                    SmtpClient client = new SmtpClient(server.ToString(), SmtpPort);
                    client.Send(mailMessage);
                    return true;
                }
                catch
                {
                    continue;
                }
            }
            return false;
        }

        public static string GetDomainName(string emailAddress)
        {
            int atIndex = emailAddress.IndexOf('@');
            if (atIndex == -1)
            {
                throw new ArgumentException("Not a valid email address", "emailAddress");
            }
            if (emailAddress.IndexOf('<') > -1 && emailAddress.IndexOf('>') > -1)
            {
                return emailAddress.Substring(atIndex + 1, emailAddress.IndexOf('>') - atIndex);
            }
            else
            {
                return emailAddress.Substring(atIndex + 1);
            }
        }

        public static IPAddress[] GetMailExchangeServer(string domainName)
        {
            IPHostEntry hostEntry = DomainNameUtil.GetIPHostEntryForMailExchange(domainName);
            if (hostEntry.AddressList.Length > 0)
            {
                return hostEntry.AddressList;
            }
            else if (hostEntry.Aliases.Length > 0)
            {
                return System.Net.Dns.GetHostAddresses(hostEntry.Aliases[0]);
            }
            else
            {
                return null;
            }
        }
    }
}
