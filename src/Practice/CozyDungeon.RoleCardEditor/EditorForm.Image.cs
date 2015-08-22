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
                    g.DrawImage(Resources.mask, new Rectangle(new Point(6, 225), Resources.mask.Size));
                    g.DrawImage(BorderImage, new Rectangle(Point.Empty, cardPictureBox.Size));

                    g.DrawImage(Resources.hp, new Rectangle(Point.Empty, Resources.hp.Size));
                    g.DrawImage(Resources.atk, new Rectangle(new Point(30, 340), Resources.atk.Size));
                    g.DrawImage(Resources.def, new Rectangle(new Point(180, 340), Resources.def.Size));

                    RefreshText(g);
                }
            }
            cardPictureBox.Image = CardPictureImage;
        }

        private void RefreshText(Graphics g)
        {
            if (HPBox.Text != null && HPBox.Text.Length > 0)
            {
                int hp = int.Parse(HPBox.Text);
                if (hp >= 0 && hp <= 10)
                {
                    var img = (Image)Resources.ResourceManager.GetObject("_" + hp.ToString(), Resources.Culture);
                    g.DrawImage(img, new Rectangle(new Point(29 - img.Width / 2, 27 - img.Height / 2), img.Size));
                }
            }

            if (ATKBox.Text != null && ATKBox.Text.Length > 0)
            {
                int atk = int.Parse(ATKBox.Text);
                if (atk >= 0 && atk <= 99)
                {
                    g.DrawString(
                        ATKBox.Text,
                        new Font("黑体", 14, FontStyle.Bold),
                        new SolidBrush(Color.FromArgb(254, 112, 12)),
                        60,
                        350);
                }
            }

            if (DEFBox.Text != null && DEFBox.Text.Length > 0)
            {
                int def = int.Parse(DEFBox.Text);
                if (def >= 0 && def <= 99)
                {
                    g.DrawString(
                        DEFBox.Text,
                        new Font("黑体", 14, FontStyle.Bold),
                        new SolidBrush(Color.FromArgb(0, 167, 227)),
                        215,
                        350);
                }
            }

            if (NameBox.Text != null && NameBox.Text.Length > 0)
            {
                var font = new Font("黑体", 14, FontStyle.Regular);
                var size = g.MeasureString(NameBox.Text, font);

                g.DrawString(
                        NameBox.Text,
                        font,
                        new SolidBrush(Color.WhiteSmoke),
                        137 - size.Width / 2,
                        328);
            }
        }
    }
}
