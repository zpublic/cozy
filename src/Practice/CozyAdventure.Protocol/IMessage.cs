using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyAdventure.Protocol
{
    public interface IMessage
    {
        uint Id { get; }
    }
}
