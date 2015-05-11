using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starbound.UI.Resources
{
    public interface IResourceManager
    {
        T GetResource<T>(string id) where T : IResource;
    }
}
