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
using Windows.UI.Xaml.Navigation;

// “文件打开选取器合同”项模板在 http://go.microsoft.com/fwlink/?LinkId=234239 上提供

namespace StoreCozy
{
    /// <summary>
    /// 此页显示该应用程序拥有的文件，以便用户可以授权其他应用程序
    /// 访问这些文件。
    /// </summary>
    public sealed partial class FileOpenPickerPage1 : Page
    {
        /// <summary>
        /// 在 Windows UI 中添加或移除文件以让 Windows 知道已选定的内容。
        /// </summary>
        private Windows.Storage.Pickers.Provider.FileOpenPickerUI _fileOpenPickerUI;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        /// <summary>
        /// 可将其更改为强类型视图模型。
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        public FileOpenPickerPage1()
        {
            this.InitializeComponent();
            Window.Current.SizeChanged += Window_SizeChanged;
            InvalidateVisualState();
        }

        void Window_SizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
            this.InvalidateVisualState();
        }

        private void InvalidateVisualState()
        {
            var visualState = DetermineVisualState();
            VisualStateManager.GoToState(this, visualState, false);
        }

        private string DetermineVisualState()
        {
            return Window.Current.Bounds.Width >= 500 ? "HorizontalView" : "VerticalView";
        }

        /// <summary>
        /// 在其他应用程序想要打开此应用程序中的文件时进行调用。
        /// </summary>
        /// <param name="e">用于与 Windows 协调进程的激活数据。</param>
        public void Activate(FileOpenPickerActivatedEventArgs e)
        {
            this._fileOpenPickerUI = e.FileOpenPickerUI;
            _fileOpenPickerUI.FileRemoved += this.FilePickerUI_FileRemoved;

            // TODO:  将 this.DefaultViewModel["Files"] 设置为显示一个项集合，
            //       其中每个项都应有可绑定的 Image、Title 和 Description

            this.DefaultViewModel["CanGoUp"] = false;
            Window.Current.Content = this;
            Window.Current.Activate();
        }

        /// <summary>
        /// 当用户从选取器框中移除某一项目时调用
        /// </summary>
        /// <param name="sender">用于包含可用文件的 FileOpenPickerUI 实例。</param>
        /// <param name="e">描述已移除文件的事件数据。</param>
        private void FilePickerUI_FileRemoved(Windows.Storage.Pickers.Provider.FileOpenPickerUI sender, Windows.Storage.Pickers.Provider.FileRemovedEventArgs e)
        {
            // TODO:  响应在选取器 UI 中取消选择的项。
        }

        /// <summary>
        /// 在选定的文件集合发生更改时进行调用。
        /// </summary>
        /// <param name="sender">用于显示可用文件的 GridView 实例。</param>
        /// <param name="e">描述选择内容如何发生更改的事件数据。</param>
        private void FileGridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // TODO:  使用 this._fileOpenPickerUI.AddFile 和 this._fileOpenPickerUI.RemoveFile
            //       更新 Windows UI
        }

        /// <summary>
        /// 在单击“转到上级”按钮时进行调用，并指示用户希望在文件
        /// 的层次结构中提升一个级别。
        /// </summary>
        /// <param name="sender">用于表示“Go up”命令的 Button 实例。</param>
        /// <param name="e">描述如何单击按钮的事件数据。</param>
        private void GoUpButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO:  将 this.DefaultViewModel["CanGoUp"] 设置为 true 以启用相应的命令，
            //       使用 this.DefaultViewModel["Files"] 的更新以反映文件层次结构遍历
        }
    }
}
