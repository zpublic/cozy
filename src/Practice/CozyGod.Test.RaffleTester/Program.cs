using CozyGod.Game.Engine;
using CozyGod.Game.Interface;
using CozyGod.Game.Model;

namespace CozyGod.Test.RaffleTester
{
    class Program
    {
        static void Main(string[] args)
        {
            const int drawTestNumber = 5;
            ICozyGodEngine engine = new CozyGodEngine();
            engine.Init();

            Card[] pentaDrawTest;
            Card drawTest;
            IRaffle iRaffle = engine.GetRaffle();

            for (int n = 0; n < drawTestNumber; n++)
            {
                pentaDrawTest = iRaffle.PentaDraw();
                foreach (var c in pentaDrawTest)
                {
                    System.Console.WriteLine("card name : {0}, card level : {1}", c.Name, c.Level);
                }

                drawTest = iRaffle.Draw(2);
                System.Console.WriteLine("card name : {0}, card level : {1}", drawTest.Name, drawTest.Level);
            }
        }
    }
}
