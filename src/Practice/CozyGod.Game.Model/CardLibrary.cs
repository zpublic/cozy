using System.Collections.Generic;

namespace CozyGod.Game.Model
{
    public class CardLibrary
    {
        //public static int MaxLevel = 15;
        public List<Card>[] Cards { get; private set; }

        public CardLibrary(int maxLevel = 15)
        {
            Cards = new List<Card>[maxLevel + 1];
        }
    }
}
