using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyCrawler.Interface
{
    public interface IUrl2UrlRunner : IControllable, IUrlIn, IUrlOut
    {
        void SetProcessor(IUrl2Url p);
    }
}
