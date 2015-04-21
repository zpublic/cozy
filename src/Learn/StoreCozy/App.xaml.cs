using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using StoreCozy.Repositories;
using StoreCozy.Model;
using StoreCozy.Storage;

// “空白应用程序”模板在 http://go.microsoft.com/fwlink/?LinkId=234227 上有介绍

namespace StoreCozy
{
    /// <summary>
    /// 提供特定于应用程序的行为，以补充默认的应用程序类。
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// 初始化单一实例应用程序对象。    这是执行的创作代码的第一行，
        /// 逻辑上等同于 main() 或 WinMain()。
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        /// <summary>
        /// 在应用程序由最终用户正常启动时进行调用。
        /// 以打开特定文件等情况下使用其他入口点。
        /// </summary>
        /// <param name="e">有关启动请求和过程的详细信息。</param>
        protected async override void OnLaunched(LaunchActivatedEventArgs e)
        {

#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                this.DebugSettings.EnableFrameRateCounter = true;
            }
#endif
            await InitSampleDataAsync();

            Frame rootFrame = Window.Current.Content as Frame;

            // 不要在窗口已包含内容时重复应用程序初始化，
            // 只需确保窗口处于活动状态
            if (rootFrame == null)
            {
                // 创建要充当导航上下文的框架，并导航到第一页
                rootFrame = new Frame();
                //设置默认语言
                rootFrame.Language = Windows.Globalization.ApplicationLanguages.Languages[0];

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO:  从之前挂起的应用程序加载状态
                }

                // 将框架放在当前窗口中
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
                // 当未还原导航堆栈时，导航到第一页，
                // 并通过将所需信息作为导航参数传入来配置
                // 参数
                rootFrame.Navigate(typeof(MainPage), e.Arguments);
            }
            // 确保当前窗口处于活动状态
            Window.Current.Activate();
        }

        /// <summary>
        ///导航到特定页失败时调用
        /// </summary>
        ///<param name="sender">导航失败的框架</param>
        ///<param name="e">有关导航失败的详细信息</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// 在将要挂起应用程序执行时调用。    在不知道应用程序
        /// 将被终止还是恢复的情况下保存应用程序状态，
        /// 并让内存内容保持不变。
        /// </summary>
        /// <param name="sender">挂起的请求的源。</param>
        /// <param name="e">有关挂起的请求的详细信息。</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO:  保存应用程序状态并停止任何后台活动
            deferral.Complete();
        }

        /// <summary>
        /// 在将应用程序作为共享操作的目标激活时调用。
        /// </summary>
        /// <param name="e">有关激活请求的详细信息。</param>
        protected override void OnShareTargetActivated(Windows.ApplicationModel.Activation.ShareTargetActivatedEventArgs e)
        {
            var shareTargetPage = new StoreCozy.ShareTargetPage1();
            shareTargetPage.Activate(e);
        }

        /// <summary>
        /// 在应用程序激活以显示文件打开选取器时调用。
        /// </summary>
        /// <param name="e">有关激活请求的详细信息。</param>
        protected override void OnFileOpenPickerActivated(Windows.ApplicationModel.Activation.FileOpenPickerActivatedEventArgs e)
        {
            var fileOpenPickerPage = new StoreCozy.FileOpenPickerPage1();
            fileOpenPickerPage.Activate(e);
        }

        private static async Task InitSampleDataAsync()
        {
            var storage = new MenuCardStorage();
            var imageStorage = new MenuCardImageStorage();
            if (await storage.IsRoamingFolderEmpty())
            {
                List<MenuCard> menuCards = MenuCardRepository.GetSampleMenuCards().ToList();
                foreach (var card in menuCards)
                {
                    RandomAccessStreamReference streamRef =
                      RandomAccessStreamReference.CreateFromUri(new Uri(card.ImagePath));
                    using (IRandomAccessStreamWithContentType stream =
                      await streamRef.OpenReadAsync())
                    {
                        card.ImagePath = string.Format("{0}.png", Guid.NewGuid());
                        await imageStorage.WriteImageAsync(stream, card.ImagePath);
                    }
                }
                await storage.WriteMenuCardsAsync(menuCards);
            }
        }
    }
}
