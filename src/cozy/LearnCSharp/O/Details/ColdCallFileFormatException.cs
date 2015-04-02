using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cozy.LearnCSharp.O.Details
{
    public class ColdCallFileFormatException : Exception
    {
        public ColdCallFileFormatException(string message) : base(message)
        {
        }

        public ColdCallFileFormatException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
