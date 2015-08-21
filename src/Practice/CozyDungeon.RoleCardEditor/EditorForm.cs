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

        private List<ListBox> CardListBoxList { get; set; }     = new List<ListBox>();
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

        private void OpenImageButton_Click(object sender, EventArgs e)
        {
            OpenImage();
        }

        private void AddCardButton_Click(object sender, EventArgs e)
        {
            if(CheckInput())
            {
                AddCard();
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

        private void LevelBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadBorder();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            SaveData();
        }
    }
}
