using System;
using System.Linq;

namespace CozyFreeGame.GameData
{
    public class ThreeWord
    {
        public char Word1;
        public char Word2;
        public char Word3;
    }

    public class ThreeWordLib
    {
        public int LevelMax;
        public ThreeWord[] LevelWords;
    }

    public class ThreeWordLibGenerator
    {
        static string Lib = "大太犬戍戌戊兵乒乓";
        static ThreeWord[] LevelWords;
        private static readonly ThreeWordLibGenerator _inst;
        static ThreeWordLibGenerator()
        {
            _inst = new ThreeWordLibGenerator();
            LevelWords = new ThreeWord[Lib.Length / 3];
            for (var i = 0; i < Lib.Length / 3; i++)
            {
                LevelWords[i] = new ThreeWord()
                {
                    Word1 = Lib[i * 3],
                    Word2 = Lib[i * 3 + 1],
                    Word3 = Lib[i * 3 + 2]
                };
            }
        }
        public static ThreeWordLibGenerator Instance
        {
            get { return _inst; }
        }
        private ThreeWordLibGenerator()
        { }

        public ThreeWordLib Gen()
        {
            ThreeWordLib lib = new ThreeWordLib();
            lib.LevelMax = 3;
            lib.LevelWords = LevelWords.OrderBy(o => Guid.NewGuid()).Take(lib.LevelMax).ToArray();
            return lib;
        }
    }
}