using System.Windows.Controls;
using CozySql.Exe.UserControls;
using CozySql.Model;

namespace CozySql.Exe.Converts
{
    public static class UserControlsAdapter
    {
        public static UserControl GetUserControl(UserControlEnum userControlEnums)
        {
            switch (userControlEnums) {
                case UserControlEnum.ConnectEditor:
                    return new ConnectEditor();
                case UserControlEnum.DatabasesView:
                    return new DatabasesView();
                case UserControlEnum.SqlFavorites:
                    return new SqlFavorites();
                case UserControlEnum.SqlInput:
                    return new SqlInput();
                case UserControlEnum.SqlView:
                    return new SqlView();
                case UserControlEnum.TablesView:
                    return new TablesView();
                case UserControlEnum.WelcomePage:
                    return new WelcomePage();
                default:
                    return null;
            }
        }
    }
}
