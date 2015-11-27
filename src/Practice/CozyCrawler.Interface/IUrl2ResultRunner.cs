using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyCrawler.Interface
{
    public interface IUrl2ResultRunner : IControllable, IUrlIn
    {
        void To(IUrl2Result to);
    }
}
