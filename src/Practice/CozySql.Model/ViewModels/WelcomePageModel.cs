using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozySql.Model.ViewModels
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
