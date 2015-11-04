using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyAscii.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Count() == 1)
            {
                char[] chars = { '@', 'w', '#', '$', 'k', 'd', 't', 'j', 'i', '.', ' ' };
                Bitmap b = new Bitmap(args[0]);
                for (int y = 0; y < b.Height; ++y)
                {
                    for (int x = 0; x < b.Width; ++x)
                    {
                        System.Console.Write(chars[(b.GetPixel(x, y).R + b.GetPixel(x, y).G + b.GetPixel(x, y).B) / 3 / 25]);
                    }
                    System.Console.WriteLine();
                }
                System.Console.ReadKey();
            }
        }
    }
}
