using StoreCozy.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Input;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “拆分页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234234 上有介绍

namespace StoreCozy
{
    /// <summary>
    /// 显示组标题、组内各项的列表以及当前选定项的
    /// 详细信息的页。
    /// </summary>
    public sealed partial class SplitPage1 : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        /// <summary>
        /// 可将其更改为强类型视图模型。
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// NavigationHelper 在每页上用于协助导航和
        /// 进程生命期管理
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        public SplitPage1()
        {
            this.InitializeComponent();

            // 设置导航帮助程序
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
            this.navigationHelper.SaveState += navigationHelper_SaveState;

            // 设置逻辑页面导航组件，使
            // 页面可一次仅显示一个窗格。
            this.navigationHelper.GoBackCommand = new StoreCozy.Common.RelayCommand(() => this.GoBack(), () => this.CanGoBack());
            this.itemListView.SelectionChanged += itemListView_SelectionChanged;

            // 开始侦听 Window 大小更改 
            // 以从显示两个窗格变为显示一个窗格
            Window.Current.SizeChanged += Window_SizeChanged;
            this.InvalidateVisualState();
        }

        void itemListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.UsingLogicalPageNavigation())
            {
                this.navigationHelper.GoBackCommand.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// 使用在导航过程中传递的内容填充页。  在从以前的会话
        /// 重新创建页时，也会提供任何已保存状态。
        /// </summary>
        /// <param name="sender">
        /// 事件的来源; 通常为 <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">事件数据，其中既提供在最初请求此页时传递给
        /// <see cref="Frame.Navigate(Type, Object)"/> 的导航参数，又提供
        /// 此页在以前会话期间保留的状态的
        /// 字典。 首次访问页面时，该状态将为 null。</param>
        private void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            // TODO:  将可绑定组分配给 Me.DefaultViewModel("Group")
            // TODO:  将可绑定项集合分配给 Me.DefaultViewModel("Items")

            if (e.PageState == null)
            {
                // 当这是新页时，除非正在使用逻辑页导航，
                // 否则会自动选择第一项(请参见下面的逻辑页导航 #region。)
                if (!this.UsingLogicalPageNavigation() && this.itemsViewSource.View != null)
                {
                    this.itemsViewSource.View.MoveCurrentToFirst();
                }
            }
            else
            {
                // 还原与此页关联的以前保存的状态
                if (e.PageState.ContainsKey("SelectedItem") && this.itemsViewSource.View != null)
                {
                    // TODO:  使用通过 pageState("SelectedItem")值指定的所选项
                    //       调用 Me.itemsViewSource.View.MoveCurrentTo()

                }
            }
        }

        /// <summary>
        /// 保留与此页关联的状态，以防挂起应用程序或
        /// 从导航缓存中放弃此页。  值必须符合
        /// <see cref="SuspensionManager.SessionState"/> 的序列化要求。
        /// </summary>
        ///<param name="sender">事件的来源；通常为 <see cref="NavigationHelper"/></param>
        ///<param name="e">提供要使用可序列化状态填充的空字典
        ///的事件数据。</param>
        private void navigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
            if (this.itemsViewSource.View != null)
            {
                // TODO:  派生一个可序列化导航参数并将该参数分配给
                //       pageState("SelectedItem")

            }
        }

        #region 逻辑页导航

        // 设计了拆分页，以便 Window 具有足够的空间同时显示
        // 列表和详细信息，一次将仅显示一个窗格。
        //
        // 这完全通过一个可表示两个逻辑页的单一物理页
        // 实现。  使用下面的代码可以实现此目标，且用户不会察觉到
        // 区别。

        private const int MinimumWidthForSupportingTwoPanes = 768;

        /// <summary>
        /// 在确定该页是应用作一个逻辑页还是两个逻辑页时进行调用。
        /// </summary>
        /// <returns>如果窗口应显示充当一个逻辑页，则为 True，
        ///为 false。</returns>
        private bool UsingLogicalPageNavigation()
        {
            return Window.Current.Bounds.Width < MinimumWidthForSupportingTwoPanes;
        }

        /// <summary>
        /// 在 Window 改变大小时调用
        /// </summary>
        /// <param name="sender">当前的 Window</param>
        /// <param name="e">描述 Window 新大小的事件数据</param>
        private void Window_SizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
            this.InvalidateVisualState();
        }

        /// <summary>
        /// 在选定列表中的项时进行调用。
        /// </summary>
        /// <param name="sender">显示所选项的 GridView。</param>
        /// <param name="e">描述如何更改选择内容的事件数据。</param>
        private void ItemListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // 使视图状态在逻辑页导航起作用时无效，因为
            // 选择内容方面的更改可能会导致当前逻辑页发生相应的更改。
            // 选定某项后，这将会导致从显示项列表
            // 更改为显示选定项的详细信息。  清除选择时，将产生
            // 相反的效果。
            if (this.UsingLogicalPageNavigation()) this.InvalidateVisualState();
        }

        private bool CanGoBack()
        {
            if (this.UsingLogicalPageNavigation() && this.itemListView.SelectedItem != null)
            {
                return true;
            }
            else
            {
                return this.navigationHelper.CanGoBack();
            }
        }
        private void GoBack()
        {
            if (this.UsingLogicalPageNavigation() && this.itemListView.SelectedItem != null)
            {
                // 如果逻辑页导航起作用且存在选定项，则当前将显示
                // 选定项的详细信息。  清除选择后将返回到
                // 项列表。  从用户的角度来看，这是一个逻辑后向
                // 导航。
                this.itemListView.SelectedItem = null;
            }
            else
            {
                this.navigationHelper.GoBack();
            }
        }

        private void InvalidateVisualState()
        {
            var visualState = DetermineVisualState();
            VisualStateManager.GoToState(this, visualState, false);
            this.navigationHelper.GoBackCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// 在确定对应于应用程序视图状态的可视状态的名称时进行
        /// 调用。
        /// </summary>
        /// <returns>所需的可视状态的名称。  此名称与视图状态的名称相同，
        /// 但在纵向和对齐视图中存在选定项时例外，在纵向和对齐视图中，
        /// 此附加逻辑页通过添加 _Detail 后缀表示。</returns>
        private string DetermineVisualState()
        {
            if (!UsingLogicalPageNavigation())
                return "PrimaryView";

            // 在视图状态更改时更新后退按钮的启用状态
            var logicalPageBack = this.UsingLogicalPageNavigation() && this.itemListView.SelectedItem != null;

            return logicalPageBack ? "SinglePane_Detail" : "SinglePane";
        }

        #endregion

        #region NavigationHelper 注册

        /// 此部分中提供的方法只是用于使
        /// NavigationHelper 可响应页面的导航方法。
        /// 
        /// 应将页面特有的逻辑放入用于
        /// <see cref="GridCS.Common.NavigationHelper.LoadState"/>
        /// 和 <see cref="GridCS.Common.NavigationHelper.SaveState"/> 的事件处理程序中。
        /// 除了在会话期间保留的页面状态之外
        /// LoadState 方法中还提供导航参数。

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        #endregion
    }
}
