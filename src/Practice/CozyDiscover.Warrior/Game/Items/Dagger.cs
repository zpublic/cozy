namespace CozyDiscover.Warrior.Game.Items
{
    public class Dagger : Item
    {
        public int Level;
        public int ATK;

        public static Dagger Generate()
        {
            Dagger d = new Dagger();
            d.Id = 1;
            d.Name = "匕首";
            d.Level = 1;
            d.ATK = MainRandom.Gen(1, 3);
            return d;
        }
    }
}
