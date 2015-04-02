using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfCozy.A.TemplateDemoDetails
{
    public class Countries
    {
        public static IEnumerable<Country> GetCountries()
        {
            return new List<Country>
            {
                new Country { Name = "Austria", ImagePath = "Images/Austria.bmp" },
                new Country { Name = "Germany", ImagePath = "Images/Germany.bmp" },
                new Country { Name = "Norway", ImagePath = "Images/Norway.bmp" },
                new Country { Name = "USA", ImagePath = "Images/USA.bmp" }
            };
        }

    }
}
