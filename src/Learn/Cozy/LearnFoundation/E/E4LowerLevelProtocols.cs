using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;

namespace Cozy.LearnFoundation.E
{
    class E4LowerLevelProtocols
    {
        public static void Cozy()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("-----------------------------------------------");

            SmtpClient_Class();
            Tcp_and_Udp_Class();
        }

        public static void SmtpClient_Class()
        {
            // 定义邮件内容
            MailMessage mm = new MailMessage();
            mm.From = new MailAddress("FromAddress@163.com", "From");
            mm.To.Add(new MailAddress("ToAddress@163.com", "To"));
            mm.CC.Add(new MailAddress("CCAddress@163.com", "CC"));
            mm.Subject = "Test";
            mm.Body = "<b>Hello World</b>";
            mm.IsBodyHtml = true;
            mm.Priority = MailPriority.High;

            // 发送邮件消息
             SmtpClient sc = new SmtpClient();
            //sc.Credentials = new System.Net.NetworkCredential("username","password");
            sc.Host = @"smtp.qq.com";
            //sc.Send(mm);
        }

        public static void Tcp_and_Udp_Class()
        {
            Console.WriteLine("未实现 Tcp和Udp部分");
        }
    }
}
