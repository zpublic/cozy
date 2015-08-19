using CozyDungeon.Game.Component.Card.Logic;
using CozyDungeon.Game.Component.Card.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyDungeon.Game.Component.Card.Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            RoleCard card1 = new RoleCard()
            {
                Id = 1,
                Name = "王文撸",
                Desc = "card1",
                Element = Enum.FiveLine.Gold,
                HP = 10,
                ATK = 2,
                DEF = 1
            };
            RoleCard card2 = new RoleCard()
            {
                Id = 2,
                Name = "愤怒的泡面",
                Desc = "card2",
                Element = Enum.FiveLine.Wood,
                HP = 10,
                ATK = 3,
                DEF = 1
            };

            RoleCard r1 = (RoleCard)card1.Clone();
            RoleCard r2 = (RoleCard)card2.Clone();

            bool b = false;
            while (RoleCardLogic.IsAlive(r1) && RoleCardLogic.IsAlive(r2))
            {
                if (b)
                {
                    Console.WriteLine("{0} attack {1}", r1.Name, r2.Name);
                    RoleCardLogic.Attack(r1, r2);
                    Console.WriteLine("撸撸.hp={0} 泡面.hp={1}\n", r1.HP, r2.HP);
                }
                else
                {
                    Console.WriteLine("{0} attack {1}", r2.Name, r1.Name);
                    RoleCardLogic.Attack(r2, r1);
                    Console.WriteLine("撸撸.hp={0} 泡面.hp={1}\n", r1.HP, r2.HP);
                }
                b = !b;
            }
        }
    }
}
