using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyCDK.Core
{
    public interface IEncrypt
    {
        string Generate(string source);
    }
}
