using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyGod.Game.Interface
{
    public interface ICozyGodEngine
    {
        void Init();

        ICardLibrary GetCardLibrary();

        ICraft GetCraft();

        IRaffle GetRaffle();

        IGameConfig GetConfig();
    }
}
