using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfCozy.A.TriggerDemoDetails
{
  /// <summary>
  /// Interaction logic for DataTriggerWindow.xaml
  /// </summary>
  public partial class DataTriggerWindow : Window
  {
    public DataTriggerWindow()
    {
      InitializeComponent();
      list1.Items.Add(new Book { Title = "Professional C# 4.0", Publisher = "Wrox Press" });
      list1.Items.Add(new Book { Title = "C# 2010 for Dummies", Publisher = "Dummies" });
      list1.Items.Add(new Book { Title = "HTML and CSS: Design and Build Websites", Publisher = "Wiley" });
    }
  }
}
