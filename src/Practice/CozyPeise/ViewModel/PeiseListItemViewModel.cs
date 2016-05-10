using CozyPeise.Models;
using GalaSoft.MvvmLight;

namespace CozyPeise.ViewModel
{
    public class PeiseListItemViewModel : ViewModelBase
    {
        public PeiseListItemViewModel(Palette p)
        {
            _palette = p;
        }

        Palette _palette;
        public Palette Palette
        {
            get { return _palette; }
            set { _palette = value; }
        }

        public string Name { get { return _palette.Name; } }
        public string Url { get { return _palette.Url; } }

        string _defautColor = "#FFFFFF";
        public string Color0
        {
            get
            {
                if (_palette?.RGB?.Count > 0)
                    return _palette.RGB[0];
                return _defautColor;
            }
        }
        public string Color1
        {
            get
            {
                if (_palette?.RGB?.Count > 1)
                    return _palette.RGB[1];
                return _defautColor;
            }
        }
        public string Color2
        {
            get
            {
                if (_palette?.RGB?.Count > 2)
                    return _palette.RGB[2];
                return _defautColor;
            }
        }
        public string Color3
        {
            get
            {
                if (_palette?.RGB?.Count > 3)
                    return _palette.RGB[3];
                return _defautColor;
            }
        }
        public string Color4
        {
            get
            {
                if (_palette?.RGB?.Count > 4)
                    return _palette.RGB[4];
                return _defautColor;
            }
        }
        public string Color5
        {
            get
            {
                if (_palette?.RGB?.Count > 5)
                    return _palette.RGB[5];
                return _defautColor;
            }
        }
        public string Color6
        {
            get
            {
                if (_palette?.RGB?.Count > 6)
                    return _palette.RGB[6];
                return _defautColor;
            }
        }
        public string Color7
        {
            get
            {
                if (_palette?.RGB?.Count > 7)
                    return _palette.RGB[7];
                return _defautColor;
            }
        }
    }
}