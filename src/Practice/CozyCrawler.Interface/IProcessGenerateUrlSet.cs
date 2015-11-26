using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyCrawler.Interface
{
    public interface IProcessGenerateUrlSet : IControllable, IUrlSetIn ,IUrlSetOut
    {
        void From(IGenerater i);
    }
}
