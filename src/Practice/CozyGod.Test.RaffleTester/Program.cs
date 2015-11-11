using CozyGod.Game.Engine;
using CozyGod.Game.Interface;
using CozyGod.Model;

namespace CozyGod.Test.RaffleTester
{
    class Program
    {
        static void Main(string[] args)
        {
            ICozyGodEngine engine = new CozyGodEngine();
            engine.Init();

            IRaffle i = engine.GetRaffle();
            Card c = i.Draw();
        }
    }
}
