using CozyPlague.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using CozyPlague.Models;
using System.IO;
using Newtonsoft.Json;
using CozyPlague.Ext;

namespace CozyPlague.ViewModels
{
    public class MainWindowViewModel : NotifyObject
    {
        public ExtObservableCollection<Palette> PaletteCollection { get; set; } = new ExtObservableCollection<Palette>();
        public ExtObservableCollection<UserColor> UserColorCollection { get; set; } = new ExtObservableCollection<UserColor>();

        private Palette _SelectedPalette;
        public Palette SelectedPalette
        {
            get { return _SelectedPalette; }
            set { Set(ref _SelectedPalette, value); }
        }

        private Palette _SelectedUserColor;
        public Palette SelectedUserColor
        {
            get { return _SelectedUserColor; }
            set { Set(ref _SelectedUserColor, value); }
        }

        public MainWindowViewModel()
        {
            LoadData();
        }

        private void LoadData()
        {
            if(File.Exists("Palette.dat"))
            {
                using (var fs = new FileStream("Palette.dat", FileMode.Open, FileAccess.Read))
                {
                    using (var reader = new StreamReader(fs))
                    {
                        var res = JsonConvert.DeserializeObject<List<Palette>>(reader.ReadToEnd());
                        PaletteCollection.AddRange(res);
                    }
                }
            }

            if(File.Exists("Color.dat"))
            {
                using (var fs = new FileStream("Color.dat", FileMode.Open, FileAccess.Read))
                {
                    using (var reader = new StreamReader(fs))
                    {
                        var res = JsonConvert.DeserializeObject<List<UserColor>>(reader.ReadToEnd());
                        UserColorCollection.AddRange(res);
                    }
                }
            }
        }
    }
}
