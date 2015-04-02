using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace cozy.LearnCSharp.H
{
    class H2RegularExpressions
    {
        public static void Cozy()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            Console.WriteLine("-----------------------------------------------");

            Find1();
            Find2();
        }

        static void Find1()
        {
            const string text = @"XML has made a major impact in almost every aspect of 
            software development. Designed as an open, extensible, self-describing 
            language, it has become the standard for data and document delivery on 
            the web. The panoply of XML-related technologies continues to develop 
            at breakneck speed, to enable validation, navigation, transformation, 
            linking, querying, description, and messaging of data.";
            const string pattern = @"\bn\S*ion\b";
            MatchCollection matches = Regex.Matches(text, pattern,
               RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace |
               RegexOptions.ExplicitCapture);
            WriteMatches(text, matches);
        }

        static void Find2()
        {
            const string text = @"XML has made a major impact in almost every aspect of 
            software development. Designed as an open, extensible, self-describing 
            language, it has become the standard for data and document delivery on 
            the web. The panoply of XML-related technologies continues to develop 
            at breakneck speed, to enable validation, navigation, transformation, 
            linking, querying, description, and messaging of data.";
            const string pattern = @"\bn";
            MatchCollection matches = Regex.Matches(text, pattern,
              RegexOptions.IgnoreCase);
            WriteMatches(text, matches);
        }

        static void WriteMatches(string text, MatchCollection matches)
        {
            Console.WriteLine("Original text was: \n\n" + text + "\n");
            Console.WriteLine("No. of matches: " + matches.Count);
            foreach (Match nextMatch in matches)
            {
                int index = nextMatch.Index;
                string result = nextMatch.ToString();
                int charsBefore = (index < 5) ? index : 5;
                int fromEnd = text.Length - index - result.Length;
                int charsAfter = (fromEnd < 5) ? fromEnd : 5;
                int charsToDisplay = charsBefore + charsAfter + result.Length;

                Console.WriteLine("Index: {0}, \tString: {1}, \t{2}",
                   index, result,
                   text.Substring(index - charsBefore, charsToDisplay));
            }
        }
    }
}
