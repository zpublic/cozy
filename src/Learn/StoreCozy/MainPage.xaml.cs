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
        #region Property

        /// <summary>
        /// NavigationHelper 在每页上用于协助导航和
        /// 进程生命期管理
        /// </summary>
        private NavigationHelper navigationHelper;
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        /// <summary>
        /// 可将其更改为强类型视图模型。
        /// </summary>
        private ObservableDictionary defaultViewModel;
        public ObservableDictionary DefaultViewModel
        {
            get 
            {
                return defaultViewModel = defaultViewModel ?? new ObservableDictionary();
            }
        }
        #endregion

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

         private void OnMenuCardClick(object sender, ItemClickEventArgs e)
        {
            Frame.Navigate(typeof(MenuItemsPage), e.ClickedItem);
        }

        #region Command

         // 添加MenuCard 添加的操作在AddMenuCardPage中
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

        // 将选中的Item从storage中删除
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
                // 确认对话框
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
        #endregion
    }
}
