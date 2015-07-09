using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace CozyAnywhere.ClientCore
{
    public partial class AnywhereClient
    {
        public Collection<Tuple<string, bool>> FileCollection { get; set; }
    }
}
