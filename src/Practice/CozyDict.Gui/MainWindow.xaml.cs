using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Tesseract;

namespace CozyDict.Gui
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Test();
        }

        public void Test()
        {
            var testImagePath = "../../phototest.tif";
            try
            {
                using (var engine = new TesseractEngine(@"../../tessdata", "eng", EngineMode.Default))
                {
                    using (var img = Pix.LoadFromFile(testImagePath))
                    {
                        var i = 1;
                        using (var page = engine.Process(img))
                        {
                            var text = page.GetText();

                            using (var iter = page.GetIterator())
                            {
                                iter.Begin();
                                do
                                {
                                    if (i % 2 == 0)
                                    {
                                        do
                                        {
                                            Console.WriteLine("word: " + iter.GetText(PageIteratorLevel.Word));
                                        } while (iter.Next(PageIteratorLevel.TextLine, PageIteratorLevel.Word));
                                   
                                    }
                                    i++;
                                } while (iter.Next(PageIteratorLevel.Para, PageIteratorLevel.TextLine));
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Unexpected Error: " + e.Message);
                Console.WriteLine("Details: ");
                Console.WriteLine(e.ToString());
            }
        }
    }
}
