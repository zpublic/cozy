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
            this.ColorPage = new System.Windows.Forms.TabPage();
            this.SelectedColorButton = new System.Windows.Forms.Button();
            this.MainTopTab = new System.Windows.Forms.TabControl();
            this.ThumbMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.OpenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainStatusStrip = new System.Windows.Forms.StatusStrip();
            this.CurrPathStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.ColorList = new CozyPixel.Controls.ColorListView();
            this.MainLeftTab = new CozyPixel.Controls.VerticalTabControl();
            this.GridOpeionPage = new System.Windows.Forms.TabPage();
            this.GridWidthLabel = new System.Windows.Forms.Label();
            this.GridWidthBox = new System.Windows.Forms.TextBox();
            this.ShowGridCheckBox = new System.Windows.Forms.CheckBox();
            this.GridColorButton = new System.Windows.Forms.Button();
            this.ColorLabel = new System.Windows.Forms.Label();
            this.FileSelectPage = new System.Windows.Forms.TabPage();
            this.RefreshThumbListButton = new System.Windows.Forms.Button();
            this.ThumbListView = new CozyPixel.Controls.ImageListView();
            this.DirectorySelectButton = new System.Windows.Forms.Button();
            this.PixelPainter = new CozyPixel.Controls.PixelPaintControl();
            this.MainStripMenu.SuspendLayout();
            this.ColorPage.SuspendLayout();
            this.MainTopTab.SuspendLayout();
            this.ThumbMenuStrip.SuspendLayout();
            this.MainStatusStrip.SuspendLayout();
            this.MainLeftTab.SuspendLayout();
            this.GridOpeionPage.SuspendLayout();
            this.FileSelectPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PixelPainter)).BeginInit();
            this.SuspendLayout();
            // 
            // MainStripMenu
            // 
            this.MainStripMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenuItem,
            this.HelpMenuItem});
            this.MainStripMenu.Location = new System.Drawing.Point(0, 0);
            this.MainStripMenu.Name = "MainStripMenu";
            this.MainStripMenu.Size = new System.Drawing.Size(1008, 25);
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
            // ColorPage
            // 
            this.ColorPage.Controls.Add(this.ColorList);
            this.ColorPage.Controls.Add(this.SelectedColorButton);
            this.ColorPage.Location = new System.Drawing.Point(4, 22);
            this.ColorPage.Name = "ColorPage";
            this.ColorPage.Padding = new System.Windows.Forms.Padding(3);
            this.ColorPage.Size = new System.Drawing.Size(632, 114);
            this.ColorPage.TabIndex = 0;
            this.ColorPage.Text = "颜色";
            this.ColorPage.UseVisualStyleBackColor = true;
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
            this.MainTopTab.Location = new System.Drawing.Point(316, 28);
            this.MainTopTab.Name = "MainTopTab";
            this.MainTopTab.SelectedIndex = 0;
            this.MainTopTab.Size = new System.Drawing.Size(640, 140);
            this.MainTopTab.TabIndex = 7;
            // 
            // ThumbMenuStrip
            // 
            this.ThumbMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenToolStripMenuItem});
            this.ThumbMenuStrip.Name = "contextMenuStrip1";
            this.ThumbMenuStrip.Size = new System.Drawing.Size(101, 26);
            // 
            // OpenToolStripMenuItem
            // 
            this.OpenToolStripMenuItem.Name = "OpenToolStripMenuItem";
            this.OpenToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.OpenToolStripMenuItem.Text = "打开";
            this.OpenToolStripMenuItem.Click += new System.EventHandler(this.OpenToolStripMenuItem_Click);
            // 
            // MainStatusStrip
            // 
            this.MainStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CurrPathStatusLabel});
            this.MainStatusStrip.Location = new System.Drawing.Point(0, 707);
            this.MainStatusStrip.Name = "MainStatusStrip";
            this.MainStatusStrip.Size = new System.Drawing.Size(1008, 22);
            this.MainStatusStrip.TabIndex = 8;
            this.MainStatusStrip.Text = "statusStrip1";
            // 
            // CurrPathStatusLabel
            // 
            this.CurrPathStatusLabel.Name = "CurrPathStatusLabel";
            this.CurrPathStatusLabel.Size = new System.Drawing.Size(88, 17);
            this.CurrPathStatusLabel.Text = "当前文件 ： 无";
            // 
            // ColorList
            // 
            this.ColorList.ColorItemMargin = new System.Windows.Forms.Padding(2);
            this.ColorList.ColorItemSize = new System.Drawing.Size(46, 20);
            this.ColorList.Location = new System.Drawing.Point(195, 22);
            this.ColorList.Name = "ColorList";
            this.ColorList.SelectedColor = System.Drawing.Color.Empty;
            this.ColorList.Size = new System.Drawing.Size(400, 72);
            this.ColorList.TabIndex = 2;
            // 
            // MainLeftTab
            // 
            this.MainLeftTab.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.MainLeftTab.Controls.Add(this.GridOpeionPage);
            this.MainLeftTab.Controls.Add(this.FileSelectPage);
            this.MainLeftTab.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.MainLeftTab.ItemSize = new System.Drawing.Size(18, 20);
            stringFormat1.Alignment = System.Drawing.StringAlignment.Center;
            stringFormat1.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.None;
            stringFormat1.LineAlignment = System.Drawing.StringAlignment.Center;
            stringFormat1.Trimming = System.Drawing.StringTrimming.Character;
            this.MainLeftTab.ItemTextFormat = stringFormat1;
            this.MainLeftTab.Location = new System.Drawing.Point(12, 28);
            this.MainLeftTab.Multiline = true;
            this.MainLeftTab.Name = "MainLeftTab";
            this.MainLeftTab.SelectedIndex = 0;
            this.MainLeftTab.Size = new System.Drawing.Size(243, 672);
            this.MainLeftTab.TabIndex = 6;
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
            this.GridOpeionPage.Size = new System.Drawing.Size(215, 664);
            this.GridOpeionPage.TabIndex = 0;
            this.GridOpeionPage.Text = "网格选项";
            this.GridOpeionPage.UseVisualStyleBackColor = true;
            // 
            // GridWidthLabel
            // 
            this.GridWidthLabel.AutoSize = true;
            this.GridWidthLabel.Location = new System.Drawing.Point(63, 235);
            this.GridWidthLabel.Name = "GridWidthLabel";
            this.GridWidthLabel.Size = new System.Drawing.Size(53, 12);
            this.GridWidthLabel.TabIndex = 4;
            this.GridWidthLabel.Text = "网格宽度";
            // 
            // GridWidthBox
            // 
            this.GridWidthBox.Location = new System.Drawing.Point(63, 250);
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
            this.ShowGridCheckBox.Location = new System.Drawing.Point(65, 69);
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
            this.GridColorButton.Location = new System.Drawing.Point(65, 152);
            this.GridColorButton.Name = "GridColorButton";
            this.GridColorButton.Size = new System.Drawing.Size(48, 48);
            this.GridColorButton.TabIndex = 1;
            this.GridColorButton.UseVisualStyleBackColor = false;
            this.GridColorButton.Click += new System.EventHandler(this.GridColorButton_Click);
            // 
            // ColorLabel
            // 
            this.ColorLabel.AutoSize = true;
            this.ColorLabel.Location = new System.Drawing.Point(63, 124);
            this.ColorLabel.Name = "ColorLabel";
            this.ColorLabel.Size = new System.Drawing.Size(53, 12);
            this.ColorLabel.TabIndex = 0;
            this.ColorLabel.Text = "网格颜色";
            // 
            // FileSelectPage
            // 
            this.FileSelectPage.Controls.Add(this.RefreshThumbListButton);
            this.FileSelectPage.Controls.Add(this.ThumbListView);
            this.FileSelectPage.Controls.Add(this.DirectorySelectButton);
            this.FileSelectPage.Location = new System.Drawing.Point(24, 4);
            this.FileSelectPage.Name = "FileSelectPage";
            this.FileSelectPage.Padding = new System.Windows.Forms.Padding(3);
            this.FileSelectPage.Size = new System.Drawing.Size(215, 664);
            this.FileSelectPage.TabIndex = 1;
            this.FileSelectPage.Text = "文件选择";
            this.FileSelectPage.UseVisualStyleBackColor = true;
            // 
            // RefreshThumbListButton
            // 
            this.RefreshThumbListButton.Location = new System.Drawing.Point(26, 47);
            this.RefreshThumbListButton.Name = "RefreshThumbListButton";
            this.RefreshThumbListButton.Size = new System.Drawing.Size(155, 23);
            this.RefreshThumbListButton.TabIndex = 3;
            this.RefreshThumbListButton.Text = "刷新";
            this.RefreshThumbListButton.UseVisualStyleBackColor = true;
            this.RefreshThumbListButton.Click += new System.EventHandler(this.RefreshThumbListButton_Click);
            // 
            // ThumbListView
            // 
            this.ThumbListView.ContextMenuStrip = this.ThumbMenuStrip;
            this.ThumbListView.Location = new System.Drawing.Point(26, 76);
            this.ThumbListView.Name = "ThumbListView";
            this.ThumbListView.Size = new System.Drawing.Size(155, 553);
            this.ThumbListView.TabIndex = 2;
            this.ThumbListView.UseCompatibleStateImageBehavior = false;
            // 
            // DirectorySelectButton
            // 
            this.DirectorySelectButton.Location = new System.Drawing.Point(26, 18);
            this.DirectorySelectButton.Name = "DirectorySelectButton";
            this.DirectorySelectButton.Size = new System.Drawing.Size(155, 23);
            this.DirectorySelectButton.TabIndex = 0;
            this.DirectorySelectButton.Text = "更换当前目录";
            this.DirectorySelectButton.UseVisualStyleBackColor = true;
            this.DirectorySelectButton.Click += new System.EventHandler(this.DirectorySelectButton_Click);
            // 
            // PixelPainter
            // 
            this.PixelPainter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PixelPainter.Cursor = System.Windows.Forms.Cursors.Cross;
            this.PixelPainter.Location = new System.Drawing.Point(316, 188);
            this.PixelPainter.Name = "PixelPainter";
            this.PixelPainter.Size = new System.Drawing.Size(640, 512);
            this.PixelPainter.SourceImage = null;
            this.PixelPainter.TabIndex = 3;
            this.PixelPainter.TabStop = false;
            this.PixelPainter.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PictureBox_MouseDown);
            // 
            // CozyPixelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.MainStatusStrip);
            this.Controls.Add(this.MainTopTab);
            this.Controls.Add(this.MainLeftTab);
            this.Controls.Add(this.PixelPainter);
            this.Controls.Add(this.MainStripMenu);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CozyPixelForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CozyPixel 0.5";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.MainStripMenu.ResumeLayout(false);
            this.MainStripMenu.PerformLayout();
            this.ColorPage.ResumeLayout(false);
            this.MainTopTab.ResumeLayout(false);
            this.ThumbMenuStrip.ResumeLayout(false);
            this.MainStatusStrip.ResumeLayout(false);
            this.MainStatusStrip.PerformLayout();
            this.MainLeftTab.ResumeLayout(false);
            this.GridOpeionPage.ResumeLayout(false);
            this.GridOpeionPage.PerformLayout();
            this.FileSelectPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PixelPainter)).EndInit();
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
        private Controls.PixelPaintControl PixelPainter;
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
        private System.Windows.Forms.ContextMenuStrip ThumbMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem OpenToolStripMenuItem;
        private System.Windows.Forms.StatusStrip MainStatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel CurrPathStatusLabel;
        private System.Windows.Forms.Button RefreshThumbListButton;
    }
}

