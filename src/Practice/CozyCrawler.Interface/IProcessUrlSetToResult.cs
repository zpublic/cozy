using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyCrawler.Interface
{
    public interface IProcessUrlSetToResult : IControllable, IUrlSetIn
    {
        void To(IResultGetter to);
    }
}
