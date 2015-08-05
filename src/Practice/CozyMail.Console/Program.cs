using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyMail.Core;

namespace CozyMail.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            CozyEmailSender.Send("zapline@laorouji.com", "805037171@163.com", "nihao", "hehe");
            CozyEmailSender.Send("zapline@laorouji.com", "952510146@qq.com", "nihao", "hehe");
            CozyEmailSender.Send("zapline@laorouji.com", "278998871@qq.com", "nihao", "hehe");
            CozyEmailSender.Send("zapline@laorouji.com", "zhuxianzhang@ijinshan.com", "nihao", "hehe");
            CozyEmailSender.Send("zapline@laorouji.com", "zhuxianzhang@cmcm.com", "nihao", "hehe");
            CozyEmailSender.Send("zapline@laorouji.com", "zhuxianzhang@kingsoft.com", "nihao", "hehe");
        }
    }
}
