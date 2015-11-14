using CozyGod.Game.Engine;
using CozyGod.Game.Interface;
using CozyGod.Game.Model;

namespace CozyGod.Test.RaffleTester
{
    class Program
    {
        static void Main(string[] args)
        {
            const int drawTestNumber = 100;
            ICozyGodEngine engine = new CozyGodEngine();
            engine.Init();

            Card[] pentaDrawTest;
            Card drawTest;
            IRaffle iRaffle = engine.GetRaffle();

            for (int n = 0; n < drawTestNumber; n++)
            {
                pentaDrawTest = iRaffle.PentaDraw();
                drawTest = iRaffle.Draw(2);
                int index = 0;
                while (pentaDrawTest[index] != null)
                {
                    System.Console.Write("card name : {0}, card level : {1}", pentaDrawTest[index].Name, pentaDrawTest[index].Level);
                }
                System.Console.Write("\n");
                System.Console.WriteLine("card name : {0}, card level : {1}", drawTest.Name, drawTest.Level);
            }
        }
    }
}
