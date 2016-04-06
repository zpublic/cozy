using CozyWallpaper.Core;
using CozyWallpaper.Gui.ViewModels;
using MMS.UI.Default;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MMS.Installation
{
    public class UpdateButton : MMS.Installation.Button
    {
        public UpdateButton()
        {
            this.Text = "更  新";
            this.IsEnabled = true;
            this.ButtonVisiblity = Visibility.Visible;
            this.Command = new UpdateCommand();
        }
    }

    public class UpdateCommand : ICommand
    {

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            var images = new ZhuokuWallpaperWebSite().GetWallpaper();
            List<ImageInfo> temp = new List<ImageInfo>();
            foreach (var image in images)
            {
                try
                {
                    Uri u = new Uri(image.Url);
                    ImageInfo item = new ImageInfo()
                    {
                        Title = image.Title,
                        Url = image.Url,
                        DownloadImage = new DownloadCommand(),
                        SetWallpaper = new SetWallpaperCommand()
                    };
                    temp.Add(item);
                }
                catch(Exception e)
                {

                }
            }
            MainWindowViewModel.GetInstance().WallpaperList = temp;
            MainWindowViewModel.GetInstance().Image = temp[0].Url.ToString();
        }
    }
}
