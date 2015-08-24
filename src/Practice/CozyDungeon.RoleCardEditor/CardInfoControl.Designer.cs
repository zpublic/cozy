namespace CozyDungeon.RoleCardEditor
{
    partial class CardInfoControl
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.OpenImageButton = new MetroFramework.Controls.MetroButton();
            this.LevelBox = new MetroFramework.Controls.MetroComboBox();
            this.metroLabel4 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel7 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel6 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel5 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.DescBox = new System.Windows.Forms.TextBox();
            this.NameBox = new System.Windows.Forms.TextBox();
            this.DEFBox = new System.Windows.Forms.TextBox();
            this.ATKBox = new System.Windows.Forms.TextBox();
            this.HPBox = new System.Windows.Forms.TextBox();
            this.IDBox = new System.Windows.Forms.TextBox();
            this.cardPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.cardPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // OpenImageButton
            // 
            this.OpenImageButton.Location = new System.Drawing.Point(254, 414);
            this.OpenImageButton.Name = "OpenImageButton";
            this.OpenImageButton.Size = new System.Drawing.Size(270, 23);
            this.OpenImageButton.TabIndex = 32;
            this.OpenImageButton.Text = "打开图片";
            this.OpenImageButton.UseSelectable = true;
            this.OpenImageButton.Click += new System.EventHandler(this.OpenImageButton_Click);
            // 
            // LevelBox
            // 
            this.LevelBox.FormattingEnabled = true;
            this.LevelBox.ItemHeight = 23;
            this.LevelBox.Location = new System.Drawing.Point(63, 10);
            this.LevelBox.Name = "LevelBox";
            this.LevelBox.Size = new System.Drawing.Size(147, 29);
            this.LevelBox.TabIndex = 17;
            this.LevelBox.UseSelectable = true;
            this.LevelBox.SelectedIndexChanged += new System.EventHandler(this.LevelBox_SelectedIndexChanged);
            // 
            // metroLabel4
            // 
            this.metroLabel4.AutoSize = true;
            this.metroLabel4.Location = new System.Drawing.Point(12, 143);
            this.metroLabel4.Name = "metroLabel4";
            this.metroLabel4.Size = new System.Drawing.Size(36, 19);
            this.metroLabel4.TabIndex = 30;
            this.metroLabel4.Text = "Desc";
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.Location = new System.Drawing.Point(12, 105);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(45, 19);
            this.metroLabel3.TabIndex = 29;
            this.metroLabel3.Text = "Name";
            // 
            // metroLabel7
            // 
            this.metroLabel7.AutoSize = true;
            this.metroLabel7.Location = new System.Drawing.Point(12, 372);
            this.metroLabel7.Name = "metroLabel7";
            this.metroLabel7.Size = new System.Drawing.Size(32, 19);
            this.metroLabel7.TabIndex = 28;
            this.metroLabel7.Text = "DEF";
            // 
            // metroLabel6
            // 
            this.metroLabel6.AutoSize = true;
            this.metroLabel6.Location = new System.Drawing.Point(12, 329);
            this.metroLabel6.Name = "metroLabel6";
            this.metroLabel6.Size = new System.Drawing.Size(30, 19);
            this.metroLabel6.TabIndex = 27;
            this.metroLabel6.Text = "ATK";
            // 
            // metroLabel5
            // 
            this.metroLabel5.AutoSize = true;
            this.metroLabel5.Location = new System.Drawing.Point(12, 284);
            this.metroLabel5.Name = "metroLabel5";
            this.metroLabel5.Size = new System.Drawing.Size(26, 19);
            this.metroLabel5.TabIndex = 31;
            this.metroLabel5.Text = "HP";
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(12, 62);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(21, 19);
            this.metroLabel2.TabIndex = 26;
            this.metroLabel2.Text = "ID";
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(12, 14);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(38, 19);
            this.metroLabel1.TabIndex = 25;
            this.metroLabel1.Text = "Level";
            // 
            // DescBox
            // 
            this.DescBox.Location = new System.Drawing.Point(12, 165);
            this.DescBox.Multiline = true;
            this.DescBox.Name = "DescBox";
            this.DescBox.Size = new System.Drawing.Size(198, 92);
            this.DescBox.TabIndex = 19;
            this.DescBox.TextChanged += new System.EventHandler(this.DescBox_TextChanged);
            // 
            // NameBox
            // 
            this.NameBox.Location = new System.Drawing.Point(63, 104);
            this.NameBox.Name = "NameBox";
            this.NameBox.Size = new System.Drawing.Size(147, 21);
            this.NameBox.TabIndex = 18;
            this.NameBox.TextChanged += new System.EventHandler(this.NameBox_TextChanged);
            // 
            // DEFBox
            // 
            this.DEFBox.Location = new System.Drawing.Point(63, 370);
            this.DEFBox.Name = "DEFBox";
            this.DEFBox.Size = new System.Drawing.Size(147, 21);
            this.DEFBox.TabIndex = 22;
            this.DEFBox.TextChanged += new System.EventHandler(this.DEFBox_TextChanged);
            // 
            // ATKBox
            // 
            this.ATKBox.Location = new System.Drawing.Point(63, 327);
            this.ATKBox.Name = "ATKBox";
            this.ATKBox.Size = new System.Drawing.Size(147, 21);
            this.ATKBox.TabIndex = 21;
            this.ATKBox.TextChanged += new System.EventHandler(this.ATKBox_TextChanged);
            // 
            // HPBox
            // 
            this.HPBox.Location = new System.Drawing.Point(63, 282);
            this.HPBox.Name = "HPBox";
            this.HPBox.Size = new System.Drawing.Size(147, 21);
            this.HPBox.TabIndex = 20;
            this.HPBox.TextChanged += new System.EventHandler(this.HPBox_TextChanged);
            // 
            // IDBox
            // 
            this.IDBox.Location = new System.Drawing.Point(63, 60);
            this.IDBox.Name = "IDBox";
            this.IDBox.Size = new System.Drawing.Size(147, 21);
            this.IDBox.TabIndex = 23;
            this.IDBox.TextChanged += new System.EventHandler(this.IDBox_TextChanged);
            // 
            // cardPictureBox
            // 
            this.cardPictureBox.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.cardPictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cardPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.cardPictureBox.Location = new System.Drawing.Point(254, 12);
            this.cardPictureBox.Name = "cardPictureBox";
            this.cardPictureBox.Size = new System.Drawing.Size(270, 380);
            this.cardPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.cardPictureBox.TabIndex = 24;
            this.cardPictureBox.TabStop = false;
            // 
            // CardInfoControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.OpenImageButton);
            this.Controls.Add(this.LevelBox);
            this.Controls.Add(this.metroLabel4);
            this.Controls.Add(this.metroLabel3);
            this.Controls.Add(this.metroLabel7);
            this.Controls.Add(this.metroLabel6);
            this.Controls.Add(this.metroLabel5);
            this.Controls.Add(this.metroLabel2);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.cardPictureBox);
            this.Controls.Add(this.DescBox);
            this.Controls.Add(this.NameBox);
            this.Controls.Add(this.DEFBox);
            this.Controls.Add(this.ATKBox);
            this.Controls.Add(this.HPBox);
            this.Controls.Add(this.IDBox);
            this.Name = "CardInfoControl";
            this.Size = new System.Drawing.Size(540, 447);
            ((System.ComponentModel.ISupportInitialize)(this.cardPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MetroFramework.Controls.MetroButton OpenImageButton;
        private MetroFramework.Controls.MetroComboBox LevelBox;
        private MetroFramework.Controls.MetroLabel metroLabel4;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private MetroFramework.Controls.MetroLabel metroLabel7;
        private MetroFramework.Controls.MetroLabel metroLabel6;
        private MetroFramework.Controls.MetroLabel metroLabel5;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private System.Windows.Forms.PictureBox cardPictureBox;
        private System.Windows.Forms.TextBox DescBox;
        private System.Windows.Forms.TextBox NameBox;
        private System.Windows.Forms.TextBox DEFBox;
        private System.Windows.Forms.TextBox ATKBox;
        private System.Windows.Forms.TextBox HPBox;
        private System.Windows.Forms.TextBox IDBox;
    }
}
