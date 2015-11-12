using System.Collections.Generic;

namespace CozyGod.Model
{
    public class CardLibrary
    {
        public static int MaxLevel = 7;
        public List<Card>[] Cards = new List<Card>[MaxLevel + 1];
    }
}
