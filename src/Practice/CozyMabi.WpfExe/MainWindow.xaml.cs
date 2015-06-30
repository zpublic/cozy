using CozyMabi.Core;
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
using CozyMabi.Core.Request;

namespace CozyMabi.WpfExe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AccountRequest account = new AccountRequest();
            account.Login("zapline", "fuckjava");

            SocialRequest social = new SocialRequest();
            social.Maopao("hehe @kingwl @MaxTan", "iPhone233");
            social.SendMessage("MaxTan", "hehe");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AccountRequest account = new AccountRequest();
            account.Login("zapline", "000000");

            SocialRequest social = new SocialRequest();
            social.GetFollowers(1, 500);
            social.GetFriends(1, 500);
            social.UnFollow("kingwl");
            social.Follow("kingwl");
        }
    }
}
