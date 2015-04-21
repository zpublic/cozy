using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreCozy.Extensions;
using Windows.UI.Xaml.Media;

namespace StoreCozy.Model
{
    public class AddMenuCardInfo : BindableBase
    {
        private string title;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        private string description;
        public string Description
        {
            get { return description; }
            set { SetProperty(ref description, value); }
        }

        private ImageSource image;
        public ImageSource Image
        {
            get { return image; }
            set
            {
                SetProperty(ref image, value);
            }
        }

        private string imageFileName;
        public string ImageFileName
        {
            get { return imageFileName; }
            set
            {
                SetProperty(ref imageFileName, value);
            }
        }
    }
}
