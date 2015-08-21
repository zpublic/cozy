using CozyDungeon.RoleCardEditor.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CozyDungeon.RoleCardEditor
{
    public partial class EditorForm
    {
        private Dictionary<int, Image> CardImageDictionary { get; set; }
            = new Dictionary<int, Image>();

        private Image BorderImage { get; set; }

        private Image SelectedImage { get; set; }

        private void OpenImage()
        {
            OpenFileDialog fileDig  = new OpenFileDialog();
            fileDig.Filter          = @"图片 | *.jpg; *.png; *.gif";
            if (fileDig.ShowDialog() == DialogResult.OK)
            {
                var filename    = fileDig.FileName;
                SelectedImage   = Image.FromFile(filename, false);
                RefreshImage();
            }
        }

        private void LoadBorder()
        {
            int value = (int)LevelBox.SelectedValue;
            if (value > 0)
            {
                string name = "level" + (value < 5 ? value : 5);
                BorderImage = (Image)Resources.ResourceManager.GetObject(name);
                RefreshImage();
            }
        }

        private void RefreshImage()
        {
            var CardPictureImage = new Bitmap(270, 380);
            using (Graphics g = Graphics.FromImage(CardPictureImage))
            {
                if (SelectedImage != null)
                {
                    g.DrawImage(SelectedImage, new Rectangle(Point.Empty, cardPictureBox.Size));
                }
                if (BorderImage != null)
                {
                    g.DrawImage(BorderImage, new Rectangle(Point.Empty, cardPictureBox.Size));
                }
            }

            cardPictureBox.Image = CardPictureImage;
        }
    }
}
