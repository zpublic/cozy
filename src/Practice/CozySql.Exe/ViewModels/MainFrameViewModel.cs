using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using CozySql.Exe.Commands;
using CozySql.Exe.Models;
using CozySql.Exe.UserControls;

namespace CozySql.Exe.ViewModels
{
    public class MainFrameViewModel : BaseViewModel
    {
        private List<UIControlInfo> mainTabItems;
        public List<UIControlInfo> MainTabItems
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
        public ICommand OpenSqliteCommand {
            get {
                return openSqliteCommand ?? new DelegateCommand(x => MessageBox.Show("open"));
            }
        }

        public MainFrameViewModel() {
            TestData();
        }

        void TestData()
        {
            MainTabItems = new List<UIControlInfo>(new[]
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
