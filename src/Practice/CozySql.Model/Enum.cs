using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozySql.Model
{
    public enum UserControlEnum
    {
        ConnectEditor = 0,
        DatabasesView = 1,
        SqlFavorites = 2,
        SqlInput = 4,
        SqlView = 8,
        TablesView = 16,
        WelcomePage = 32
    }
}
