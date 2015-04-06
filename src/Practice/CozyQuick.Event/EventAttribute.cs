using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyQuick.Event
{
    [AttributeUsage(AttributeTargets.Class)]
    public class EventNameAttribute : Attribute
    {
        private readonly string _EventAlias;

        public EventNameAttribute(string commandAlias)
        {
            _EventAlias = commandAlias;
        }

        public string EventName
        {
            get { return _EventAlias; }
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class EventHelpAttribute : Attribute
    {
        public EventHelpAttribute(string text)
        {
            this.HelpText = text;
        }

        public string HelpText { get; set; }
    }
}
