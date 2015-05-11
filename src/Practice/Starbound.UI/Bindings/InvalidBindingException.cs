using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Starbound.UI.Bindings
{
    public class InvalidBindingException : Exception
    {
        public InvalidBindingException() : base() { }
        public InvalidBindingException(string message) : base(message) { }
        public InvalidBindingException(string message, Exception innerException) : base(message, innerException) { }
        public InvalidBindingException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
