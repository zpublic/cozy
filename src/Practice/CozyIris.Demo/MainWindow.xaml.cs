using ImageProcessor;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace CozyIris.Demo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            byte[] photoBytes = File.ReadAllBytes(@"C:\Users\Administrator\Pictures\wwl.jpg");
            BitmapImage bim = new BitmapImage();
            bim.BeginInit();
            bim.StreamSource = new MemoryStream(photoBytes);
            bim.EndInit();
            image.Source = bim;
        }
    }
}
