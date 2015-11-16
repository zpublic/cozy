using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CozyGod.Game.Engine;
using CozyGod.Game.Interface;

namespace CozyGod.Test.CardLibraryTester
{
    static class Program
    {
        static void Main()
        {
            ICozyGodEngine engine = new CozyGodEngine();
            engine.Init();
            ICardLibrary cards = engine.GetCardLibrary();
            var lib = cards.Get();
            for (int i = 0; i < lib.Cards.Length; ++i)
            {
                Console.WriteLine("Level : " + i);
                Console.WriteLine("Count : " + (lib.Cards[i] == null ? 0 : lib.Cards[i].Count));
                if(lib.Cards[i] != null)
                {
                    foreach(var card in lib.Cards[i])
                    {
                        Console.WriteLine(card.Name + " " + card.CN_Name);
                    }
                }
            }
            Console.ReadKey();
        }
    }
}
