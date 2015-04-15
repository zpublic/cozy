using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozySql.Model.ViewModels
{
    public class MainFrameViewModel : BaseViewModel
    {
        private Dictionary<string, UserControlEnum> userControls;
        public Dictionary<string, UserControlEnum> UserControls
        {
            get
            {
                return userControls;
            }
            set
            {
                Set(ref userControls, value, "UserControls");
            }
        }

        public MainFrameViewModel()
        {
            TestData();
        }

        void TestData()
        {
            UserControls = new Dictionary<string, UserControlEnum>();
            UserControls.Add("Welcome!", UserControlEnum.WelcomePage);
            UserControls.Add("Sql Favorites",UserControlEnum.SqlFavorites);
            UserControls.Add("Query!",UserControlEnum.SqlInput);
            UserControls.Add("SqlView1",UserControlEnum.SqlView);
            UserControls.Add("SqlView2",UserControlEnum.SqlView);
            UserControls.Add("SqlInput",UserControlEnum.SqlInput);
        }
    }
}
