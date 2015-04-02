using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cozy.LearnCSharp.O.Details
{
    public class UnexpectedException : Exception
    {
        public UnexpectedException(string message) : base(message)
        {
        }

        public UnexpectedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
