using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyGod.Tool.CardGenerator.json
{
    public class TranslateObject
    {
        public LocalizationObject Localization { get; set; }
    }

    public class LocalizationObject
    {
        public List<StringObject> String { get; set; }
    }

    public class StringObject
    {
        public string name { get; set; }
        public string text { get; set; }
    }

}
