using StoreCozy.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// TODO: 将搜索结果页连接至应用程序内的搜索。
//“搜索结果页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234240 上有介绍

namespace StoreCozy
{
    /// <summary>
    /// 此页显示全局搜索定向到此应用程序时的搜索结果。
    /// </summary>
    public sealed partial class SearchResultsPage1 : Page
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

        public SearchResultsPage1()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
        }

        /// <summary>
        /// 使用在导航过程中传递的内容填充页。  在从以前的会话
        /// 重新创建页时，也会提供任何已保存状态。
        /// </summary>
        /// <param name="navigationParameter">最初请求此页时传递给
        /// <see cref="Frame.Navigate(Type, Object)"/> 的参数值。
        /// </param>
        /// <param name="pageState">此页在以前会话期间保留的状态
        /// 字典。首次访问页面时为 null。</param>
        private void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            var queryText = e.NavigationParameter as String;

            // TODO:  特定于应用程序的搜索逻辑。  搜索进程负责
            //       创建用户可选的结果类别列表: 
            //
            //       filterList.Add(new Filter("<filter name>", <result count>));
            //
            //       仅第一个筛选器(通常为“全部”)应将 true 作为第三个参数传入
            //       以便以活动状态开始。  活动筛选器的结果在
            //       下面的 Filter_SelectionChanged 中提供。

            var filterList = new List<Filter>();
            filterList.Add(new Filter("All", 0, true));

            // 通过视图模型沟通结果
            this.DefaultViewModel["QueryText"] = '\u201c' + queryText + '\u201d';
            this.DefaultViewModel["Filters"] = filterList;
            this.DefaultViewModel["ShowFilters"] = filterList.Count > 1;
        }

        /// <summary>
        /// 在未对齐的情况下使用 RadioButton 选定筛选器时进行调用。
        /// </summary>
        /// <param name="sender">选定的 RadioButton 实例。</param>
        /// <param name="e">描述如何选定 RadioButton 的事件数据。</param>
        void Filter_Checked(object sender, RoutedEventArgs e)
        {
            var filter = (sender as FrameworkElement).DataContext;

            // 将更改镜像到 CollectionViewSource。
            // 此项很可能不需要。
            if (filtersViewSource.View != null)
            {
                filtersViewSource.View.MoveCurrentTo(filter);
            }

            // 确定选定的筛选器
            var selectedFilter = filter as Filter;
            if (selectedFilter != null)
            {
                // 将结果镜像到相应的筛选器对象中，以允许
                // 在未对齐以反映更改时使用的 RadioButton 表示形式
                selectedFilter.Active = true;

                // TODO:  通过将 this.DefaultViewModel["Results"] 设置为具有可绑定的 Image、Title 和 Subtitle 属性的项集合，
                //       具有可绑定的 Image、Title、Subtitle 和 Description 属性的项的集合

                // 确保找到结果
                object results;
                ICollection resultsCollection;
                if (this.DefaultViewModel.TryGetValue("Results", out results) &&
                    (resultsCollection = results as ICollection) != null &&
                    resultsCollection.Count != 0)
                {
                    VisualStateManager.GoToState(this, "ResultsFound", true);
                    return;
                }
            }

            // 无搜索结果时显示信息性文本。
            VisualStateManager.GoToState(this, "NoResultsFound", true);
        }

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

        /// <summary>
        /// 描述可用于查看搜索结果的筛选器之一的视图模型。
        /// </summary>
        private sealed class Filter : INotifyPropertyChanged
        {
            private String _name;
            private int _count;
            private bool _active;

            public Filter(String name, int count, bool active = false)
            {
                this.Name = name;
                this.Count = count;
                this.Active = active;
            }

            public override String ToString()
            {
                return Description;
            }

            public String Name
            {
                get { return _name; }
                set { if (this.SetProperty(ref _name, value)) this.OnPropertyChanged("Description"); }
            }

            public int Count
            {
                get { return _count; }
                set { if (this.SetProperty(ref _count, value)) this.OnPropertyChanged("Description"); }
            }

            public bool Active
            {
                get { return _active; }
                set { this.SetProperty(ref _active, value); }
            }

            public String Description
            {
                get { return String.Format("{0} ({1})", _name, _count); }
            }

            /// <summary>
            /// 针对属性更改通知的多播事件。
            /// </summary>
            public event PropertyChangedEventHandler PropertyChanged;

            /// <summary>
            /// 检查属性是否已与所需值匹配。    仅当需要时才设置
            /// 该属性并通知侦听器。
            /// </summary>
            /// <typeparam name="T">属性的类型。</typeparam>
            /// <param name="storage">对具有 getter 和 setter 的属性的引用。</param>
            /// <param name="value">属性的所需值。</param>
            /// <param name="propertyName">用于通知侦听器的属性的名称。    此
            /// 值是可选的，可以在从支持 CallerMemberName 的编译器调用时
            /// 自动提供。</param>
            /// <returns>如果更改了值，则为 true，如果现有值与所需值匹配，
            /// 则为 false。</returns>
            private bool SetProperty<T>(ref T storage, T value, [CallerMemberName] String propertyName = null)
            {
                if (object.Equals(storage, value)) return false;

                storage = value;
                this.OnPropertyChanged(propertyName);
                return true;
            }

            /// <summary>
            /// 向侦听器通知已更改了某个属性值。
            /// </summary>
            /// <param name="propertyName">用于通知侦听器的属性的名称。    此
            /// 值是可选的，可以在从支持
            /// <see cref="CallerMemberNameAttribute"/> 的编译器调用时自动提供。</param>
            private void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                var eventHandler = this.PropertyChanged;
                if (eventHandler != null)
                {
                    eventHandler(this, new PropertyChangedEventArgs(propertyName));
                }
            }

        }
    }
}
