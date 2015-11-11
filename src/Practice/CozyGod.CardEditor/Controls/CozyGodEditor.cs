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
                RefreshImage();
                UpdateElementHandler();
            }
        }

        private Image SourceImage { get; set; }
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

        public Image ElementBorder { get; set; }

        public Font NameFont { get; set; } = SystemFonts.DefaultFont;

        public CozyGodEditor()
        {
            InitializeComponent();
        }

        private void Init()
        {
            if (Image == null)
            {
                Image = new Bitmap(Width, Height);
            }

            SourceImage = null;
            if (Element != null && File.Exists(Element.Picture))
            {
                SourceImage = Image.FromFile(Element.Picture);
            }
            DrawSourceImage();
        }

        private void DrawSourceImage()
        {
            using (var g = Graphics.FromImage(Image))
            {
                g.Clear(SystemColors.Control);

                if (SourceImage != null)
                {
                    g.DrawImage(SourceImage,
                        new Rectangle(0, 0, Width, (int)(Height * 0.625)),
                        new Rectangle(0, 0, SourceImage.Width, SourceImage.Height),
                            GraphicsUnit.Pixel);

                }
            }
        }

        private void UpdateElementHandler()
        {
            if(Element != null)
            {
                Element.PropertyChanged += OnElementPropertyChanged;
            }
        }

        private void OnElementPropertyChanged(object sender, EventArgs e)
        {
            DrawSourceImage();
            RefreshImage();
        }

        private void RefreshImage()
        {
            if(Image != null)
            {
                using (var g = Graphics.FromImage(Image))
                {
                    if(ElementBorder != null)
                    {
                        g.DrawImage(ElementBorder, 
                            new Rectangle(0, 0, Width, Height),
                            new Rectangle(0, 0, ElementBorder.Width, ElementBorder.Height),
                            GraphicsUnit.Pixel);
                    }

                    if(Element != null)
                    {
                        SizeF sizeText = g.MeasureString(Element.Name, NameFont);

                        g.DrawString(Element.Name,
                            NameFont,
                            SystemBrushes.ControlText,
                            (Width - sizeText.Width) / 2,
                            (int)(Height * 0.625));
                    }
                }
                InnerPictruceBox.Invalidate();
            }
        }
    }
}
