using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CozyDitto.Exe.ViewModel
{
    public partial class MainWindowViewModel
    {
        private ObservableCollection<string> clipboardList = new ObservableCollection<string>();
        public ObservableCollection<string> ClipboardList
        {
            get
            {
                return clipboardList;
            }
            set
            {
                Set(ref clipboardList, value, "ClipboardList");
            }
        }

        private Visibility windowVisibility = Visibility.Visible;
        public Visibility WindowVisibility
        {
            get
            {
                return windowVisibility;
            }
            set
            {
                Set(ref windowVisibility, value, "WindowVisibility");
            }
        }

        private string selectedClipboardText;
        public string SelectedClipboardText
        {
            get
            {
                return selectedClipboardText;
            }
            set
            {
                Set(ref selectedClipboardText, value, "SelectedClipboardText");
            }
        }
    }
}
