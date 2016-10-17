using System;
using System.Linq;

namespace CozyFreeGame.GameData
{
    public class WordPair
    {
        public char Word1;
        public char Word2;
    }

    public class TwoWordLib
    {
        public int LevelMax;
        public WordPair[] LevelWords;
    }

    public class WordPairGenerator
    {
        static string Lib = "壁璧刑形绞跤彻砌歼奸魅魁搀馋遗遣碗婉诞蜒巴巳失矢悄俏大太戍戌白自兵乒竟竞鸣呜柬束壳亮含合士土己已籍藉误娱";
        static WordPair[] LevelWords;
        private static readonly WordPairGenerator _inst;
        static WordPairGenerator()
        {
            _inst = new WordPairGenerator();
            LevelWords = new WordPair[Lib.Length / 2];
            for (var i = 0; i < Lib.Length / 2; i++)
            {
                LevelWords[i] = new WordPair() { Word1 = Lib[i * 2], Word2 = Lib[i * 2 + 1] };
            }
        }
        public static WordPairGenerator Instance
        {
            get { return _inst; }
        }
        private WordPairGenerator()
        { }

        public TwoWordLib Gen()
        {
            TwoWordLib lib = new TwoWordLib();
            lib.LevelMax = 10;
            lib.LevelWords = LevelWords.OrderBy(o => Guid.NewGuid()).Take(lib.LevelMax).ToArray();
            return lib;
        }
    }
}
