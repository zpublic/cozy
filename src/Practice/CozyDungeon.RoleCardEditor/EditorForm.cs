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
        private List<BindingList<RoleCard>> ListOfRoleCardList { get; set; }
            = new List<BindingList<RoleCard>>();

        private List<RoleCardLevel> CardLevels { get; set; }
            = new List<RoleCardLevel>();

        private Dictionary<int, CozyCardImage> CardImageDictionary { get; set; }
            = new Dictionary<int, CozyCardImage>();

        private RoleCard SelectedItem { get; set; }

        private bool IsModified { get; set; }

        public EditorForm()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            LoadCardLevels();
            InitCardControl();
            InitTabControlPages();
            CreateNewCards();

            CardIdCache = IDMaker;
        }

        private void LoadCardLevels()
        {
            var fields = typeof(RoleCardLevel).GetFields(BindingFlags.Static | BindingFlags.Public);
            foreach (var fi in fields)
            {
                var value = fi.GetValue(null);
                CardLevels.Add((RoleCardLevel)value);
            }
        }

        private void InitCardControl()
        {
            cardInfoControl1.CardLevels = CardLevels;
            cardInfoControl1.ImageRefreshEventHandler += (sender, msg) =>
            {
                if(CardImageDictionary.ContainsKey(msg.Id))
                {
                    CardImageDictionary[msg.Id] = msg.Img;
                }
            };
        }

        private void InitTabControlPages()
        {
            foreach(var obj in CardLevels)
            {
                var page = new TabPage { Text = CardLevel.RoleCardLevelName(obj) };
                CardTabControl.TabPages.Add(page);

                var listData        = new BindingList<RoleCard>();
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
                    var item = list.SelectedItem as RoleCard;
                    if (ListOfRoleCardList[CardTabControl.SelectedIndex].Contains(item))
                    {
                        SelectedItem = item;

                        cardInfoControl1.BeginModify();
                        cardInfoControl1.Images     = CardImageDictionary[SelectedItem.Id];
                        cardInfoControl1.RoleCard   = SelectedItem;
                        cardInfoControl1.EndModify();
                    }
                };

                page.Controls.Add(list);
                ListOfRoleCardList.Add(listData);
            }
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

        private void CreateCardButton_Click(object sender, EventArgs e)
        {
            CreateCard();
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
            CardTabControl.Enabled      = false;
            cardInfoControl1.Enabled    = false;
        }

        private void EnableAllControls()
        {
            CardTabControl.Enabled      = true;
            cardInfoControl1.Enabled    = true;
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
            cardInfoControl1.Clear();
        }

        private void CreateCardItem_Click(object sender, EventArgs e)
        {
            CreateCard();
        }

        private void RemoveCardItem_Click(object sender, EventArgs e)
        {
            RemoveCard();
        }
    }
}
