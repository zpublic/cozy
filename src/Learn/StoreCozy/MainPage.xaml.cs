using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using StoreCozy.Common;
using System.Collections.ObjectModel;
using Windows.UI.Popups;
using StoreCozy.Model;
using StoreCozy.Storage;
using StoreCozy.Repositories;

namespace StoreCozy
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {

        private NavigationHelper navigationHelper;
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        private ObservableDictionary defaultViewModel;
        public ObservableDictionary DefaultViewModel
        {
            get 
            {
                return defaultViewModel = defaultViewModel ?? new ObservableDictionary();
            }
        }

        public MainPage()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
        }

        private async void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            this.DefaultViewModel["Commands"] = this;

            var storage = new MenuCardStorage();
            MenuCardRepository.Instance.InitMenuCards(new ObservableCollection<MenuCard>(
                await storage.ReadMenuCardsAsync()));
            this.DefaultViewModel["Items"] = MenuCardRepository.Instance.Cards;
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        private void OnAdd()
        {
            Frame.Navigate(typeof(AddMenuCardPage));
        }

        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ?? (addCommand = new RelayCommand(OnAdd));
            }
        }

        private void DeleteMenuCard(IUICommand command)
        {
            var menuCards = this.DefaultViewModel["Items"] as ObservableCollection<MenuCard>;
            if (menuCards != null)
            {
                menuCards.Remove(itemGridView.SelectedItem as MenuCard);
            }
        }

        private async void OnDelete()
        {
            var selectedMenuCard = itemGridView.SelectedItem as MenuCard;
            if (selectedMenuCard != null)
            {

                var dlg = new MessageDialog(string.Format("Delete the menu card {0}?", selectedMenuCard.Title));
                dlg.Commands.Add(new UICommand("Delete", new UICommandInvokedHandler(DeleteMenuCard)));
                dlg.Commands.Add(new UICommand("Cancel"));
                await dlg.ShowAsync();
            }
        }

        private RelayCommand deleteCommand;
        public RelayCommand DeleteCommand
        {
            get
            {
                return deleteCommand ?? (deleteCommand = new RelayCommand(OnDelete));
            }
        }
    }
}
