using StoreCozy.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// “共享目标合同”项模板在 http://go.microsoft.com/fwlink/?LinkId=234241 上提供

namespace StoreCozy
{
    /// <summary>
    /// 此页允许其他应用程序共享此应用程序中的内容。
    /// </summary>
    public sealed partial class ShareTargetPage1 : Page
    {
        /// <summary>
        /// 提供与 Windows 就共享操作进行沟通的渠道。
        /// </summary>
        private Windows.ApplicationModel.DataTransfer.ShareTarget.ShareOperation _shareOperation;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        /// <summary>
        /// 可将其更改为强类型视图模型。
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        public ShareTargetPage1()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// 在其他应用程序想要共享此应用程序中的内容时进行调用。
        /// </summary>
        /// <param name="e">用于与 Windows 协调进程的激活数据。</param>
        public async void Activate(ShareTargetActivatedEventArgs e)
        {
            this._shareOperation = e.ShareOperation;

            // 通过视图模型沟通关于共享内容的元数据
            var shareProperties = this._shareOperation.Data.Properties;
            var thumbnailImage = new BitmapImage();
            this.DefaultViewModel["Title"] = shareProperties.Title;
            this.DefaultViewModel["Description"] = shareProperties.Description;
            this.DefaultViewModel["Image"] = thumbnailImage;
            this.DefaultViewModel["Sharing"] = false;
            this.DefaultViewModel["ShowImage"] = false;
            this.DefaultViewModel["Comment"] = String.Empty;
            this.DefaultViewModel["Placeholder"] = "Add a comment";
            this.DefaultViewModel["SupportsComment"] = true;
            Window.Current.Content = this;
            Window.Current.Activate();

            // 在后台更新共享内容的缩略图
            if (shareProperties.Thumbnail != null)
            {
                var stream = await shareProperties.Thumbnail.OpenReadAsync();
                thumbnailImage.SetSource(stream);
                this.DefaultViewModel["ShowImage"] = true;
            }
        }

        /// <summary>
        /// 在用户单击“共享”按钮时进行调用。
        /// </summary>
        /// <param name="sender">用于启动共享的 Button 实例。</param>
        /// <param name="e">描述如何单击按钮的事件数据。</param>
        private void ShareButton_Click(object sender, RoutedEventArgs e)
        {
            this.DefaultViewModel["Sharing"] = true;
            this._shareOperation.ReportStarted();

            // TODO:  使用 this._shareOperation.Data 执行适合您的共享方案的工作，
            //       通常通过添加到此页的自定义用户界面元素
            //       通过添加到此页的自定义用户界面元素，例如 
            //       this.DefaultViewModel["Comment"]

            this._shareOperation.ReportCompleted();
        }
    }
}
