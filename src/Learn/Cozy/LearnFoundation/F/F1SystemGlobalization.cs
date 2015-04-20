using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Data;

namespace Cozy.LearnFoundation.F
{
    class F1SystemGlobalization
    {
        public static void Cozy()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("-----------------------------------------------");

            Unicode_Issues();
            Number_Formatting();
            Date_Formating();
        }

        public static void Unicode_Issues()
        {
            String str = "a\u0304\u0308bc\u0327";
            Console.WriteLine(str);

            Console.WriteLine("GetTextElementEnumerator");
            TextElementEnumerator charEnum = StringInfo.GetTextElementEnumerator(str);
            while(charEnum.MoveNext())
            {
                Console.WriteLine("Character at index {0} is '{1}'", charEnum.ElementIndex, charEnum.GetTextElement());
            }

            Console.WriteLine("ParseCombiningCharacters");
            Int32[] textElemIndex = StringInfo.ParseCombiningCharacters(str);
            for (Int32 i = 0; i < textElemIndex.Length; i++)
            {
                Console.WriteLine("Character {0} starts at index {1}", i, textElemIndex[i]);
            }
        }

        public static void Number_Formatting()
        {
            int i = 1234567890;

            Console.WriteLine(i.ToString());
            Console.WriteLine(i.ToString("N"));
            Console.WriteLine(i.ToString("N", new CultureInfo("de-DE")));
            Console.WriteLine(i.ToString("N", new CultureInfo("fr-FR")));
        }

        public static void Date_Formating()
        {
            DateTime time = DateTime.Now;

            Console.WriteLine(time.ToString());
            Console.WriteLine(time.ToLongDateString());
            Console.WriteLine(time.ToString("D", new CultureInfo("fr-FR")));
        }
    }
}
