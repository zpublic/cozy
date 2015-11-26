using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyCrawler.Interface
{
    public enum ControllableEvent
    {
        begin,
        end,
    }

    public interface IControllable
    {
        void Start();
        void OnEvent(ControllableEvent ev);
        void Stop();
    }
}
