namespace CozyDungeon.RoleCardEditor
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
            this.DescBox = new System.Windows.Forms.TextBox();
            this.cardPictureBox = new System.Windows.Forms.PictureBox();
            this.OpenButton = new MetroFramework.Controls.MetroButton();
            this.SaveButton = new MetroFramework.Controls.MetroButton();
            this.CloseButton = new MetroFramework.Controls.MetroButton();
            this.CardTabControl = new MetroFramework.Controls.MetroTabControl();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel4 = new MetroFramework.Controls.MetroLabel();
            this.LevelBox = new MetroFramework.Controls.MetroComboBox();
            this.NameBox = new System.Windows.Forms.TextBox();
            this.IDBox = new System.Windows.Forms.TextBox();
            this.HPBox = new System.Windows.Forms.TextBox();
            this.metroLabel5 = new MetroFramework.Controls.MetroLabel();
            this.ATKBox = new System.Windows.Forms.TextBox();
            this.metroLabel6 = new MetroFramework.Controls.MetroLabel();
            this.DEFBox = new System.Windows.Forms.TextBox();
            this.metroLabel7 = new MetroFramework.Controls.MetroLabel();
            this.CreateButton = new MetroFramework.Controls.MetroButton();
            this.OpenImageButton = new MetroFramework.Controls.MetroButton();
            this.AddCardButton = new MetroFramework.Controls.MetroButton();
            ((System.ComponentModel.ISupportInitialize)(this.cardPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // DescBox
            // 
            this.DescBox.Location = new System.Drawing.Point(343, 287);
            this.DescBox.Multiline = true;
            this.DescBox.Name = "DescBox";
            this.DescBox.Size = new System.Drawing.Size(198, 92);
            this.DescBox.TabIndex = 8;
            // 
            // cardPictureBox
            // 
            this.cardPictureBox.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.cardPictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cardPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.cardPictureBox.Location = new System.Drawing.Point(585, 134);
            this.cardPictureBox.Name = "cardPictureBox";
            this.cardPictureBox.Size = new System.Drawing.Size(270, 380);
            this.cardPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.cardPictureBox.TabIndex = 9;
            this.cardPictureBox.TabStop = false;
            // 
            // OpenButton
            // 
            this.OpenButton.Location = new System.Drawing.Point(24, 63);
            this.OpenButton.Name = "OpenButton";
            this.OpenButton.Size = new System.Drawing.Size(75, 23);
            this.OpenButton.TabIndex = 10;
            this.OpenButton.Text = "打开";
            this.OpenButton.UseSelectable = true;
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(105, 62);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 10;
            this.SaveButton.Text = "保存";
            this.SaveButton.UseSelectable = true;
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(186, 63);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(75, 23);
            this.CloseButton.TabIndex = 10;
            this.CloseButton.Text = "关闭";
            this.CloseButton.UseSelectable = true;
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
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(343, 136);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(38, 19);
            this.metroLabel1.TabIndex = 12;
            this.metroLabel1.Text = "Level";
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(343, 184);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(21, 19);
            this.metroLabel2.TabIndex = 12;
            this.metroLabel2.Text = "ID";
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.Location = new System.Drawing.Point(343, 227);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(45, 19);
            this.metroLabel3.TabIndex = 12;
            this.metroLabel3.Text = "Name";
            // 
            // metroLabel4
            // 
            this.metroLabel4.AutoSize = true;
            this.metroLabel4.Location = new System.Drawing.Point(343, 265);
            this.metroLabel4.Name = "metroLabel4";
            this.metroLabel4.Size = new System.Drawing.Size(36, 19);
            this.metroLabel4.TabIndex = 12;
            this.metroLabel4.Text = "Desc";
            // 
            // LevelBox
            // 
            this.LevelBox.FormattingEnabled = true;
            this.LevelBox.ItemHeight = 23;
            this.LevelBox.Location = new System.Drawing.Point(394, 132);
            this.LevelBox.Name = "LevelBox";
            this.LevelBox.Size = new System.Drawing.Size(147, 29);
            this.LevelBox.TabIndex = 13;
            this.LevelBox.UseSelectable = true;
            // 
            // NameBox
            // 
            this.NameBox.Location = new System.Drawing.Point(394, 226);
            this.NameBox.Name = "NameBox";
            this.NameBox.Size = new System.Drawing.Size(147, 21);
            this.NameBox.TabIndex = 7;
            // 
            // IDBox
            // 
            this.IDBox.Location = new System.Drawing.Point(394, 182);
            this.IDBox.Name = "IDBox";
            this.IDBox.Size = new System.Drawing.Size(147, 21);
            this.IDBox.TabIndex = 7;
            // 
            // HPBox
            // 
            this.HPBox.Location = new System.Drawing.Point(394, 404);
            this.HPBox.Name = "HPBox";
            this.HPBox.Size = new System.Drawing.Size(147, 21);
            this.HPBox.TabIndex = 7;
            // 
            // metroLabel5
            // 
            this.metroLabel5.AutoSize = true;
            this.metroLabel5.Location = new System.Drawing.Point(343, 406);
            this.metroLabel5.Name = "metroLabel5";
            this.metroLabel5.Size = new System.Drawing.Size(26, 19);
            this.metroLabel5.TabIndex = 12;
            this.metroLabel5.Text = "HP";
            // 
            // ATKBox
            // 
            this.ATKBox.Location = new System.Drawing.Point(394, 449);
            this.ATKBox.Name = "ATKBox";
            this.ATKBox.Size = new System.Drawing.Size(147, 21);
            this.ATKBox.TabIndex = 7;
            // 
            // metroLabel6
            // 
            this.metroLabel6.AutoSize = true;
            this.metroLabel6.Location = new System.Drawing.Point(343, 451);
            this.metroLabel6.Name = "metroLabel6";
            this.metroLabel6.Size = new System.Drawing.Size(30, 19);
            this.metroLabel6.TabIndex = 12;
            this.metroLabel6.Text = "ATK";
            // 
            // DEFBox
            // 
            this.DEFBox.Location = new System.Drawing.Point(394, 492);
            this.DEFBox.Name = "DEFBox";
            this.DEFBox.Size = new System.Drawing.Size(147, 21);
            this.DEFBox.TabIndex = 7;
            // 
            // metroLabel7
            // 
            this.metroLabel7.AutoSize = true;
            this.metroLabel7.Location = new System.Drawing.Point(343, 494);
            this.metroLabel7.Name = "metroLabel7";
            this.metroLabel7.Size = new System.Drawing.Size(32, 19);
            this.metroLabel7.TabIndex = 12;
            this.metroLabel7.Text = "DEF";
            // 
            // CreateButton
            // 
            this.CreateButton.Location = new System.Drawing.Point(267, 63);
            this.CreateButton.Name = "CreateButton";
            this.CreateButton.Size = new System.Drawing.Size(75, 23);
            this.CreateButton.TabIndex = 14;
            this.CreateButton.Text = "新建";
            this.CreateButton.UseSelectable = true;
            // 
            // OpenImageButton
            // 
            this.OpenImageButton.Location = new System.Drawing.Point(585, 536);
            this.OpenImageButton.Name = "OpenImageButton";
            this.OpenImageButton.Size = new System.Drawing.Size(114, 23);
            this.OpenImageButton.TabIndex = 15;
            this.OpenImageButton.Text = "打开图片";
            this.OpenImageButton.UseSelectable = true;
            this.OpenImageButton.Click += new System.EventHandler(this.OpenImageButton_Click);
            // 
            // AddCardButton
            // 
            this.AddCardButton.Location = new System.Drawing.Point(741, 536);
            this.AddCardButton.Name = "AddCardButton";
            this.AddCardButton.Size = new System.Drawing.Size(114, 23);
            this.AddCardButton.TabIndex = 16;
            this.AddCardButton.Text = "添加到牌组";
            this.AddCardButton.UseSelectable = true;
            this.AddCardButton.Click += new System.EventHandler(this.AddCardButton_Click);
            // 
            // EditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(936, 592);
            this.Controls.Add(this.AddCardButton);
            this.Controls.Add(this.OpenImageButton);
            this.Controls.Add(this.CreateButton);
            this.Controls.Add(this.LevelBox);
            this.Controls.Add(this.metroLabel4);
            this.Controls.Add(this.metroLabel3);
            this.Controls.Add(this.metroLabel7);
            this.Controls.Add(this.metroLabel6);
            this.Controls.Add(this.metroLabel5);
            this.Controls.Add(this.metroLabel2);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.CardTabControl);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.OpenButton);
            this.Controls.Add(this.cardPictureBox);
            this.Controls.Add(this.DescBox);
            this.Controls.Add(this.NameBox);
            this.Controls.Add(this.DEFBox);
            this.Controls.Add(this.ATKBox);
            this.Controls.Add(this.HPBox);
            this.Controls.Add(this.IDBox);
            this.Name = "EditorForm";
            this.Text = "至强卡牌编辑器";
            ((System.ComponentModel.ISupportInitialize)(this.cardPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox DescBox;
        private System.Windows.Forms.PictureBox cardPictureBox;
        private MetroFramework.Controls.MetroButton OpenButton;
        private MetroFramework.Controls.MetroButton SaveButton;
        private MetroFramework.Controls.MetroButton CloseButton;
        private MetroFramework.Controls.MetroTabControl CardTabControl;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private MetroFramework.Controls.MetroLabel metroLabel4;
        private MetroFramework.Controls.MetroComboBox LevelBox;
        private System.Windows.Forms.TextBox NameBox;
        private System.Windows.Forms.TextBox IDBox;
        private System.Windows.Forms.TextBox HPBox;
        private MetroFramework.Controls.MetroLabel metroLabel5;
        private System.Windows.Forms.TextBox ATKBox;
        private MetroFramework.Controls.MetroLabel metroLabel6;
        private System.Windows.Forms.TextBox DEFBox;
        private MetroFramework.Controls.MetroLabel metroLabel7;
        private MetroFramework.Controls.MetroButton CreateButton;
        private MetroFramework.Controls.MetroButton OpenImageButton;
        private MetroFramework.Controls.MetroButton AddCardButton;
    }
}