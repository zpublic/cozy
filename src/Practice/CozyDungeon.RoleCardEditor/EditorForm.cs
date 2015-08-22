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
using CozyDungeon.Game.Component.Card.Model;

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
            CreateNewCards();
            ResetId();
        }

        private List<BindingList<RoleCard>> ListOfRoleCardList { get; set; } = new List<BindingList<RoleCard>>();
        private List<RoleCardLevel> CardLevels { get; set; }    = new List<RoleCardLevel>();

        private RoleCard SelectedItem { get; set; }

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

                var listData = new BindingList<RoleCard>();
                var list            = new ListBox()
                {
                    Dock                = DockStyle.Fill,
                    ContextMenuStrip    = TabControlContextMenu,
                    DisplayMember       = "Name",
                    ValueMember         = "Id",
                    DataSource          = listData,
                };
                
                list.SelectedIndexChanged += (sender, msg) =>
                {
                    SelectedItem = list.SelectedItem as RoleCard;
                };

                page.Controls.Add(list);
                ListOfRoleCardList.Add(listData);
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

        private void CreateNewCards()
        {
            CloseCards();
            EnableAllControls();
            this.Text = "至强卡牌编辑器 - " + "未命名";
        }

        private void CloseCards()
        {
            if (IsModified)
            {
                var result = MessageBox.Show("是否保存", "关闭", MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                {
                    SaveData();
                }
                else if (result == DialogResult.Cancel)
                {
                    return;
                }
            }
            ClearAll();
            DisableAllControls();
        }

        private void DisableAllControls()
        {
            CardTabControl.Enabled  = false;
            LevelBox.Enabled        = false;
            NameBox.Enabled         = false;
            DescBox.Enabled         = false;
            HPBox.Enabled           = false;
            ATKBox.Enabled          = false;
            DEFBox.Enabled          = false;
            cardPictureBox.Enabled  = false;
            OpenImageButton.Enabled = false;
            AddCardButton.Enabled   = false;
        }

        private void EnableAllControls()
        {
            CardTabControl.Enabled = true;
            LevelBox.Enabled        = true;
            NameBox.Enabled         = true;
            DescBox.Enabled         = true;
            HPBox.Enabled           = true;
            ATKBox.Enabled          = true;
            DEFBox.Enabled          = true;
            cardPictureBox.Enabled  = true;
            OpenImageButton.Enabled = true;
            AddCardButton.Enabled   = true;
        }

        private void ClearAll()
        {
            foreach(var obj in ListOfRoleCardList)
            {
                obj.Clear();
            }

            CardImageDictionary.Clear();
            ResetInput();
            ClearId();

            SaveFileName    = null;
            IsModified      = false;
            this.Text       = "至强卡牌编辑器";
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

        private void CreateButton_Click(object sender, EventArgs e)
        {
            CreateNewCards();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            CloseCards();
        }

        private void HPBox_TextChanged(object sender, EventArgs e)
        {
            RefreshImage();
        }

        private void ATKBox_TextChanged(object sender, EventArgs e)
        {
            RefreshImage();
        }

        private void DEFBox_TextChanged(object sender, EventArgs e)
        {
            RefreshImage();
        }

        private void NameBox_TextChanged(object sender, EventArgs e)
        {
            RefreshImage();
        }
    }
}
