using CozyGod.Game.Engine;
using CozyGod.Game.Interface;
using CozyGod.Game.Model;
using System.Collections.Generic;

namespace CozyGod.Test.CraftTester
{
    
    class Program
    {
        static void Main(string[] args)
        {
            ICozyGodEngine engine = new CozyGodEngine();
            engine.Init();

            ICraft i = engine.GetCraft();
            Card a = null;
            Card b = null;
            ICardLibrary lib = engine.GetCardLibrary();


            while(true)
            {
                string input = System.Console.ReadLine();
                string[] craftCard = input.Split(',');
                if(craftCard.Length < 2)
                {
                    System.Console.WriteLine("Input Error, \"name,name\"");
                    continue;
                }
                a = lib.FindCardByName(craftCard[0]);
                b = lib.FindCardByName(craftCard[1]);
                if (a != null && b != null)
                {
                    System.Console.Write("card : {0}, level : {1} + card : {2}, card level : {3} = "
                        , a.Name, a.Level, b.Name, b.Level);
                }
                else
                {
                    System.Console.Write("Some card has no fond.");
                    continue;
                }
                Card cardRet = null;
                if (i.TryCraft(a, b))
                {
                    cardRet = i.Craft(a, b);
                }
                else
                {
                    System.Console.WriteLine("null");
                    continue;
                }
                System.Console.WriteLine("card : {0}, level : {1}", cardRet.Name, cardRet.Level);
            }
            
        }
    }
}
