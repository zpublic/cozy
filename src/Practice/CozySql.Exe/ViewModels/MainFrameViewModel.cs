using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using CozySql.Exe.Commands;
using CozySql.Exe.Models;
using CozySql.Exe.UserControls;
using Microsoft.Win32;
using Simple.Data;
using System.ComponentModel;

namespace CozySql.Exe.ViewModels
{
    public class MainFrameViewModel : BaseViewModel
    {
        #region Property

        private ObservableCollection<UIControlInfo> mainTabItems;
        public ObservableCollection<UIControlInfo> MainTabItems
        {
            get
            {
                return mainTabItems;
            }
            set
            {
                Set(ref mainTabItems, value, "MainTabItems");
            }
        }

        private ObservableCollection<SelectPropertyInfo> _SelectTreeItems;
        public ObservableCollection<SelectPropertyInfo> SelectTreeItems
        {
            get
            {
                return _SelectTreeItems;
            }
            set
            {
                Set(ref _SelectTreeItems, value, "SelectTreeInfo");
            }
        }

        private bool isOpenLeftFlyout;
        public bool IsOpenLeftFlyout
        {
            get
            {
                return isOpenLeftFlyout;
            }
            set
            {
                Set(ref isOpenLeftFlyout, value, "IsOpenLeftFlyout");
            }
        }

        #endregion

        #region Commad

        private ICommand openSqliteCommand;
        public ICommand OpenSqliteCommand
        {
            get
            {
                return openSqliteCommand = openSqliteCommand ?? new DelegateCommand(x =>
                {
                    var openFileDialog = new OpenFileDialog();
                    if (openFileDialog.ShowDialog() ?? false)
                    {
                        var db = Database.OpenFile(openFileDialog.FileName);
                        MessageBox.Show(openFileDialog.FileName);
                    }
                });
            }
        }

        private ICommand addTabCommand;
        public ICommand AddTabCommand
        {
            get
            {
                return addTabCommand = addTabCommand ?? new DelegateCommand(x =>
                {
                    MainTabItems.Add(new UIControlInfo
                    {
                        Title = "新建",
                        Content = new SqlInput()
                    });
                });
            }
        }

        private ICommand removeTabCommand;
        public ICommand RemoveTabCommand
        {
            get
            {
                return removeTabCommand = removeTabCommand ?? new DelegateCommand(x => MainTabItems.RemoveAt(MainTabItems.Count - 1),
                    x => MainTabItems.Count > 0);
            }
        }

        private ICommand showLeftCommand;
        public ICommand ShowLeftCommand
        {
            get
            {
                return showLeftCommand = showLeftCommand ?? new DelegateCommand(x => IsOpenLeftFlyout = true);
            }
        }

        private ICommand addTreeViewItem;
        public ICommand AddTreeViewItem
        {
            get
            {
                return addTreeViewItem = addTreeViewItem ?? new DelegateCommand(x => 
                {
                    SelectTreeItems.Add(new SelectPropertyInfo
                    {
                        Name = "New Item",
                    });
                });
            }
        }

        #endregion

        public MainFrameViewModel()
        {
            PropertyChanged += ItemChangeEvent;
            TestData();
            TreeTestData();
        }

        void TestData()
        {
            MainTabItems = new ObservableCollection<UIControlInfo>(new[]
            {
                new UIControlInfo { Title = "Welcome!", Content = new WelcomePage() },
                new UIControlInfo { Title = "Sql Favorites", Content = new SqlFavorites() },
                new UIControlInfo { Title = "Query!", Content = new SqlInput() },
                new UIControlInfo { Title = "SqlView1", Content = new SqlView() },
                new UIControlInfo { Title = "SqlView2", Content = new SqlView() },
                new UIControlInfo { Title = "SqlInput", Content = new SqlInput() }
            });
        }

        void TreeTestData()
        {
            var TempItem = new ObservableCollection<SelectPropertyInfo>
            {
                new SelectPropertyInfo
                {
                    Name = "TreeView Floder",
                    Children = 
                    {
                        new SelectPropertyInfo
                        {
                            Name = "TreeView Floder",
                            Children = 
                            {
                                new SelectPropertyInfo
                                {
                                    Name = "TreeView Node 1",
                                },
                                new SelectPropertyInfo
                                {
                                    Name = "TreeView Node 2",
                                },
                            },
                        },
                        new SelectPropertyInfo
                        {
                            Name = "TreeView Floder",
                            Children = 
                            {
                                new SelectPropertyInfo
                                {
                                    Name = "TreeView Node",
                                },
                            },
                        },
                    },
                },
                new SelectPropertyInfo
                {
                    Name = "TreeView Floder",
                    Children = 
                    {
                        new SelectPropertyInfo
                        {
                            Name = "TreeView Node",
                        },
                    },
                },
            };

            SelectTreeItems = TempItem;
        }

        private void ItemChangeEvent(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectTreeInfo")
            {

            }
        }
    }
}
