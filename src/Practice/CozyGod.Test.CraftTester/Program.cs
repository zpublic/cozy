using CozyGod.Game.Engine;
using CozyGod.Game.Interface;
using CozyGod.Model;
using System.Collections.Generic;

namespace CozyGod.Test.CraftTester
{
    
    class Program
    {
        static void Main(string[] args)
        {
            const int drawTestNumber = 100;
            ICozyGodEngine engine = new CozyGodEngine();
            engine.Init();

            ICraft i = engine.GetCraft();
            Card a = new Card();
            Card b = new Card();
            System.Console.WriteLine("You cost card name : {0}, card level : {1} and card name : {2}, card level : {3}"
                , a.Name, a.Level, b.Name, b.Level);
            i.TryCraft(a, b);
            i.Craft(a, b);
            System.Console.WriteLine("You get card name : {0}, card level : {1}", i.Craft(a, b).Name, i.Craft(a, b).Level);

            Card[] pentaDrawTest;
            Card drawTest;
            IRaffle iRaffle = engine.GetRaffle();

            for(int n = 0; n < drawTestNumber; n++)
            {
                pentaDrawTest = iRaffle.PentaDraw();
                drawTest = iRaffle.Draw(2);
                int index = 0;
                while(pentaDrawTest[index] != null)
                {
                    System.Console.Write("card name : {0}, card level : {1}",pentaDrawTest[index].Name,pentaDrawTest[index].Level);
                }
                System.Console.Write("\n");
                System.Console.WriteLine("card name : {0}, card level : {1}", drawTest.Name, drawTest.Level);
            }
            
        }
    }
}
