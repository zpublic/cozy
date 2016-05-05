using System.Collections.Generic;

namespace CozyPeise.Models
{
    public class Palette
    {
        public string Name { get; set; }

        public string Url { get; set; }

        public List<string> RGB { get; set; } = new List<string>();
    }
}
