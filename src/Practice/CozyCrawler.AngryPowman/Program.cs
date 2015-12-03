using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyCrawler.Interface;
using CozyCrawler.Runner;

namespace CozyCrawler.AngryPowman
{
    public class Program
    {
        static void Main(string[] args)
        {
            RunnerIoCContainerSetting setting = new RunnerIoCContainerSetting();
            setting.RegisterType<IUrlGeneraterRunner, MultiUrlGeneraterRunner>();
            setting.RegisterType<IUrl2UrlRunner, BlockedUrl2UrlRunner>();
            setting.RegisterType<IUrl2ResultRunner, AsyncUrl2ResultRunner>();
            RunnerIoCContainer.Instance.Init(setting);

            // mail
            var reader = new ZhihuUrlReader("xxxxxxxx@xxx.com", "xxxxxx");

            var gen = RunnerIoCContainer.Instance.Resolve<IUrlGeneraterRunner>();
            var AskRunner = RunnerIoCContainer.Instance.Resolve<IUrl2UrlRunner>();
            var AnswerRunner = RunnerIoCContainer.Instance.Resolve<IUrl2UrlRunner>();
            var ResultRunner = RunnerIoCContainer.Instance.Resolve<IUrl2ResultRunner>();

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
