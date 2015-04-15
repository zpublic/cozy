using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace StoreCozy.Common
{
    /// <summary>
    /// NavigationHelper 协助在页面间进行导航。 它提供一些命令，用于
    /// 前后导航以及注册 Windows 中用于前进和后退的标准鼠标和键盘
    /// 导航请求快捷方式和 Windows Phone 中的
    /// 硬件“后退”按钮。此外，它集成了 SuspensionManger 以在页面之间导航时处理
    /// 进程生存期管理和状态管理。
    /// </summary>
    /// <example>
    /// 若要利用 NavigationHelper，请执行下面两步或
    /// 以 BasicPage 或除 BlankPage 以外的任何页项开始。
    /// 
    /// 1) 在某处创建一个 NavigationHelper 实例(如
    ///     页面的构造函数中)，并注册 LoadState 和
    ///     SaveState 事件的回调。
    /// <code>
    ///     public MyPage()
    ///     {
    ///         this.InitializeComponent();
    ///         var navigationHelper = new NavigationHelper(this);
    ///         this.navigationHelper.LoadState += navigationHelper_LoadState;
    ///         this.navigationHelper.SaveState += navigationHelper_SaveState;
    ///     }
    ///     
    ///     private async void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
    ///     { }
    ///     private async void navigationHelper_SaveState(object sender, LoadStateEventArgs e)
    ///     { }
    /// </code>
    /// 
    /// 2) 在以下情况下注册页面以调入 NavigationHelper: 该页面
    ///      通过重写 <see cref="Windows.UI.Xaml.Controls.Page.OnNavigatedTo"/> 
    ///     和 <see cref="Windows.UI.Xaml.Controls.Page.OnNavigatedFrom"/> 事件以参与导航。
    /// <code>
    ///     protected override void OnNavigatedTo(NavigationEventArgs e)
    ///     {
    ///         navigationHelper.OnNavigatedTo(e);
    ///     }
    ///     
    ///     protected override void OnNavigatedFrom(NavigationEventArgs e)
    ///     {
    ///         navigationHelper.OnNavigatedFrom(e);
    ///     }
    /// </code>
    /// </example>
    [Windows.Foundation.Metadata.WebHostHidden]
    public class NavigationHelper : DependencyObject
    {
        private Page Page { get; set; }
        private Frame Frame { get { return this.Page.Frame; } }

        /// <summary>
        /// 初始化 <see cref="NavigationHelper"/> 类的新实例。
        /// </summary>
        /// <param name="page">对当前页面的引用，用于导航。 
        /// 此引用可操纵帧，并确保
        /// 仅在页面占用整个窗口时产生导航请求。</param>
        public NavigationHelper(Page page)
        {
            this.Page = page;

            // 当此页是可视化树的一部分时，进行两个更改: 
            // 1) 将应用程序视图状态映射到页的可视状态
            // 2) 处理用于在 Windows 中向前和向后移动的
            this.Page.Loaded += (sender, e) =>
            {
#if WINDOWS_PHONE_APP
                Windows.Phone.UI.Input.HardwareButtons.BackPressed += HardwareButtons_BackPressed;
#else
                // 仅当占用整个窗口时，键盘和鼠标导航才适用
                if (this.Page.ActualHeight == Window.Current.Bounds.Height &&
                    this.Page.ActualWidth == Window.Current.Bounds.Width)
                {
                    // 直接侦听窗口，因此无需焦点
                    Window.Current.CoreWindow.Dispatcher.AcceleratorKeyActivated +=
                        CoreDispatcher_AcceleratorKeyActivated;
                    Window.Current.CoreWindow.PointerPressed +=
                        this.CoreWindow_PointerPressed;
                }
#endif
            };

            // 当页不再可见时，撤消相同更改
            this.Page.Unloaded += (sender, e) =>
            {
#if WINDOWS_PHONE_APP
                Windows.Phone.UI.Input.HardwareButtons.BackPressed -= HardwareButtons_BackPressed;
#else
                Window.Current.CoreWindow.Dispatcher.AcceleratorKeyActivated -=
                    CoreDispatcher_AcceleratorKeyActivated;
                Window.Current.CoreWindow.PointerPressed -=
                    this.CoreWindow_PointerPressed;
#endif
            };
        }

        #region 导航支持

        RelayCommand _goBackCommand;
        RelayCommand _goForwardCommand;

        /// <summary>
        /// 如果 Frame 管理其导航历史记录，则 <see cref="RelayCommand"/> 用于绑定到后退按钮的 Command 属性
        /// 以导航到后退导航历史记录中的最新项
        ///。
        /// 
        /// <see cref="RelayCommand"/> 被设置为使用虚拟方法 <see cref="GoBack"/>
        /// 作为执行操作，并将 <see cref="CanGoBack"/> 用于 CanExecute。
        /// </summary>
        public RelayCommand GoBackCommand
        {
            get
            {
                if (_goBackCommand == null)
                {
                    _goBackCommand = new RelayCommand(
                        () => this.GoBack(),
                        () => this.CanGoBack());
                }
                return _goBackCommand;
            }
            set
            {
                _goBackCommand = value;
            }
        }
        /// <summary>
        /// 如果 Frame 管理其导航历史记录，则 <see cref="RelayCommand"/> 用于导航到
        /// 前进历史记录中的最新项。
        /// 
        /// <see cref="RelayCommand"/> 被设置为使用虚拟方法 <see cref="GoForward"/>
        /// 作为执行操作，并将 <see cref="CanGoForward"/> 用于 CanExecute。
        /// </summary>
        public RelayCommand GoForwardCommand
        {
            get
            {
                if (_goForwardCommand == null)
                {
                    _goForwardCommand = new RelayCommand(
                        () => this.GoForward(),
                        () => this.CanGoForward());
                }
                return _goForwardCommand;
            }
        }

        /// <summary>
        /// <see cref="GoBackCommand"/> 属性使用的虚拟方法，用于
        /// 确定 <see cref="Frame"/> 能否后退。
        /// </summary>
        /// <returns>
        /// 如果 <see cref="Frame"/> 至少在
        /// 后退导航历史记录中有一个条目，则为 true。
        /// </returns>
        public virtual bool CanGoBack()
        {
            return this.Frame != null && this.Frame.CanGoBack;
        }
        /// <summary>
        /// <see cref="GoForwardCommand"/> 属性使用的虚拟方法，用于
        /// 确定 <see cref="Frame"/> 能否前进。
        /// </summary>
        /// <returns>
        /// 如果 <see cref="Frame"/> 至少在
        /// 前进导航历史记录中有一个条目，则为 true。
        /// </returns>
        public virtual bool CanGoForward()
        {
            return this.Frame != null && this.Frame.CanGoForward;
        }

        /// <summary>
        /// <see cref="GoBackCommand"/> 属性使用的虚拟方法，用于
        /// 调用 <see cref="Windows.UI.Xaml.Controls.Frame.GoBack"/> 方法。
        /// </summary>
        public virtual void GoBack()
        {
            if (this.Frame != null && this.Frame.CanGoBack) this.Frame.GoBack();
        }
        /// <summary>
        /// <see cref="GoForwardCommand"/> 属性使用的虚拟方法，用于
        /// 调用 <see cref="Windows.UI.Xaml.Controls.Frame.GoForward"/> 方法。
        /// </summary>
        public virtual void GoForward()
        {
            if (this.Frame != null && this.Frame.CanGoForward) this.Frame.GoForward();
        }

#if WINDOWS_PHONE_APP
        /// <summary>
        /// 在按下硬件“后退”按钮时调用。仅适用于 Windows Phone。
        /// </summary>
        /// <param name="sender">触发事件的实例。</param>
        /// <param name="e">描述导致事件的条件的事件数据。</param>
        private void HardwareButtons_BackPressed(object sender, Windows.Phone.UI.Input.BackPressedEventArgs e)
        {
            if (this.GoBackCommand.CanExecute(null))
            {
                e.Handled = true;
                this.GoBackCommand.Execute(null);
            }
        }
#else
        /// <summary>
        /// 当此页处于活动状态并占用整个窗口时，在每次
        /// 击键(包括系统键，如 Alt 组合键)时调用。    用于检测页之间的键盘
        /// 导航(即使在页本身没有焦点时)。
        /// </summary>
        /// <param name="sender">触发事件的实例。</param>
        /// <param name="e">描述导致事件的条件的事件数据。</param>
        private void CoreDispatcher_AcceleratorKeyActivated(CoreDispatcher sender,
            AcceleratorKeyEventArgs e)
        {
            var virtualKey = e.VirtualKey;

            // 仅当按向左、向右或专用上一页或下一页键时才进一步
            // 调查
            if ((e.EventType == CoreAcceleratorKeyEventType.SystemKeyDown ||
                e.EventType == CoreAcceleratorKeyEventType.KeyDown) &&
                (virtualKey == VirtualKey.Left || virtualKey == VirtualKey.Right ||
                (int)virtualKey == 166 || (int)virtualKey == 167))
            {
                var coreWindow = Window.Current.CoreWindow;
                var downState = CoreVirtualKeyStates.Down;
                bool menuKey = (coreWindow.GetKeyState(VirtualKey.Menu) & downState) == downState;
                bool controlKey = (coreWindow.GetKeyState(VirtualKey.Control) & downState) == downState;
                bool shiftKey = (coreWindow.GetKeyState(VirtualKey.Shift) & downState) == downState;
                bool noModifiers = !menuKey && !controlKey && !shiftKey;
                bool onlyAlt = menuKey && !controlKey && !shiftKey;

                if (((int)virtualKey == 166 && noModifiers) ||
                    (virtualKey == VirtualKey.Left && onlyAlt))
                {
                    // 在按上一页键或 Alt+向左键时向后导航
                    e.Handled = true;
                    this.GoBackCommand.Execute(null);
                }
                else if (((int)virtualKey == 167 && noModifiers) ||
                    (virtualKey == VirtualKey.Right && onlyAlt))
                {
                    // 在按下一页键或 Alt+向右键时向前导航
                    e.Handled = true;
                    this.GoForwardCommand.Execute(null);
                }
            }
        }

        /// <summary>
        /// 当此页处于活动状态并占用整个窗口时，在每次鼠标单击、触摸屏点击
        /// 或执行等效交互时调用。    用于检测浏览器样式下一页和
        /// 上一步鼠标按钮单击以在页之间导航。
        /// </summary>
        /// <param name="sender">触发事件的实例。</param>
        /// <param name="e">描述导致事件的条件的事件数据。</param>
        private void CoreWindow_PointerPressed(CoreWindow sender,
            PointerEventArgs e)
        {
            var properties = e.CurrentPoint.Properties;

            // 忽略与鼠标左键、右键和中键的键关联
            if (properties.IsLeftButtonPressed || properties.IsRightButtonPressed ||
                properties.IsMiddleButtonPressed) return;

            // 如果按下后退或前进(但不是同时)，则进行相应导航
            bool backPressed = properties.IsXButton1Pressed;
            bool forwardPressed = properties.IsXButton2Pressed;
            if (backPressed ^ forwardPressed)
            {
                e.Handled = true;
                if (backPressed) this.GoBackCommand.Execute(null);
                if (forwardPressed) this.GoForwardCommand.Execute(null);
            }
        }
#endif

        #endregion

        #region 进程生命期管理

        private String _pageKey;

        /// <summary>
        /// 在当前页上注册此事件以向该页填入
        /// 在导航过程中传递的内容以及任何
        /// 在从以前的会话重新创建页时提供的已保存状态。
        /// </summary>
        public event LoadStateEventHandler LoadState;
        /// <summary>
        /// 在当前页上注册此事件以保留
        /// 与当前页关联的状态，以防
        /// 应用程序挂起或从导航缓存中丢弃
        /// 该页。
        /// </summary>
        public event SaveStateEventHandler SaveState;

        /// <summary>
        /// 即将在 Frame 中显示此页时调用。 
        /// 此方法调用 <see cref="LoadState"/>，应在此处放置所有
        /// 导航和进程生命周期管理逻辑。
        /// </summary>
        /// <param name="e">描述如何访问此页的事件数据。    Parameter
        /// 属性提供要显示的组。</param>
        public void OnNavigatedTo(NavigationEventArgs e)
        {
            var frameState = SuspensionManager.SessionStateForFrame(this.Frame);
            this._pageKey = "Page-" + this.Frame.BackStackDepth;

            if (e.NavigationMode == NavigationMode.New)
            {
                // 在向导航堆栈添加新页时清除向前导航的
                // 现有状态
                var nextPageKey = this._pageKey;
                int nextPageIndex = this.Frame.BackStackDepth;
                while (frameState.Remove(nextPageKey))
                {
                    nextPageIndex++;
                    nextPageKey = "Page-" + nextPageIndex;
                }

                // 将导航参数传递给新页
                if (this.LoadState != null)
                {
                    this.LoadState(this, new LoadStateEventArgs(e.Parameter, null));
                }
            }
            else
            {
                // 通过将相同策略用于加载挂起状态并从缓存重新创建
                // 放弃的页，将导航参数和保留页状态传递
                // 给页
                if (this.LoadState != null)
                {
                    this.LoadState(this, new LoadStateEventArgs(e.Parameter, (Dictionary<String, Object>)frameState[this._pageKey]));
                }
            }
        }

        /// <summary>
        /// 当此页不再在 Frame 中显示时调用。
        /// 此方法调用 <see cref="SaveState"/>，应在此处放置所有
        /// 导航和进程生命周期管理逻辑。
        /// </summary>
        /// <param name="e">描述如何访问此页的事件数据。    Parameter
        /// 属性提供要显示的组。</param>
        public void OnNavigatedFrom(NavigationEventArgs e)
        {
            var frameState = SuspensionManager.SessionStateForFrame(this.Frame);
            var pageState = new Dictionary<String, Object>();
            if (this.SaveState != null)
            {
                this.SaveState(this, new SaveStateEventArgs(pageState));
            }
            frameState[_pageKey] = pageState;
        }

        #endregion
    }

    /// <summary>
    /// 代表将处理 <see cref="NavigationHelper.LoadState"/> 事件的方法
    /// </summary>
    public delegate void LoadStateEventHandler(object sender, LoadStateEventArgs e);
    /// <summary>
    /// 代表将处理 <see cref="NavigationHelper.SaveState"/> 事件的方法
    /// </summary>
    public delegate void SaveStateEventHandler(object sender, SaveStateEventArgs e);

    /// <summary>
    /// 一个类，用于存放在某页尝试加载状态时所需的事件数据。
    /// </summary>
    public class LoadStateEventArgs : EventArgs
    {
        /// <summary>
        /// 最初请求此页时传递给 <see cref="Frame.Navigate(Type, Object)"/> 
        /// 的参数值。
        /// </summary>
        public Object NavigationParameter { get; private set; }
        /// <summary>
        /// 此页在以前会话期间保留的状态
        /// 的字典。 首次访问某页时，此项将为 null。
        /// </summary>
        public Dictionary<string, Object> PageState { get; private set; }

        /// <summary>
        /// 初始化 <see cref="LoadStateEventArgs"/> 类的新实例。
        /// </summary>
        /// <param name="navigationParameter">
        /// 最初请求此页时传递给 <see cref="Frame.Navigate(Type, Object)"/> 
        /// 的参数值。
        /// </param>
        /// <param name="pageState">
        /// 此页在以前会话期间保留的状态
        /// 的字典。 首次访问某页时，此项将为 null。
        /// </param>
        public LoadStateEventArgs(Object navigationParameter, Dictionary<string, Object> pageState)
            : base()
        {
            this.NavigationParameter = navigationParameter;
            this.PageState = pageState;
        }
    }
    /// <summary>
    /// 一个类，用于存放在某页尝试保存状态时所需的事件数据。
    /// </summary>
    public class SaveStateEventArgs : EventArgs
    {
        /// <summary>
        /// 要填入可序列化状态的空字典。
        /// </summary>
        public Dictionary<string, Object> PageState { get; private set; }

        /// <summary>
        /// 初始化 <see cref="SaveStateEventArgs"/> 类的新实例。
        /// </summary>
        /// <param name="pageState">要使用可序列化状态填充的空字典。</param>
        public SaveStateEventArgs(Dictionary<string, Object> pageState)
            : base()
        {
            this.PageState = pageState;
        }
    }
}
