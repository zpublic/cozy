﻿using System.Windows;
using MahApps.Metro.Controls;
using System.IO;

namespace CozyLauncher.Plugin.Guide
{
    /// <summary>
    /// GuideWindow.xaml 的交互逻辑
    /// </summary>
    public partial class GuideWindow : MetroWindow
    {
        public GuideWindow()
        {
            InitializeComponent();
        }

        public static string ContentData
        {
            get
            {
                if (File.Exists("test.json"))
                {
                    using (var fs = new FileStream("test.json", FileMode.Open))
                    {
                        using (var reader = new StreamReader(fs))
                        {
                            return reader.ReadToEnd();
                        }
                    }
                }
                return string.Empty;
            }
        }
    }
}
