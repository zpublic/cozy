using CozyGod.Game.Craft;
using CozyGod.Game.Interface;
using CozyGod.Model;

namespace CozyGod.Test.CraftTester
{
    class Program
    {
        static void Main(string[] args)
        {
            ICraft i = new CraftImpl();
            Card a = new Card();
            Card b = new Card();
            i.TryCraft(a, b);
            i.Craft(a, b);
        }
    }
}
