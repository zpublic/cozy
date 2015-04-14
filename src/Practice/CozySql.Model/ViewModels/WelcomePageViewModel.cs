using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozySql.Model.ViewModels
{
    public class WelcomePageViewModel : BaseViewModel
    {
        private WelcomePageModel[] _Data;
        public WelcomePageModel[] Data
        {
            get
            {
                return _Data;
            }
            set
            {
                Set(ref _Data, value, "Data");
            }
        }

        public WelcomePageViewModel()
        {

        }

        public void TestData()
        {
            var WelcomeBlockList = new WelcomePageModel[]
            {
                new WelcomePageModel
                {
                    Text = "Open a sqlite db",
                    FontSize = 50,
                    Foreground = "{StaticResource Foreground}",
                },
                new WelcomePageModel
                {
                    Text = "Connect a mysql db",
                    FontSize = 30,
                    Foreground = "{StaticResource Foreground}",
                },
            };
             
            Data = WelcomeBlockList;
        }
    }
}
