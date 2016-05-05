using CozyPeise.Models;
using GalaSoft.MvvmLight;
using System.Collections.Generic;

namespace CozyPeise.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            var palette = new Palette()
            {
                Name = "hehe",
                Url = "xxx",
                RGB = new List<string>()
                {
                    "#5FD9CD",
                    "#EAF786",
                    "#FFB5A1",
                    "#B8FFB8",
                    "#B8F4FF",
                }
            };
            PeiseDisplayX = new PeiseDisplayViewModel(palette);
        }

        public string Title { get; } = "ÅäÉ«";

        public PeiseDisplayViewModel PeiseDisplayX { get; }
    }
}