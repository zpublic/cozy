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
                    elemts = new WelcomePageInfo[]
                    {
                        new WelcomePageInfo
                        {
                            Text = "Open a sqlite db",
                            FontSize = 50,
                            Foreground = "#FFFFFF",
                        },
                        new WelcomePageInfo
                        {
                            Text = "sqlite db viewer",
                            FontSize = 30,
                            Foreground = "#FFFFFF",
                        },
                        new WelcomePageInfo
                        {
                            Text = "Select a *.db file",
                            FontSize = 18,
                            Foreground = "#FFFFFF",
                        },
                        new WelcomePageInfo
                        {
                            Text = "For sqlite: only read",
                            FontSize = 18,
                            Foreground = "#FFAAAA",
                        },
                    },
                },

                new WelcomePageModel
                {
                    elemts = new WelcomePageInfo[]
                    {
                        new WelcomePageInfo
                        {
                            Text = "Connect a mysql db",
                            FontSize = 30,
                            Foreground = "#FFFFFF",
                        },
                        new WelcomePageInfo
                        {
                            Text = "query mysql",
                            FontSize = 18,
                            Foreground = "#FFFFFF",
                        },
                        new WelcomePageInfo
                        {
                            Text = "Input server's ip address",
                            FontSize = 18,
                            Foreground = "#FFFFFF",
                        },
                        new WelcomePageInfo
                        {
                            Text = "run sql",
                            FontSize = 18,
                            Foreground = "#AAAAAA",
                        },
                    },
                },
            };
             
            Data = WelcomeBlockList;
        }
    }
}
