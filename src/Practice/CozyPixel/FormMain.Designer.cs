namespace CozyPixel
{
    partial class CozyPixelForm
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Drawing.StringFormat stringFormat1 = new System.Drawing.StringFormat();
            this.MainStripMenu = new System.Windows.Forms.MenuStrip();
            this.FileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CreateMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SelectedColorButton = new System.Windows.Forms.Button();
            this.MainTopTab = new System.Windows.Forms.TabControl();
            this.ColorPage = new System.Windows.Forms.TabPage();
            this.CozyColorPage = new System.Windows.Forms.TabPage();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.MainStatusStrip = new System.Windows.Forms.StatusStrip();
            this.CurrPathStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.PixelPainter = new CozyPixel.Controls.PixelPainter();
            this.ColorList = new CozyPixel.Controls.ColorListView();
            this.MainLeftTab = new CozyPixel.Controls.VerticalTabControl();
            this.SelectToolPage = new System.Windows.Forms.TabPage();
            this.FillToolButton = new System.Windows.Forms.Button();
            this.StrawToolButton = new System.Windows.Forms.Button();
            this.EarserToolButton = new System.Windows.Forms.Button();
            this.LineToolButton = new System.Windows.Forms.Button();
            this.PencilToolButton = new System.Windows.Forms.Button();
            this.FileSelectPage = new System.Windows.Forms.TabPage();
            this.RefreshThumbListButton = new System.Windows.Forms.Button();
            this.ThumbListView = new CozyPixel.Controls.ImageListView();
            this.DirectorySelectButton = new System.Windows.Forms.Button();
            this.GridOpeionPage = new System.Windows.Forms.TabPage();
            this.GridWidthLabel = new System.Windows.Forms.Label();
            this.GridWidthBox = new System.Windows.Forms.TextBox();
            this.ShowGridCheckBox = new System.Windows.Forms.CheckBox();
            this.GridColorButton = new System.Windows.Forms.Button();
            this.ColorLabel = new System.Windows.Forms.Label();
            this.MainStripMenu.SuspendLayout();
            this.MainTopTab.SuspendLayout();
            this.ColorPage.SuspendLayout();
            this.CozyColorPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.MainStatusStrip.SuspendLayout();
            this.MainLeftTab.SuspendLayout();
            this.SelectToolPage.SuspendLayout();
            this.FileSelectPage.SuspendLayout();
            this.GridOpeionPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainStripMenu
            // 
            this.MainStripMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenuItem,
            this.HelpMenuItem});
            this.MainStripMenu.Location = new System.Drawing.Point(0, 0);
            this.MainStripMenu.Name = "MainStripMenu";
            this.MainStripMenu.Size = new System.Drawing.Size(1264, 25);
            this.MainStripMenu.TabIndex = 1;
            this.MainStripMenu.Text = "menuStrip1";
            // 
            // FileMenuItem
            // 
            this.FileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CreateMenuItem,
            this.OpenMenuItem,
            this.SaveMenuItem,
            this.ExitMenuItem});
            this.FileMenuItem.Name = "FileMenuItem";
            this.FileMenuItem.Size = new System.Drawing.Size(44, 21);
            this.FileMenuItem.Text = "文件";
            // 
            // CreateMenuItem
            // 
            this.CreateMenuItem.Name = "CreateMenuItem";
            this.CreateMenuItem.Size = new System.Drawing.Size(100, 22);
            this.CreateMenuItem.Text = "新建";
            this.CreateMenuItem.Click += new System.EventHandler(this.CreateMenuItem_Click);
            // 
            // OpenMenuItem
            // 
            this.OpenMenuItem.Name = "OpenMenuItem";
            this.OpenMenuItem.Size = new System.Drawing.Size(100, 22);
            this.OpenMenuItem.Text = "打开";
            this.OpenMenuItem.Click += new System.EventHandler(this.OpenMenuItem_Click);
            // 
            // SaveMenuItem
            // 
            this.SaveMenuItem.Name = "SaveMenuItem";
            this.SaveMenuItem.Size = new System.Drawing.Size(100, 22);
            this.SaveMenuItem.Text = "保存";
            this.SaveMenuItem.Click += new System.EventHandler(this.SaveMenuItem_Click);
            // 
            // ExitMenuItem
            // 
            this.ExitMenuItem.Name = "ExitMenuItem";
            this.ExitMenuItem.Size = new System.Drawing.Size(100, 22);
            this.ExitMenuItem.Text = "退出";
            this.ExitMenuItem.Click += new System.EventHandler(this.ExitMenuItem_Click);
            // 
            // HelpMenuItem
            // 
            this.HelpMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AboutMenuItem});
            this.HelpMenuItem.Name = "HelpMenuItem";
            this.HelpMenuItem.Size = new System.Drawing.Size(44, 21);
            this.HelpMenuItem.Text = "帮助";
            // 
            // AboutMenuItem
            // 
            this.AboutMenuItem.Name = "AboutMenuItem";
            this.AboutMenuItem.Size = new System.Drawing.Size(100, 22);
            this.AboutMenuItem.Text = "关于";
            this.AboutMenuItem.Click += new System.EventHandler(this.AboutMenuItem_Click);
            // 
            // SelectedColorButton
            // 
            this.SelectedColorButton.FlatAppearance.BorderSize = 0;
            this.SelectedColorButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SelectedColorButton.Location = new System.Drawing.Point(67, 22);
            this.SelectedColorButton.Name = "SelectedColorButton";
            this.SelectedColorButton.Size = new System.Drawing.Size(72, 72);
            this.SelectedColorButton.TabIndex = 5;
            this.SelectedColorButton.UseVisualStyleBackColor = true;
            // 
            // MainTopTab
            // 
            this.MainTopTab.Controls.Add(this.ColorPage);
            this.MainTopTab.Controls.Add(this.CozyColorPage);
            this.MainTopTab.Location = new System.Drawing.Point(432, 41);
            this.MainTopTab.Name = "MainTopTab";
            this.MainTopTab.SelectedIndex = 0;
            this.MainTopTab.Size = new System.Drawing.Size(720, 140);
            this.MainTopTab.TabIndex = 7;
            // 
            // ColorPage
            // 
            this.ColorPage.Controls.Add(this.ColorList);
            this.ColorPage.Controls.Add(this.SelectedColorButton);
            this.ColorPage.Location = new System.Drawing.Point(4, 22);
            this.ColorPage.Name = "ColorPage";
            this.ColorPage.Padding = new System.Windows.Forms.Padding(3);
            this.ColorPage.Size = new System.Drawing.Size(712, 114);
            this.ColorPage.TabIndex = 0;
            this.ColorPage.Text = "24色";
            this.ColorPage.UseVisualStyleBackColor = true;
            // 
            // CozyColorPage
            // 
            this.CozyColorPage.Controls.Add(this.pictureBox1);
            this.CozyColorPage.Controls.Add(this.button1);
            this.CozyColorPage.Location = new System.Drawing.Point(4, 22);
            this.CozyColorPage.Name = "CozyColorPage";
            this.CozyColorPage.Size = new System.Drawing.Size(712, 114);
            this.CozyColorPage.TabIndex = 1;
            this.CozyColorPage.Text = "CozyColor";
            this.CozyColorPage.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(108, 17);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(500, 80);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(15, 17);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(87, 80);
            this.button1.TabIndex = 0;
            this.button1.Text = "生成";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MainStatusStrip
            // 
            this.MainStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CurrPathStatusLabel});
            this.MainStatusStrip.Location = new System.Drawing.Point(0, 659);
            this.MainStatusStrip.Name = "MainStatusStrip";
            this.MainStatusStrip.Size = new System.Drawing.Size(1264, 22);
            this.MainStatusStrip.TabIndex = 8;
            this.MainStatusStrip.Text = "statusStrip1";
            // 
            // CurrPathStatusLabel
            // 
            this.CurrPathStatusLabel.Name = "CurrPathStatusLabel";
            this.CurrPathStatusLabel.Size = new System.Drawing.Size(88, 17);
            this.CurrPathStatusLabel.Text = "当前文件 ： 无";
            // 
            // PixelPainter
            // 
            this.PixelPainter.AutoScroll = true;
            this.PixelPainter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PixelPainter.Cursor = System.Windows.Forms.Cursors.Cross;
            this.PixelPainter.DefaultDrawColor = System.Drawing.Color.White;
            this.PixelPainter.Image = null;
            this.PixelPainter.Location = new System.Drawing.Point(436, 201);
            this.PixelPainter.Name = "PixelPainter";
            this.PixelPainter.Size = new System.Drawing.Size(710, 420);
            this.PixelPainter.SourceImage = null;
            this.PixelPainter.TabIndex = 9;
            this.PixelPainter.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PixelPainter_MouseDown);
            this.PixelPainter.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PixelPainter_MouseMove);
            this.PixelPainter.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PixelPainter_MouseUp);
            // 
            // ColorList
            // 
            this.ColorList.ColorItemMargin = new System.Windows.Forms.Padding(2);
            this.ColorList.ColorItemSize = new System.Drawing.Size(58, 20);
            this.ColorList.Location = new System.Drawing.Point(195, 22);
            this.ColorList.Name = "ColorList";
            this.ColorList.SelectedColor = System.Drawing.Color.Empty;
            this.ColorList.Size = new System.Drawing.Size(500, 72);
            this.ColorList.TabIndex = 2;
            this.ColorList.ColorSelectedEventHandler += new System.EventHandler<CozyPixel.Controls.ControlEventArgs.ColorEventAgs>(this.ColorList_ColorSelectedEventHandler);
            // 
            // MainLeftTab
            // 
            this.MainLeftTab.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.MainLeftTab.Controls.Add(this.SelectToolPage);
            this.MainLeftTab.Controls.Add(this.FileSelectPage);
            this.MainLeftTab.Controls.Add(this.GridOpeionPage);
            this.MainLeftTab.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.MainLeftTab.ItemSize = new System.Drawing.Size(18, 20);
            stringFormat1.Alignment = System.Drawing.StringAlignment.Center;
            stringFormat1.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.None;
            stringFormat1.LineAlignment = System.Drawing.StringAlignment.Center;
            stringFormat1.Trimming = System.Drawing.StringTrimming.Character;
            this.MainLeftTab.ItemTextFormat = stringFormat1;
            this.MainLeftTab.Location = new System.Drawing.Point(12, 41);
            this.MainLeftTab.Multiline = true;
            this.MainLeftTab.Name = "MainLeftTab";
            this.MainLeftTab.SelectedIndex = 0;
            this.MainLeftTab.Size = new System.Drawing.Size(300, 580);
            this.MainLeftTab.TabIndex = 6;
            // 
            // SelectToolPage
            // 
            this.SelectToolPage.Controls.Add(this.FillToolButton);
            this.SelectToolPage.Controls.Add(this.StrawToolButton);
            this.SelectToolPage.Controls.Add(this.EarserToolButton);
            this.SelectToolPage.Controls.Add(this.LineToolButton);
            this.SelectToolPage.Controls.Add(this.PencilToolButton);
            this.SelectToolPage.Location = new System.Drawing.Point(24, 4);
            this.SelectToolPage.Name = "SelectToolPage";
            this.SelectToolPage.Size = new System.Drawing.Size(272, 572);
            this.SelectToolPage.TabIndex = 2;
            this.SelectToolPage.Text = "工具选择";
            this.SelectToolPage.UseVisualStyleBackColor = true;
            // 
            // FillToolButton
            // 
            this.FillToolButton.Location = new System.Drawing.Point(88, 253);
            this.FillToolButton.Name = "FillToolButton";
            this.FillToolButton.Size = new System.Drawing.Size(75, 23);
            this.FillToolButton.TabIndex = 4;
            this.FillToolButton.Text = "填充工具";
            this.FillToolButton.UseVisualStyleBackColor = true;
            this.FillToolButton.Click += new System.EventHandler(this.FillToolButton_Click);
            // 
            // StrawToolButton
            // 
            this.StrawToolButton.Location = new System.Drawing.Point(88, 209);
            this.StrawToolButton.Name = "StrawToolButton";
            this.StrawToolButton.Size = new System.Drawing.Size(75, 23);
            this.StrawToolButton.TabIndex = 3;
            this.StrawToolButton.Text = "吸取工具";
            this.StrawToolButton.UseVisualStyleBackColor = true;
            this.StrawToolButton.Click += new System.EventHandler(this.StrawToolButton_Click);
            // 
            // EarserToolButton
            // 
            this.EarserToolButton.Location = new System.Drawing.Point(88, 162);
            this.EarserToolButton.Name = "EarserToolButton";
            this.EarserToolButton.Size = new System.Drawing.Size(75, 23);
            this.EarserToolButton.TabIndex = 2;
            this.EarserToolButton.Text = "橡皮擦";
            this.EarserToolButton.UseVisualStyleBackColor = true;
            this.EarserToolButton.Click += new System.EventHandler(this.EarserToolButton_Click);
            // 
            // LineToolButton
            // 
            this.LineToolButton.Location = new System.Drawing.Point(88, 113);
            this.LineToolButton.Name = "LineToolButton";
            this.LineToolButton.Size = new System.Drawing.Size(75, 23);
            this.LineToolButton.TabIndex = 1;
            this.LineToolButton.Text = "画线";
            this.LineToolButton.UseVisualStyleBackColor = true;
            this.LineToolButton.Click += new System.EventHandler(this.LineToolButton_Click);
            // 
            // PencilToolButton
            // 
            this.PencilToolButton.Location = new System.Drawing.Point(88, 65);
            this.PencilToolButton.Name = "PencilToolButton";
            this.PencilToolButton.Size = new System.Drawing.Size(75, 23);
            this.PencilToolButton.TabIndex = 0;
            this.PencilToolButton.Text = "铅笔";
            this.PencilToolButton.UseVisualStyleBackColor = true;
            this.PencilToolButton.Click += new System.EventHandler(this.PencilToolButton_Click);
            // 
            // FileSelectPage
            // 
            this.FileSelectPage.Controls.Add(this.RefreshThumbListButton);
            this.FileSelectPage.Controls.Add(this.ThumbListView);
            this.FileSelectPage.Controls.Add(this.DirectorySelectButton);
            this.FileSelectPage.Location = new System.Drawing.Point(24, 4);
            this.FileSelectPage.Name = "FileSelectPage";
            this.FileSelectPage.Padding = new System.Windows.Forms.Padding(3);
            this.FileSelectPage.Size = new System.Drawing.Size(272, 572);
            this.FileSelectPage.TabIndex = 1;
            this.FileSelectPage.Text = "文件选择";
            this.FileSelectPage.UseVisualStyleBackColor = true;
            // 
            // RefreshThumbListButton
            // 
            this.RefreshThumbListButton.Location = new System.Drawing.Point(60, 51);
            this.RefreshThumbListButton.Name = "RefreshThumbListButton";
            this.RefreshThumbListButton.Size = new System.Drawing.Size(155, 23);
            this.RefreshThumbListButton.TabIndex = 3;
            this.RefreshThumbListButton.Text = "刷新";
            this.RefreshThumbListButton.UseVisualStyleBackColor = true;
            this.RefreshThumbListButton.Click += new System.EventHandler(this.RefreshThumbListButton_Click);
            // 
            // ThumbListView
            // 
            this.ThumbListView.Location = new System.Drawing.Point(60, 80);
            this.ThumbListView.MaxNameLength = 12;
            this.ThumbListView.MultiSelect = false;
            this.ThumbListView.Name = "ThumbListView";
            this.ThumbListView.ShowItemToolTips = true;
            this.ThumbListView.Size = new System.Drawing.Size(155, 472);
            this.ThumbListView.TabIndex = 2;
            this.ThumbListView.UseCompatibleStateImageBehavior = false;
            this.ThumbListView.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.ThumbListView_ItemSelectionChanged);
            // 
            // DirectorySelectButton
            // 
            this.DirectorySelectButton.Location = new System.Drawing.Point(60, 22);
            this.DirectorySelectButton.Name = "DirectorySelectButton";
            this.DirectorySelectButton.Size = new System.Drawing.Size(155, 23);
            this.DirectorySelectButton.TabIndex = 0;
            this.DirectorySelectButton.Text = "更换当前目录";
            this.DirectorySelectButton.UseVisualStyleBackColor = true;
            this.DirectorySelectButton.Click += new System.EventHandler(this.DirectorySelectButton_Click);
            // 
            // GridOpeionPage
            // 
            this.GridOpeionPage.Controls.Add(this.GridWidthLabel);
            this.GridOpeionPage.Controls.Add(this.GridWidthBox);
            this.GridOpeionPage.Controls.Add(this.ShowGridCheckBox);
            this.GridOpeionPage.Controls.Add(this.GridColorButton);
            this.GridOpeionPage.Controls.Add(this.ColorLabel);
            this.GridOpeionPage.ForeColor = System.Drawing.SystemColors.ControlText;
            this.GridOpeionPage.Location = new System.Drawing.Point(24, 4);
            this.GridOpeionPage.Name = "GridOpeionPage";
            this.GridOpeionPage.Padding = new System.Windows.Forms.Padding(3);
            this.GridOpeionPage.Size = new System.Drawing.Size(272, 572);
            this.GridOpeionPage.TabIndex = 0;
            this.GridOpeionPage.Text = "网格选项";
            this.GridOpeionPage.UseVisualStyleBackColor = true;
            // 
            // GridWidthLabel
            // 
            this.GridWidthLabel.AutoSize = true;
            this.GridWidthLabel.Location = new System.Drawing.Point(87, 235);
            this.GridWidthLabel.Name = "GridWidthLabel";
            this.GridWidthLabel.Size = new System.Drawing.Size(53, 12);
            this.GridWidthLabel.TabIndex = 4;
            this.GridWidthLabel.Text = "网格宽度";
            // 
            // GridWidthBox
            // 
            this.GridWidthBox.Location = new System.Drawing.Point(87, 250);
            this.GridWidthBox.Name = "GridWidthBox";
            this.GridWidthBox.Size = new System.Drawing.Size(100, 21);
            this.GridWidthBox.TabIndex = 3;
            this.GridWidthBox.Text = "2";
            this.GridWidthBox.TextChanged += new System.EventHandler(this.GridWidthBox_TextChanged);
            // 
            // ShowGridCheckBox
            // 
            this.ShowGridCheckBox.AutoSize = true;
            this.ShowGridCheckBox.Checked = true;
            this.ShowGridCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ShowGridCheckBox.Location = new System.Drawing.Point(89, 69);
            this.ShowGridCheckBox.Name = "ShowGridCheckBox";
            this.ShowGridCheckBox.Size = new System.Drawing.Size(72, 16);
            this.ShowGridCheckBox.TabIndex = 2;
            this.ShowGridCheckBox.Text = "显示网格";
            this.ShowGridCheckBox.UseVisualStyleBackColor = true;
            this.ShowGridCheckBox.CheckedChanged += new System.EventHandler(this.ShowGridCheckBox_CheckedChanged);
            // 
            // GridColorButton
            // 
            this.GridColorButton.BackColor = System.Drawing.Color.Black;
            this.GridColorButton.FlatAppearance.BorderSize = 0;
            this.GridColorButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.GridColorButton.Location = new System.Drawing.Point(89, 152);
            this.GridColorButton.Name = "GridColorButton";
            this.GridColorButton.Size = new System.Drawing.Size(48, 48);
            this.GridColorButton.TabIndex = 1;
            this.GridColorButton.UseVisualStyleBackColor = false;
            this.GridColorButton.Click += new System.EventHandler(this.GridColorButton_Click);
            // 
            // ColorLabel
            // 
            this.ColorLabel.AutoSize = true;
            this.ColorLabel.Location = new System.Drawing.Point(87, 124);
            this.ColorLabel.Name = "ColorLabel";
            this.ColorLabel.Size = new System.Drawing.Size(53, 12);
            this.ColorLabel.TabIndex = 0;
            this.ColorLabel.Text = "网格颜色";
            // 
            // CozyPixelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.PixelPainter);
            this.Controls.Add(this.MainStatusStrip);
            this.Controls.Add(this.MainTopTab);
            this.Controls.Add(this.MainLeftTab);
            this.Controls.Add(this.MainStripMenu);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CozyPixelForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CozyPixel 0.6";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.MainStripMenu.ResumeLayout(false);
            this.MainStripMenu.PerformLayout();
            this.MainTopTab.ResumeLayout(false);
            this.ColorPage.ResumeLayout(false);
            this.CozyColorPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.MainStatusStrip.ResumeLayout(false);
            this.MainStatusStrip.PerformLayout();
            this.MainLeftTab.ResumeLayout(false);
            this.SelectToolPage.ResumeLayout(false);
            this.FileSelectPage.ResumeLayout(false);
            this.GridOpeionPage.ResumeLayout(false);
            this.GridOpeionPage.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip MainStripMenu;
        private System.Windows.Forms.ToolStripMenuItem FileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem HelpMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OpenMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ExitMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AboutMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SaveMenuItem;
        private Controls.VerticalTabControl MainLeftTab;
        private System.Windows.Forms.TabPage GridOpeionPage;
        private System.Windows.Forms.Button GridColorButton;
        private System.Windows.Forms.Label ColorLabel;
        private System.Windows.Forms.CheckBox ShowGridCheckBox;
        private System.Windows.Forms.Label GridWidthLabel;
        private System.Windows.Forms.TextBox GridWidthBox;
        private System.Windows.Forms.ToolStripMenuItem CreateMenuItem;
        private System.Windows.Forms.TabPage ColorPage;
        private Controls.ColorListView ColorList;
        private System.Windows.Forms.Button SelectedColorButton;
        private System.Windows.Forms.TabControl MainTopTab;
        private System.Windows.Forms.TabPage FileSelectPage;
        private System.Windows.Forms.Button DirectorySelectButton;
        private Controls.ImageListView ThumbListView;
        private System.Windows.Forms.StatusStrip MainStatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel CurrPathStatusLabel;
        private System.Windows.Forms.Button RefreshThumbListButton;
        private System.Windows.Forms.TabPage CozyColorPage;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TabPage SelectToolPage;
        private System.Windows.Forms.Button PencilToolButton;
        private System.Windows.Forms.Button LineToolButton;
        private System.Windows.Forms.Button EarserToolButton;
        private System.Windows.Forms.Button StrawToolButton;
        private System.Windows.Forms.Button FillToolButton;
        private Controls.PixelPainter PixelPainter;
    }
}

