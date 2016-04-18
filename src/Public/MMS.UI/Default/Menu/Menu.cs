using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MMS.UI.Default
{
    public class Menu : System.Windows.Controls.Menu
    {
        private List<MenuItem> mMenuItems = new List<MenuItem>();
        public List<MenuItem> MenuItems { get { return this.mMenuItems; } set { this.mMenuItems = value; } }

        private System.Windows.Controls.Menu mMainMenu;

        public static readonly DependencyProperty MenuItemsProperty = DependencyProperty.Register("MenuItems", typeof(List<MenuItem>), typeof(Menu), null);

        public Menu()
        {
            this.Style = (Style)Application.Current.Resources["DefaultMenuStyle"];
            this.Loaded += Menu_Loaded;
        }

        void Menu_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.mMenuItems != null)
            {
                this.mMainMenu.ItemsSource = this.mMenuItems;
            }
        }

        public override void OnApplyTemplate()
        {
            this.mMainMenu = (System.Windows.Controls.Menu)this.GetTemplateChild("mainMenu");
            base.OnApplyTemplate();
        }
    }
}
