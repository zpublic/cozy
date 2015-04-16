using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozySql.Exe.Models;

namespace CozySql.Exe.ViewModels
{
    public class WelcomePageViewModel : BaseViewModel
    {
        private List<WelcomePageInfo> _Data;
        public List<WelcomePageInfo> Data
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
            TestData();
        }

        public void TestData()
        {
            var WelcomeBlockList = new List<WelcomePageInfo>
            {
                new WelcomePageInfo
                {
                    Elemts = new List<WelcomePageBlockInfo>
                    {
                        new WelcomePageBlockInfo
                        {
                            Text = "Open a sqlite db",
                            FontSize = 50,
                            Foreground = "#FFFFFF",
                        },
                        new WelcomePageBlockInfo
                        {
                            Text = "sqlite db viewer",
                            FontSize = 30,
                            Foreground = "#FFFFFF",
                        },
                        new WelcomePageBlockInfo
                        {
                            Text = "Select a *.db file",
                            FontSize = 18,
                            Foreground = "#FFFFFF",
                        },
                        new WelcomePageBlockInfo
                        {
                            Text = "For sqlite: only read",
                            FontSize = 18,
                            Foreground = "#FFAAAA",
                        },
                    },
                },
                new WelcomePageInfo
                {
                    Elemts = new List<WelcomePageBlockInfo>
                    {
                        new WelcomePageBlockInfo
                        {
                            Text = "Connect a mysql db",
                            FontSize = 30,
                            Foreground = "#FFFFFF",
                        },
                        new WelcomePageBlockInfo
                        {
                            Text = "query mysql",
                            FontSize = 18,
                            Foreground = "#FFFFFF",
                        },
                        new WelcomePageBlockInfo
                        {
                            Text = "Input server's ip address",
                            FontSize = 18,
                            Foreground = "#FFFFFF",
                        },
                        new WelcomePageBlockInfo
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
