using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CozySql.Exe.Commands;
using CozySql.Exe.Models;
using CozySql.Exe.UserControls;

namespace CozySql.Exe.ViewModels
{
    public class MainFrameViewModel : BaseViewModel
    {
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

        private ICommand openSqliteCommand;
        public ICommand OpenSqliteCommand
        {
            get {
                return openSqliteCommand = openSqliteCommand ?? new DelegateCommand(x => MessageBox.Show("open"));
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

        public MainFrameViewModel()
        {
            TestData();
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
    }
}
