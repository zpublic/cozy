using CozyAnywhere.ClientCore;
using CozyAnywhere.ClientCore.EventArg;
using System.IO;
using System.Windows.Media.Imaging;
using CozyAnywhere.WpfClient.Command;
using System.Windows.Input;

namespace CozyAnywhere.WpfClient.ViewModel
{
    public class CapturePluginPageViewModel : BaseViewModel
    {
        public AnywhereClient clientCore { get; set; }

        private BitmapImage _CaptureImage;

        public BitmapImage CaptureImage
        {
            get
            {
                return _CaptureImage;
            }
            set
            {
                Set(ref _CaptureImage, value, "CaptureImage");
            }
        }

        private ICommand _RefreshCommand;
        public ICommand RefreshCommand
        {
            get
            {
                return _RefreshCommand = _RefreshCommand ?? new DelegateCommand((x) =>
                {
                    if (clientCore != null)
                    {
                        clientCore.SendCaptureMessage();
                    }
                });
            }
        }

        public CapturePluginPageViewModel()
        {
            clientCore = MainWindowViewModel.clientCore;
            clientCore.CaptureRefreshHandler += OnCaptureRefresh;
        }

        private void OnCaptureRefresh(object sender, CaptureRefreshEventArgs msg)
        {
            using (MemoryStream ms = new MemoryStream(msg.Data))
            {
                BitmapImage bm = new BitmapImage();
                bm.BeginInit();
                bm.StreamSource = ms;
                bm.CacheOption = BitmapCacheOption.OnLoad;
                bm.EndInit();
                CaptureImage = bm;
            }
        }
    }
}