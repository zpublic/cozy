using CozyGod.Game.Engine;
using CozyGod.Game.Interface;
using CozyGod.Model;

namespace CozyGod.Test.CraftTester
{
    class Program
    {
        static void Main(string[] args)
        {
            ICozyGodEngine engine = new CozyGodEngine();
            engine.Init();

            ICraft i = engine.GetCraft();
            Card a = new Card();
            Card b = new Card();
            i.TryCraft(a, b);
            i.Craft(a, b);
        }
    }
}
