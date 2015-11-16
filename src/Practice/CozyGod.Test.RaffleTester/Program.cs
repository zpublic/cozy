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
            IRaffle iRaffle = engine.GetRaffle();
            int[] cardRet = new int[10];
            for (int n = 0; n < drawTestNumber; n++)
            {
                pentaDrawTest = iRaffle.PentaDraw();

                foreach (var c in pentaDrawTest)
                {
                    if(c != null)
                    {
                        System.Console.WriteLine("card name : {0}, card level : {1}", c.Name, c.Level);
                        cardRet[c.Level]++;
                    }
                    else
                    {
                        System.Console.WriteLine("你抽到一张并不存在的卡牌");
                        return;
                    }
                }
                System.Console.WriteLine("---------------------------------------------");
            }

            for(int i = 0; i < cardRet.Length; i++)
            {
                System.Console.WriteLine("level:{0} count: {1} probability: {2};", i,cardRet[i],cardRet[i]/(5.0 * drawTestNumber));
            }
        }
    }
}
