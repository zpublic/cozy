using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CozyDungeon.Game.Component.Card.Model;
using CozyDungeon.Game.Component.Card.Enum;
using System.Reflection;
using System.Resources;
using CozyDungeon.RoleCardEditor.Properties;
using Newtonsoft.Json;
using System.IO;
using System.Drawing.Imaging;

namespace CozyDungeon.RoleCardEditor
{
    public partial class EditorForm : MetroForm
    {
        public EditorForm()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            InitTabControlPages();
            InitLevelBox();
            ResetId();
        }

        private List<ListBox> CardListBoxList { get; set; }     = new List<ListBox>();
        private List<RoleCard> CardList { get; set; }           = new List<RoleCard>();
        private List<RoleCardLevel> CardLevels { get; set; }    = new List<RoleCardLevel>();

        private void InitTabControlPages()
        {
            var fields = typeof(RoleCardLevel).GetFields(BindingFlags.Static | BindingFlags.Public);
            foreach(var fi in fields)
            {
                var value = fi.GetValue(null);
                CardLevels.Add((RoleCardLevel)value);
            }

            foreach(var obj in CardLevels)
            {
                var page = new TabPage { Text = CardLevel.RoleCardLevelName(obj) };
                CardTabControl.TabPages.Add(page);

                var list = new ListBox() { Dock = DockStyle.Fill };
                page.Controls.Add(list);
                CardListBoxList.Add(list);
            }
        }

        private void InitLevelBox()
        {
            var LevelValueList = from obj
                       in CardLevels
                       select new KeyValuePair<string, int>(CardLevel.RoleCardLevelName(obj), (int)obj);

            LevelBox.DisplayMember  = "Key";
            LevelBox.ValueMember    = "Value";
            LevelBox.DataSource     = LevelValueList.ToList();
        }

        private int InnerID = 0;
        public string IDMaker
        {
            get
            {
                InnerID++;
                return InnerID.ToString();
            }
        }

        private void ResetId()
        {
            IDBox.Text = IDMaker;
        }

        private Image SelectedImage { get; set; }
        private void OpenImageButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDig  = new OpenFileDialog();
            fileDig.Filter          = @"图片 | *.jpg; *.png; *.gif";
            if(fileDig.ShowDialog() == DialogResult.OK)
            {
                var filename            = fileDig.FileName;
                SelectedImage           = Image.FromFile(filename, false);
                RefreshImage();
                ResetId();
            }
        }

        private Dictionary<int, Image> CardImageDictionary { get; set; } 
            = new Dictionary<int, Image>();
        private void AddCardButton_Click(object sender, EventArgs e)
        {
            if(CheckInput())
            {
                var card = new RoleCard()
                {
                    Level   = CardLevels[(int)LevelBox.SelectedValue],
                    Id      = int.Parse(IDBox.Text),
                    Name    = NameBox.Text,
                    Desc    = DescBox.Text,
                    ATK     = int.Parse(ATKBox.Text),
                    DEF     = int.Parse(DEFBox.Text),
                    HP      = int.Parse(HPBox.Text),
                };

                CardList.Add(card);
                CardListBoxList[(int)LevelBox.SelectedValue].Items.Add(card.Name);
                CardImageDictionary[card.Id] = cardPictureBox.Image;
                ResetInput();
            }
        }

        private void ResetInput()
        {
            LevelBox.SelectedIndex  = 0;
            NameBox.Text            = string.Empty;
            DescBox.Text            = string.Empty;
            HPBox.Text              = string.Empty;
            ATKBox.Text             = string.Empty;
            DEFBox.Text             = string.Empty;
            SelectedImage           = null;
            BorderImage             = null;
            cardPictureBox.Image    = null;
        }

        private bool CheckInput()
        {
            if (NameBox.Text.Length     == 0) return false;
            if (HPBox.Text.Length       == 0) return false;
            if (ATKBox.Text.Length      == 0) return false;
            if (DEFBox.Text.Length      == 0) return false;
            if (SelectedImage           == null) return false;
            return true;
        }

        private Image BorderImage { get; set; }
        private void LevelBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int value = (int)LevelBox.SelectedValue;
            if ( value > 0)
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
                if(SelectedImage != null)
                {
                    g.DrawImage(SelectedImage, new Rectangle(Point.Empty, cardPictureBox.Size));
                }
                if(BorderImage != null)
                {
                    g.DrawImage(BorderImage, new Rectangle(Point.Empty, cardPictureBox.Size));
                }
            }
           
            cardPictureBox.Image = CardPictureImage;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog fileDig = new SaveFileDialog();
            fileDig.Filter = @"json | *.json";
            if (fileDig.ShowDialog() == DialogResult.OK)
            {
                var filename = new Uri(fileDig.FileName);
                List<List<int>> savejson = new List<List<int>>();
                for(int i = 0; i < CardLevels.Count; ++i)
                {
                    savejson.Add(new List<int>());
                }

                foreach(var obj in CardList)
                {
                    savejson[(int)obj.Level].Add(obj.Id);
                }

                using (var fs = new FileStream(filename.AbsolutePath, FileMode.Create, FileAccess.ReadWrite))
                {
                    var json = JsonConvert.SerializeObject(savejson);
                    var data = Encoding.UTF8.GetBytes(json);
                    fs.Write(data, 0, data.Length);
                }

                var JsonDireName = Path.GetDirectoryName(filename.AbsolutePath) + @"\object\";
                Directory.CreateDirectory(JsonDireName);
                foreach (var obj in CardList)
                {
                    var json        = JsonConvert.SerializeObject(obj);
                    var jsonname    = JsonDireName + obj.Id + ".json";

                    using (var fs = new FileStream(jsonname, FileMode.Create, FileAccess.ReadWrite))
                    {
                        var data = Encoding.UTF8.GetBytes(json);
                        fs.Write(data, 0, data.Length);
                    }
                }

                var ImageDireName = Path.GetDirectoryName(filename.AbsolutePath) + @"\image\";
                Directory.CreateDirectory(ImageDireName);
                foreach (var obj in CardImageDictionary)
                {
                    var imagename   = ImageDireName + obj.Key + ".png";
                    using (var fs = new FileStream(imagename, FileMode.Create, FileAccess.ReadWrite))
                    {
                        obj.Value.Save(fs, ImageFormat.Png);
                    }
                }
            }
        }
    }
}
