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

        private List<ListBox> CardListBoxList { get; set; } = new List<ListBox>();
        private List<RoleCard> CardList { get; set; } = new List<RoleCard>();
        private List<RoleCardLevel> CardLevels { get; set; } = new List<RoleCardLevel>();

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

            LevelBox.DataSource     = LevelValueList.ToList();
            LevelBox.DisplayMember  = "Key";
            LevelBox.ValueMember    = "Value";
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

        private void OpenImageButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDig  = new OpenFileDialog();
            fileDig.Filter          = @"图片 | *.jpg; *.png; *.gif";
            if(fileDig.ShowDialog() == DialogResult.OK)
            {
                var filename            = fileDig.FileName;
                cardPictureBox.Image    = Image.FromFile(filename, false);
            }
        }

        private void AddCardButton_Click(object sender, EventArgs e)
        {
            if(CheckInput())
            {
                var card = new RoleCard()
                {
                    Level   = (RoleCardLevel)Enum.ToObject(typeof(RoleCardLevel), (int)LevelBox.SelectedValue),
                    Id      = int.Parse(IDBox.Text),
                    Name    = NameBox.Text,
                    Desc    = DescBox.Text,
                    ATK     = int.Parse(ATKBox.Text),
                    DEF     = int.Parse(DEFBox.Text),
                    HP      = int.Parse(HPBox.Text),
                    Element = FiveLine.Water,
                };

                CardList.Add(card);
                CardListBoxList[(int)LevelBox.SelectedValue].Items.Add(card.Name);

                ResetInput();
            }
        }

        private void ResetInput()
        {
            ResetId();
            NameBox.Text            = string.Empty;
            DescBox.Text            = string.Empty;
            HPBox.Text              = string.Empty;
            ATKBox.Text             = string.Empty;
            DEFBox.Text             = string.Empty;
            cardPictureBox.Image    = null;
        }

        private bool CheckInput()
        {
            if (NameBox.Text.Length     == 0) return false;
            if (HPBox.Text.Length       == 0) return false;
            if (ATKBox.Text.Length      == 0) return false;
            if (DEFBox.Text.Length      == 0) return false;
            if (cardPictureBox.Image    == null) return false;
            return true;
        }
    }
}
