using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Composition;

namespace CozyQuick.Interface
{
    public interface IEventPublish
    {
        bool Init(IEventDispatcher disp);
        bool Uninit();
        string Name();
        bool ShowPublishConfigurePanel();
    }
}
