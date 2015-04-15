using System;
using System.Collections;

namespace CozySql.Exe.ViewModels
{
    public class WelcomePageInfo
    {
        public string Text { get; set; }

        public string Foreground { get; set; }

        public int FontSize { get; set; }
    }

    public class WelcomePageModel : IEnumerable
    {
        public WelcomePageInfo[] elemts { get; set; }

        public IEnumerator GetEnumerator()
        {
            return elemts.GetEnumerator();
        }
    }
}
