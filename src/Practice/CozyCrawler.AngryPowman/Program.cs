using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyCrawler.Interface;
using CozyCrawler.Runner;

namespace CozyCrawler.AngryPowman
{
    class Program
    {
        static void Main(string[] args)
        {
            // mail
            var reader = new ZhihuUrlReader("xxxxxx@xxx.com", "xxxxxx");

            IUrlGeneraterRunner gen = new MultiUrlGeneraterRunner();
            IUrl2UrlRunner AskRunner = new BlockedUrl2UrlRunner();
            IUrl2UrlRunner AnswerRunner = new BlockedUrl2UrlRunner();
            IUrl2ResultRunner ResultRunner = new AsyncUrl2ResultRunner();

            gen.From(new ZhihuUrlGenerater("AngryPowman"));
            gen.To(AskRunner);
            gen.To(AnswerRunner);
            AskRunner.To(ResultRunner);
            AnswerRunner.To(ResultRunner);
            ResultRunner.To(new ZhihuUrl2Result());

            AskRunner.SetProcessor(new ZhihuAskUrl2Url("AngryPowman", 4));
            AnswerRunner.SetProcessor(new ZhihuAnswerUrl2Url("AngryPowman", 4));

            ResultRunner.Start();
            AskRunner.Start();
            AnswerRunner.Start();
            gen.Start();

            Console.ReadKey();
        }
    }
}
