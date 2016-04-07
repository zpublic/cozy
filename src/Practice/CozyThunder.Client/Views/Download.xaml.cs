using Common.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
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

namespace CozyThunder.Client.Views
{
    /// <summary>
    /// Download.xaml 的交互逻辑
    /// </summary>
    public partial class Download : Page
    {
        private static readonly Logger mLog = Logger.GetInstance(MethodBase.GetCurrentMethod().DeclaringType);

        public Download()
        {
            try
            {
                mLog.Info("Start load dowmload page.");
                InitializeComponent();
                this.DataContext = DownloadViewModel.GetInstance();
                Test();
            }
            catch (Exception e)
            {
                mLog.Error("An error has occurred in the init download xaml,error:{0}", e.ToString());
                MMS.UI.Default.MessageBox.Error(String.Format("初始化下载页面出现异常.错误：{0}", e.Message));
            }
        }

        public void Test()
        {
            Thread t = new Thread(() =>
            {
                List<int> nums = new List<int>();
                Random random = new Random();
                int num = -1;
                string[] colorStr = new string[] { "0","1", "2", "3", "4", "5", "6", "7", "8", "9", "a", "b", "c", "d", "e", "f" };
                for (int i = 0; i < DownloadViewModel.GetInstance().PointCount; i++)
                {
                    while (num == -1 || nums.Contains(num))
                    {
                        num = random.Next(0, DownloadViewModel.GetInstance().PointCount);
                    }
                    string color = "#";
                    for (int n = 0; n < 6; n++)
                    {
                        color += colorStr[random.Next(0, colorStr.Count())];
                    }
                    DownloadViewModel.GetInstance().UpdatePointByIndex(num, color);
                    Thread.Sleep(50);
                    nums.Add(num);
                }
            });
            t.IsBackground = true;
            t.Start();
        }
    }
}
