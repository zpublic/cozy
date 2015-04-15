using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple.Data;

namespace CozySql.Exe.ViewModels
{
    public class MainWindowsViewModel : BaseViewModel
    {
        #region Property

        private string[] accents;
        public string[] Accents
        {
            get { return accents; }
            set
            {
                accents = value;
                this.OnPropertyChanged("Accents");
            }
        }

        private string[] themes;
        public string[] Themes
        {
            get { return themes; }
            set
            {
                themes = value;
                this.OnPropertyChanged("Themes");
            }
        }

        private string selectedAccent;
        public string SelectedAccent
        {
            get { return selectedAccent; }
            set
            {
                selectedAccent = value;
                this.OnPropertyChanged("SelectedAccent");
            }
        }

        private int comboBoxWidth;
        public int ComboBoxWidth
        {
            get { return comboBoxWidth; }
            set
            {
                comboBoxWidth = value;
                this.OnPropertyChanged("ComboBoxWidth");
            }
        }

        private bool isLight;
        public bool IsLight
        {
            get { return isLight; }
            set
            {
                isLight = value;
                this.OnPropertyChanged("IsLight");
            }
        }

        #endregion

        public MainWindowsViewModel()
        {
            var fileDb = Database.OpenFile("c:\\mysql.db");

            Accents = new[] { "Red", "Green", "Blue", "Purple", "Orange",
                    "Lime", "Emerald", "Teal", "Cyan", "Cobalt", "Indigo",
                    "Violet", "Pink", "Magenta", "Crimson", "Amber","Yellow",
                    "Brown", "Olive", "Steel", "Mauve", "Taupe", "Sienna" };
            IsLight = false;
            Themes = new[] { "BaseLight", "BaseDark" };
            SelectedAccent = Accents[0];
            ComboBoxWidth = Accents.OrderByDescending(x => x.Length).First().Length * 15;
        }
    }
}
