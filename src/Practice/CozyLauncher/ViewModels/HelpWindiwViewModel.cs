using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using CozyLauncher.Models;
using CozyLauncher.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing;
using System.Windows.Input;
using CozyLauncher.Infrastructure.MVVM;

namespace CozyLauncher.ViewModels
{
    public class HelpWindiwViewModel : BaseViewModel
    {
        public ObservableCollection<HelpContent> FunctionList { get; set; }
            = new ObservableCollection<HelpContent>();

        private int _SelectedIndex;
        public int SelectedIndex
        {
            get
            {
                return _SelectedIndex;
            }
            set
            {
                Set(ref _SelectedIndex, value);
            }
        }

        private ICommand _PrevCommand;
        public ICommand PrevCommand
        {
            get
            {
                return _PrevCommand = _PrevCommand ?? new DelegateCommand(x => 
                {
                    SelectedIndex = (SelectedIndex > 0 ? SelectedIndex - 1 : SelectedIndex);
                });
            }
        }

        private ICommand _NextCommand;
        public ICommand NextCommand
        {
            get
            {
                return _NextCommand = _NextCommand ?? new DelegateCommand(x =>
                {
                    SelectedIndex = (SelectedIndex < FunctionList.Count - 1 ? SelectedIndex + 1 : FunctionList.Count - 1);
                });
            }
        }

        private ICommand _CloseCommand;
        public ICommand CloseCommand
        {
            get
            {
                return _CloseCommand = _CloseCommand ?? new DelegateCommand(x =>
                {
                    this.OnPropertyChanged("Syscommand.Close");
                });
            }
        }

        public HelpWindiwViewModel()
        {
            LoadHelp();
        }

        private void LoadHelp()
        {
            FunctionList.Add(new HelpContent
            {
                Desc = "介绍",
                Control = new HelpControl
                {
                    Text = "CozyLauncher \n最好用最有情怀的快速启动工具。"
                }
            });
            FunctionList.Add(new HelpContent
            {
                Desc = "快捷键",
                Control = new HelpControl
                {
                    Text = "默认 Ctrl + Alt + Space 唤出主界面。"
                }
            });

            var img = new DrawingImage();

            FunctionList.Add(new HelpContent
            {
                Desc = "功能",
                Control = new HelpControl
                {
                    Text = "退出 - exit",
                    ImageHeight = 120,
                    Image = ToBitmapImage(Resource.exit),
                }
            });
        }

        public static BitmapImage ToBitmapImage(Bitmap bitmap)
        {
            using (var memory = new MemoryStream())
            {
                bitmap.Save(memory, ImageFormat.Png);
                memory.Position = 0;

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();

                return bitmapImage;
            }
        }
    }
}
