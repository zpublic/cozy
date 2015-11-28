using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyCrawler.Core;
using CozyCrawler.Core.Url2Url;
using CozyCrawler.Interface;

namespace CozyCrawler.AngryPowman
{
    class Program
    {
        static void Main(string[] args)
        {
            IUrlGeneraterRunner gen = new SingleThreadMultSourceMultTargetUrlGeneraterRunner();
            IUrl2UrlRunner AskRunner = new BlockedUrl2UrlRunner();
            IUrl2UrlRunner AnswerRunner = new BlockedUrl2UrlRunner();
            IUrl2ResultRunner ResultRunner = new AsyncUrl2ResultRunner();

            gen.From(new ZhihuUrlGenerater("zapline"));
            gen.To(AskRunner);
            gen.To(AnswerRunner);
            AskRunner.To(ResultRunner);
            AnswerRunner.To(ResultRunner);

            var url = new ZhihuAskUrl2Url("zapline", 4);
            url.Start();
            AskRunner.SetProcessor(url);

            var url1 = new ZhihuAnswerUrl2Url("zapline", 4);
            url1.Start();
            AnswerRunner.SetProcessor(url1);

            ResultRunner.To(new ZhihuUrl2Result());

            ResultRunner.Start();
            AskRunner.Start();
            AnswerRunner.Start();
            gen.Start();

            Console.ReadKey();
        }
    }
}
