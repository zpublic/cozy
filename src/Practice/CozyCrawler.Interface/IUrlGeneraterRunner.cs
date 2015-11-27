using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyCrawler.Interface
{
    public interface IUrlGeneraterRunner : IControllable, IUrlIn ,IUrlOut
    {
        void From(IUrlGenerater i);
    }
}
