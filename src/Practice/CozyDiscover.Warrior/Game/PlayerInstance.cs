namespace CozyDiscover.Warrior.Game
{
    public class PlayerInstance
    {
        public static readonly PlayerInstance Instance = new PlayerInstance();

        public string Name = "zapline";
        public int Level = 1;
        public int EXP = 0; // 经验
        public int STR = 0; // 力量
        public int CON = 0; // 体魄
        public int DEX = 0; // 智慧
    }
}
