using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyGod.Game.Interface.ConfigEnum;

namespace CozyGod.Game.Interface
{
    public interface IGameConfig
    {
        bool TryGetStringConfig(StringConfigEnum name, out string output);
        bool TryGetIntegerConfig(IntegerConfigEnum name, out int output);

        bool TrySetStringConfig(StringConfigEnum name, string value);
        bool TrySetIntegerConfig(IntegerConfigEnum name, int value);
    }
}
