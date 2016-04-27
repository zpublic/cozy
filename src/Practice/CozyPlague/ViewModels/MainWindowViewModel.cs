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

namespace CozyPlague.ViewModels
{
    public class MainWindowViewModel : NotifyObject
    {
        public ObservableCollection<Palette> PaletteCollection { get; set; } = new ObservableCollection<Palette>();
        public ObservableCollection<UserColor> UserColorCollection { get; set; } = new ObservableCollection<UserColor>();

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
                        var res = JsonConvert.DeserializeObject<ObservableCollection<Palette>>(reader.ReadToEnd());
                        foreach(var obj in res)
                        {
                            PaletteCollection.Add(obj);
                        }
                    }
                }
            }

            if(File.Exists("Color.dat"))
            {
                using (var fs = new FileStream("Color.dat", FileMode.Open, FileAccess.Read))
                {
                    using (var reader = new StreamReader(fs))
                    {
                        var res = JsonConvert.DeserializeObject<ObservableCollection<UserColor>>(reader.ReadToEnd());
                        foreach (var obj in res)
                        {
                            UserColorCollection.Add(obj);
                        }
                    }
                }
            }
        }
    }
}
