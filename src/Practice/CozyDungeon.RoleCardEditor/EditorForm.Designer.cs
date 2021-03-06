﻿namespace CozyDungeon.RoleCardEditor
{
    partial class EditorForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            CozyDungeon.RoleCardEditor.CozyCardImage cozyCardImage1 = new CozyDungeon.RoleCardEditor.CozyCardImage();
            CozyDungeon.Game.Component.Card.Model.RoleCard roleCard1 = new CozyDungeon.Game.Component.Card.Model.RoleCard();
            this.OpenButton = new MetroFramework.Controls.MetroButton();
            this.SaveButton = new MetroFramework.Controls.MetroButton();
            this.CloseButton = new MetroFramework.Controls.MetroButton();
            this.CardTabControl = new MetroFramework.Controls.MetroTabControl();
            this.CreateButton = new MetroFramework.Controls.MetroButton();
            this.TabControlContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.RemoveCardItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CreateCardItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CreateCardButton = new MetroFramework.Controls.MetroButton();
            this.cardInfoControl1 = new CozyDungeon.RoleCardEditor.CardInfoControl();
            this.TabControlContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // OpenButton
            // 
            this.OpenButton.Location = new System.Drawing.Point(24, 63);
            this.OpenButton.Name = "OpenButton";
            this.OpenButton.Size = new System.Drawing.Size(75, 23);
            this.OpenButton.TabIndex = 10;
            this.OpenButton.Text = "打开";
            this.OpenButton.UseSelectable = true;
            this.OpenButton.Click += new System.EventHandler(this.OpenButton_Click);
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(105, 62);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 10;
            this.SaveButton.Text = "保存";
            this.SaveButton.UseSelectable = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(186, 63);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(75, 23);
            this.CloseButton.TabIndex = 10;
            this.CloseButton.Text = "关闭";
            this.CloseButton.UseSelectable = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // CardTabControl
            // 
            this.CardTabControl.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.CardTabControl.ItemSize = new System.Drawing.Size(60, 0);
            this.CardTabControl.Location = new System.Drawing.Point(24, 92);
            this.CardTabControl.Multiline = true;
            this.CardTabControl.Name = "CardTabControl";
            this.CardTabControl.Size = new System.Drawing.Size(302, 477);
            this.CardTabControl.TabIndex = 11;
            this.CardTabControl.UseSelectable = true;
            // 
            // CreateButton
            // 
            this.CreateButton.Location = new System.Drawing.Point(267, 63);
            this.CreateButton.Name = "CreateButton";
            this.CreateButton.Size = new System.Drawing.Size(75, 23);
            this.CreateButton.TabIndex = 14;
            this.CreateButton.Text = "新建";
            this.CreateButton.UseSelectable = true;
            this.CreateButton.Click += new System.EventHandler(this.CreateButton_Click);
            // 
            // TabControlContextMenu
            // 
            this.TabControlContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CreateCardItem,
            this.RemoveCardItem});
            this.TabControlContextMenu.Name = "TabControlContextMenu";
            this.TabControlContextMenu.Size = new System.Drawing.Size(101, 48);
            // 
            // RemoveCardItem
            // 
            this.RemoveCardItem.Name = "RemoveCardItem";
            this.RemoveCardItem.Size = new System.Drawing.Size(100, 22);
            this.RemoveCardItem.Text = "删除";
            this.RemoveCardItem.Click += new System.EventHandler(this.RemoveCardItem_Click);
            // 
            // CreateCardItem
            // 
            this.CreateCardItem.Name = "CreateCardItem";
            this.CreateCardItem.Size = new System.Drawing.Size(100, 22);
            this.CreateCardItem.Text = "创建";
            this.CreateCardItem.Click += new System.EventHandler(this.CreateCardItem_Click);
            // 
            // CreateCardButton
            // 
            this.CreateCardButton.Location = new System.Drawing.Point(589, 62);
            this.CreateCardButton.Name = "CreateCardButton";
            this.CreateCardButton.Size = new System.Drawing.Size(96, 23);
            this.CreateCardButton.TabIndex = 16;
            this.CreateCardButton.Text = "创建新卡片";
            this.CreateCardButton.UseSelectable = true;
            this.CreateCardButton.Click += new System.EventHandler(this.CreateCardButton_Click);
            // 
            // cardInfoControl1
            // 
            this.cardInfoControl1.CardLevels = null;
            this.cardInfoControl1.Id = 0;
            cozyCardImage1.CardImage = null;
            cozyCardImage1.SelectedImage = null;
            this.cardInfoControl1.Images = cozyCardImage1;
            this.cardInfoControl1.Location = new System.Drawing.Point(342, 92);
            this.cardInfoControl1.Name = "cardInfoControl1";
            roleCard1.ATK = 1;
            roleCard1.DEF = 0;
            roleCard1.Desc = null;
            roleCard1.Element = CozyDungeon.Game.Component.Card.Enum.FiveLine.Gold;
            roleCard1.HP = 1;
            roleCard1.Id = 0;
            roleCard1.Level = CozyDungeon.Game.Component.Card.Enum.RoleCardLevel.LevelInvalid;
            roleCard1.Name = "无效名字";
            this.cardInfoControl1.RoleCard = roleCard1;
            this.cardInfoControl1.Size = new System.Drawing.Size(770, 519);
            this.cardInfoControl1.TabIndex = 15;
            // 
            // EditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(936, 592);
            this.Controls.Add(this.CreateCardButton);
            this.Controls.Add(this.cardInfoControl1);
            this.Controls.Add(this.CreateButton);
            this.Controls.Add(this.CardTabControl);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.OpenButton);
            this.Name = "EditorForm";
            this.Text = "至强卡牌编辑器";
            this.TabControlContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private MetroFramework.Controls.MetroButton OpenButton;
        private MetroFramework.Controls.MetroButton SaveButton;
        private MetroFramework.Controls.MetroButton CloseButton;
        private MetroFramework.Controls.MetroTabControl CardTabControl;
        private MetroFramework.Controls.MetroButton CreateButton;
        private System.Windows.Forms.ContextMenuStrip TabControlContextMenu;
        private System.Windows.Forms.ToolStripMenuItem RemoveCardItem;
        private CardInfoControl cardInfoControl1;
        private MetroFramework.Controls.MetroButton CreateCardButton;
        private System.Windows.Forms.ToolStripMenuItem CreateCardItem;
    }
}