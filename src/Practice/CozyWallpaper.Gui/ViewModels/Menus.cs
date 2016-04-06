using MMS.UI.Default;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyWallpaper.Gui.ViewModels
{
    public class Menus
    {
        private static Menus mMenu;

        public static Menus GetInstance()
        {
            if (mMenu == null)
            {
                mMenu = new Menus();
            }
            return mMenu;
        }

        public List<NavigationItem> Menu { get; set; }

        private Menus()
        {
            this.Menu = new List<NavigationItem>();
            NavigationItem all = new NavigationItem()
            {
                Status = NavigationType.Process,
                Text = "全部壁纸"
            };
            NavigationItem local = new NavigationItem()
            {
                Status = NavigationType.Wait,
                Text = "本地壁纸"
            };
            this.Menu.Add(all);
            this.Menu.Add(local);
        }
    }
}
