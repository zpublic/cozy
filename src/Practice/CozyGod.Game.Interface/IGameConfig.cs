using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyGod.Game.Interface
{
    public interface IGameConfig
    {
        string GetContentPath();

        bool TryGetConfig<T>(string name, out T output);
        bool TrySetConfig<T>(string name, T value);
    }
}
