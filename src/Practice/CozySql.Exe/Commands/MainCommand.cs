using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CozySql.Exe.Commands
{
    public static class MainCommand
    {
        private static RoutedUICommand OpenSqlite_;
        public static ICommand OpenSqlite
        {
            get
            {
                return OpenSqlite_ ?? (OpenSqlite_ = new RoutedUICommand("OpenSqlite", "OpenSqlite", typeof(MainCommand)));
            }
        }
    }
}
