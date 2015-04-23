using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cozy.LearnLinq.Details.Compare
{
    public class AnagramEqualityComparer : IEqualityComparer<string>
    {
        public bool Equals(string x, string y)
        {
            return getCanonicalString(x) == getCanonicalString(y);
        }

        public int GetHashCode(string obj)
        {
            return getCanonicalString(obj).GetHashCode();
        }

        private string getCanonicalString(string word)
        {
            char[] wordChars = word.ToCharArray();
            Array.Sort<char>(wordChars);
            return new string(wordChars);
        }
    } 
 
}
