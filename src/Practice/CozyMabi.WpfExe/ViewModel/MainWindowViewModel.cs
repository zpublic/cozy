using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using CozyMabi.WpfExe.Command;
using System.Collections.ObjectModel;
using CozyMabi.WpfExe.Model;
using CozyMabi.WpfExe.UserControls;

namespace CozyMabi.WpfExe.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {

        #region Property

        private ObservableCollection<UIControlInfo> _MainTabItems;
        public ObservableCollection<UIControlInfo> MainTabItems
        {
            get
            {
                return _MainTabItems;
            }
            set
            {
                Set(ref _MainTabItems, value, "MainTabItems");
            }
        }

        #endregion

        public MainWindowViewModel()
        {
            this.PropertyChanged += PropertyChangedEvent;

            InitTabItems();
        }

        void InitTabItems()
        {
            MainTabItems = new ObservableCollection<UIControlInfo>(new[]
            {
                new UIControlInfo{ Title = "Bubble", Content= new BubbleView() },
            });
        }

        private void PropertyChangedEvent(object sender, PropertyChangedEventArgs e)
        {
            
        }
    }
}
