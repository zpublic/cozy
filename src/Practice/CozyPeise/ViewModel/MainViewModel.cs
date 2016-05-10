using CozyPeise.Models;
using GalaSoft.MvvmLight;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace CozyPeise.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            if (File.Exists("Palette.dat"))
            {
                using (var fs = new FileStream("Palette.dat", FileMode.Open, FileAccess.Read))
                {
                    using (var reader = new StreamReader(fs))
                    {
                        var res = JsonConvert.DeserializeObject<List<Palette>>(reader.ReadToEnd());
                        _palettes = new ObservableCollection<PeiseListItemViewModel>();
                        foreach (var i in res)
                            _palettes.Add(new PeiseListItemViewModel(i));
                    }
                }
            }
            if (_palettes.Count > 0)
            {
                _curpeiselistitem = _palettes[0];
                PeiseDisplayViewModel vm = new PeiseDisplayViewModel(_curpeiselistitem.Palette);
                _curpeisedisplay = vm;
            }
        }

        public string Title { get; } = "ÅäÉ«";

        ObservableCollection<PeiseListItemViewModel> _palettes;
        public ObservableCollection<PeiseListItemViewModel> Palettes
        {
            get
            {
                return _palettes;
            }
            set
            {
                Set("Palettes", ref value);
            }
        }

        PeiseListItemViewModel _curpeiselistitem;
        public PeiseListItemViewModel CurPeiseListItem
        {
            get { return _curpeiselistitem; }
            set
            {
                PeiseDisplayViewModel vm = new PeiseDisplayViewModel(value.Palette);
                _curpeisedisplay = vm;
                Set("CurPeiseDisplay", ref vm);
            }
        }

        PeiseDisplayViewModel _curpeisedisplay;
        public PeiseDisplayViewModel CurPeiseDisplay
        {
            get
            {
                return _curpeisedisplay;
            }
        }
    }
}