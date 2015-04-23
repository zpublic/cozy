using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data;
using System.Xml.Linq;
using Cozy.LearnLinq.Details;

namespace Cozy.LearnLinq
{
    class CustomSequenceOperators
    {
        public static void Cozy()
        {
            CustomSequenceOperators self = new CustomSequenceOperators();

            self.Linq98();
        }

        public void Linq98()
        {
            int[] vectorA = { 0, 2, 4, 5, 6 };
            int[] vectorB = { 1, 3, 5, 7, 8 };
            int dotProduct = vectorA.Combine(vectorB, (a, b) => a * b).Sum();

            Console.WriteLine("Dot product: {0}", dotProduct); 
        }
    }
}
