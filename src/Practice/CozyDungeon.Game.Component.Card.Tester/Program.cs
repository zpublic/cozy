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
                Name = "c1",
                Desc = "card1",
                Element = Enum.FiveLine.Gold,
                HP = 10,
                ATK = 2,
                DEF = 1
            };
            RoleCard card2 = new RoleCard()
            {
                Id = 2,
                Name = "c2",
                Desc = "card2",
                Element = Enum.FiveLine.Wood,
                HP = 10,
                ATK = 3,
                DEF = 1
            };

            RoleCard r1 = (RoleCard)card1.Clone();
            RoleCard r2 = (RoleCard)card2.Clone();

            Console.WriteLine("r1.hp={0} r2.hp={1}", r1.HP, r2.HP);
            RoleCardLogic.Attack(r1, r2);
            Console.WriteLine(RoleCardLogic.IsAlive(r2));
            Console.WriteLine("r1.hp={0} r2.hp={1}", r1.HP, r2.HP);
            RoleCardLogic.Attack(r1, r2);
            Console.WriteLine("r1.hp={0} r2.hp={1}", r1.HP, r2.HP);
            RoleCardLogic.Attack(r1, r2);
            Console.WriteLine("r1.hp={0} r2.hp={1}", r1.HP, r2.HP);
            RoleCardLogic.Attack(r1, r2);
            Console.WriteLine("r1.hp={0} r2.hp={1}", r1.HP, r2.HP);
            RoleCardLogic.Attack(r2, r1);
            Console.WriteLine("r1.hp={0} r2.hp={1}", r1.HP, r2.HP);
            RoleCardLogic.Attack(r2, r1);
            Console.WriteLine(RoleCardLogic.IsAlive(r2));
            Console.WriteLine("r1.hp={0} r2.hp={1}", r1.HP, r2.HP);
        }
    }
}
