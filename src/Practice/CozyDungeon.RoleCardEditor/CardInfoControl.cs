using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CozyDungeon.Game.Component.Card.Enum;
using CozyDungeon.Game.Component.Card.Model;
using CozyDungeon.RoleCardEditor.Properties;
using CozyDungeon.RoleCardEditor.EventArg;

namespace CozyDungeon.RoleCardEditor
{
    public partial class CardInfoControl : UserControl
    {
        public event EventHandler<ImageRefreshEventArgs> ImageRefreshEventHandler;

        private CozyCardImage images = new CozyCardImage();
        public CozyCardImage Images
        {
            get
            {
                return images;
            }
            set
            {
                images = value;
            }
        }

        private RoleCard roleCard = new RoleCard();
        public RoleCard RoleCard
        {
            get
            {
                return roleCard;
            }
            set
            {
                if (RoleCard != value)
                {
                    roleCard = value;
                }
            }
        }

        public Image RoleCardImage { get; private set; }

        private List<RoleCardLevel> cardLevels;
        public List<RoleCardLevel> CardLevels
        {
            get
            {
                return cardLevels;
            }
            set
            {
                cardLevels = value;
                RefreshLevelBox();
            }
        }

        private int InnerId;
        public int Id
        {
            get
            {
                return InnerId;
            }
            set
            {
                InnerId = value;
                RoleCard.Id = value;
                IDBox.Text = value.ToString();
            }
        }

        private bool IsModification { get; set; }

        public void BeginModify()
        {
            IsModification = true;
        }

        public void EndModify()
        {
            IsModification = false;
            RefreshControls();
        }

        public CardInfoControl()
        {
            InitializeComponent();
        }

        private void RefreshLevelBox()
        {
            if(CardLevels != null)
            {
                var LevelValueList = from obj
                       in CardLevels
                                     select new KeyValuePair<string, RoleCardLevel>(CardLevel.RoleCardLevelName(obj), obj);

                LevelBox.DisplayMember = "Key";
                LevelBox.ValueMember = "Value";
                LevelBox.DataSource = LevelValueList.ToList();
            }
        }

        private void LevelBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            RoleCard.Level = (RoleCardLevel)LevelBox.SelectedValue;
            LoadBorder();
        }

        public void　Clear()
        {
            BeginModify();
            RoleCard    = new RoleCard();
            Images      = new CozyCardImage();
            EndModify();
        }

        private void RefreshControls()
        {
            LevelBox.SelectedValue  = RoleCard.Level;
            IDBox.Text              = RoleCard.Id.ToString();
            NameBox.Text            = RoleCard.Name;
            DescBox.Text            = RoleCard.Desc;
            HPBox.Text              = RoleCard.HP.ToString();
            ATKBox.Text             = RoleCard.ATK.ToString();
            DEFBox.Text             = RoleCard.DEF.ToString();
            RefreshImage();
        }

        private void DescBox_TextChanged(object sender, EventArgs e)
        {
            RoleCard.Desc = DescBox.Text;
            if(!IsModification)
            {
                RefreshImage();
            }
        }

        private void NameBox_TextChanged(object sender, EventArgs e)
        {
            RoleCard.Name = NameBox.Text;
            if (!IsModification)
            {
                RefreshImage();
            }
        }

        private void HPBox_TextChanged(object sender, EventArgs e)
        {
            if(HPBox.Text.Length > 0)
            {
                RoleCard.HP = int.Parse(HPBox.Text);
                if (!IsModification)
                {
                    RefreshImage();
                }
            }
        }

        private void ATKBox_TextChanged(object sender, EventArgs e)
        {
            if (ATKBox.Text.Length > 0)
            {
                RoleCard.ATK = int.Parse(ATKBox.Text);
                if (!IsModification)
                {
                    RefreshImage();
                }
            }
        }

        private void DEFBox_TextChanged(object sender, EventArgs e)
        {
            if (DEFBox.Text.Length > 0)
            {
                RoleCard.DEF = int.Parse(DEFBox.Text);
                if (!IsModification)
                {
                    RefreshImage();
                }
            }
        }

        private void OpenImageButton_Click(object sender, EventArgs e)
        {
            OpenImage();
            RefreshImage();
        }

        private Image BorderImage { get; set; }

        private void OpenImage()
        {
            OpenFileDialog fileDig  = new OpenFileDialog();
            fileDig.Filter          = @"图片 | *.jpg; *.png; *.gif";
            if (fileDig.ShowDialog() == DialogResult.OK)
            {
                var filename    = fileDig.FileName;
                Images          = new CozyCardImage()
                {
                    SelectedImage = Image.FromFile(filename, false),
                };
                RefreshImage();
                if(ImageRefreshEventHandler != null)
                {
                    ImageRefreshEventHandler(this, new ImageRefreshEventArgs(RoleCard.Id, Images));
                }
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
            if (Images != null)
            {
                Images.CardImage = new Bitmap(270, 380);
                using (Graphics g = Graphics.FromImage(Images.CardImage))
                {
                    if (Images.SelectedImage != null)
                    {
                        g.DrawImage(Images.SelectedImage, new Rectangle(Point.Empty, cardPictureBox.Size));
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
                cardPictureBox.Image = Images.CardImage;
            }
        }

        private void RefreshText(Graphics g)
        {
            RefreshName(g);
            RefreshHp(g);
            RefreshATK(g);
            RefreshDEF(g);
        }

        private void RefreshName(Graphics g)
        {
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

        private void RefreshHp(Graphics g)
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
        }

        private void RefreshATK(Graphics g)
        {
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
        }

        private void RefreshDEF(Graphics g)
        {
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
        }
    }
}
