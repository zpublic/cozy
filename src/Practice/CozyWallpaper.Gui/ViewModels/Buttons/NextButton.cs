using CozyWallpaper.Gui.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MMS.Installation
{
    public class NextButton : MMS.Installation.Button
    {
        public NextButton()
        {
            this.Text = "下一页";
            this.IsEnabled = true;
            this.ButtonVisiblity = Visibility.Collapsed;
            this.Command = new NextCommand();
        }
    }

    public class NextCommand : ICommand
    {
        //暂时这么些
        private int mCurrentId = 0;

        public delegate bool CheckCanEnabled();

        public event CheckCanEnabled IsCanEnabled;

        public bool CanExecute(object parameter)
        {
            try
            {
                return this.IsCanEnabled();
            }
            catch (Exception e)
            {
                return true;
            }
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            if (mCurrentId + 1 < MainWindowViewModel.GetInstance().WallpaperList.Count)
            {
                mCurrentId++;
                MainWindowViewModel.GetInstance().Image = MainWindowViewModel.GetInstance().WallpaperList[mCurrentId].Url.ToString();
                //MainWindowViewModel.GetInstance().WallpaperList[mCurrentId].Status = UI.Default.NavigationType.Process;
                //MainWindowViewModel.GetInstance().WallpaperList[mCurrentId - 1].Status = UI.Default.NavigationType.Wait;
                MainWindowViewModel.GetInstance().BackButton.ButtonVisiblity = Visibility.Visible;
            }
        }
    }
}
