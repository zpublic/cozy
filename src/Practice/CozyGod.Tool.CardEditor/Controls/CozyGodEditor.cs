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
        private Card _Element { get; set; }
        public Card Element
        {
            get
            {
                return _Element;
            }
            set
            {
                _Element = value;

                if(Image != null)
                {
                    RefreshAll();
                }
                
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

        public Size SourceImageSize { get; set; }
        public Point SourceImagePos { get; set; }

        private Image _ElementBorder { get; set; }
        public Image ElementBorder
        {
            get
            {
                return _ElementBorder;
            }
            set
            {
                _ElementBorder = value;

                RefreshAll();
            }
        }

        public Font NameFont { get; set; } = SystemFonts.DefaultFont;
        public int NamePoxY { get; set; }

        private string LastFilePath = string.Empty;

        public CozyGodEditor()
        {
            InitializeComponent();
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
            if (Image != null)
            {
                RefreshAll();
            }
        }

        private void RefreshAll()
        {
            if(Image != null && Element != null)
            {
                using (var g = Graphics.FromImage(Image))
                {
                    RefreshSourceImage();
                    DrawSourceImage(g);
                    RefreshBorder(g);
                    RefreshImage(g);
                }
            }
        }

        private void RefreshSourceImage()
        {
            if (Element != null && Element.Picture != LastFilePath && File.Exists(Element.Picture))
            {
                LastFilePath    = Element.Picture;
                SourceImage     = Image.FromFile(Element.Picture);
            }
        }

        private void DrawSourceImage(Graphics g)
        {
            g.Clear(SystemColors.Control);

            if (SourceImage != null)
            {
                g.DrawImage(SourceImage,
                    new Rectangle(SourceImagePos, SourceImageSize),
                    new Rectangle(Point.Empty, SourceImage.Size),
                        GraphicsUnit.Pixel);
            }
            InnerPictruceBox.Invalidate();
        }

        private void RefreshImage(Graphics g)
        {
            if (Element != null)
            {
                SizeF sizeText = g.MeasureString(Element.Name, NameFont);

                g.DrawString(Element.Name,
                    NameFont,
                    SystemBrushes.ControlText,
                    (Width - sizeText.Width) / 2,
                    NamePoxY);
            }
            InnerPictruceBox.Invalidate();
        }

        private void RefreshBorder(Graphics g)
        {
            if (ElementBorder != null)
            {
                g.DrawImage(ElementBorder,
                    new Rectangle(Point.Empty, Size),
                    new Rectangle(Point.Empty, ElementBorder.Size),
                    GraphicsUnit.Pixel);
            }
            InnerPictruceBox.Invalidate();
        }

        private void CozyGodEditor_Load(object sender, EventArgs e)
        {
             Image = new Bitmap(Width, Height);
        }
    }
}
