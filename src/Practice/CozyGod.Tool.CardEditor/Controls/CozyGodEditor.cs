using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CozyGod.Model;
using System.IO;

namespace CozyGod.CardEditor.Controls
{
    public partial class CozyGodEditor : UserControl
    {

        private CozyGodElement _Element { get; set; }
        public CozyGodElement Element
        {
            get
            {
                return _Element;
            }
            set
            {
                _Element = value;
                Init();
                Refresh();
            }
        }

        public Image Image
        {
            get
            {
                return InnerPictruceBox.Image;
            }

            private set
            {
                InnerPictruceBox.Image = value;
            }
        }

        public CozyGodEditor()
        {
            InitializeComponent();
        }

        private void Init()
        {
            if (File.Exists(Element.Picture))
            {
                Image = Image.FromFile(Element.Picture);
            }
            else
            {
                Image = null;
            }
        }

        private void RefreshImage()
        {
            if(Image != null)
            {
                using (var g = Graphics.FromImage(Image))
                {

                }
            }
        }
    }
}
