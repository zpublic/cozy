using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyEditor.EventArguments
{
    public class ValueChangedArgs<T>
    {
        public T OldValue { get; set; }

        public T NewValue { get; set; }
    }
}
