using CozyAnywhere.ClientCore;
using CozyAnywhere.ClientCore.EventArg;
using System.IO;
using System.Windows.Media.Imaging;
using CozyAnywhere.WpfClient.Command;
using System.Windows.Input;
using System.Drawing;
using CozyAnywhere.WpfClient.Ext;
using System.Drawing.Imaging;

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

        public Bitmap GlobalBMP { get; set; }
        public BitmapLocker GlobalLocker { get; set; }

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
            clientCore      = MainWindowViewModel.clientCore;
            clientCore.CaptureRefreshHandler += OnCaptureRefresh;

            GlobalBMP       = new Bitmap(((1366 + 127) / 128 * 128), ((768 + 127) / 128) * 128);
            GlobalLocker    = new BitmapLocker(GlobalBMP);
        }

        private void OnCaptureRefresh(object sender, CaptureRefreshEventArgs msg)
        {
            using (MemoryStream ms = new MemoryStream(msg.Data))
            {
                int x = msg.MetaData.Item1;
                int y = msg.MetaData.Item2;

                Bitmap recvbmp          = (Bitmap)Image.FromStream(ms);
                BitmapLocker recvdata   = new BitmapLocker(recvbmp);

                recvdata.LockBits();
                GlobalLocker.LockBits();


                for(int i = 0; i < recvdata.Width; ++i)
                {
                    for (int j = 0; j < recvdata.Height; ++j)
                    {
                        GlobalLocker.SetPixel(i + x, j + y, recvdata.GetPixel(i, j));
                    }
                }

                GlobalLocker.UnlockBits();
                recvdata.UnlockBits();

                using (MemoryStream ims = new MemoryStream())
                {
                    GlobalBMP.Save(ims, ImageFormat.Bmp);
                    ims.Seek(0, SeekOrigin.Begin);

                    BitmapImage bm  = new BitmapImage();
                    bm.BeginInit();
                    bm.StreamSource = ims;
                    bm.CacheOption  = BitmapCacheOption.OnLoad;
                    bm.EndInit();
                    CaptureImage    = bm;
                }
            }
        }
    }
}