using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyGod.Model
{
    public class Card
    {
        public static Card Empty
        {
            get
            {
                return new Card()
                {
                    CN_Name = "无",
                    Name    = "Null",
                    Level   = 0,
                    Picture = string.Empty,
                };
            }
        }

        private string _Name;
        private string _CN_Name;
        private int _Level;
        private string _Picture;

        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
                OnPropertyChanged();
            }
        }

        public string CN_Name
        {
            get
            {
                return _CN_Name;
            }
            set
            {
                _CN_Name = value;
                OnPropertyChanged();
            }
        }

        public int Level
        {
            get
            {
                return _Level;
            }
            set
            {
                _Level = value;
                OnPropertyChanged();
            }
        }

        public string Picture
        {
            get
            {
                return _Picture;
            }
            set
            {
                _Picture = value;
                OnPropertyChanged();
            }
        }

        public event EventHandler PropertyChanged;

        public void OnPropertyChanged()
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, EventArgs.Empty);
            }
        }
    }
}
