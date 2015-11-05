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
            this.MainStripMenu = new System.Windows.Forms.MenuStrip();
            this.FileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CreateMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CancleMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ResumeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ZoomInMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ZoomOutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ZoomResetMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SelectedColorButton = new System.Windows.Forms.Button();
            this.MainTopTab = new MetroFramework.Controls.MetroTabControl();
            this.ColorPage = new MetroFramework.Controls.MetroTabPage();
            this.ColorList = new CozyPixel.Controls.ColorListView();
            this.CozyColorPage = new MetroFramework.Controls.MetroTabPage();
            this.GenerateButton = new MetroFramework.Controls.MetroButton();
            this.CozyColorListView = new CozyPixel.Controls.ColorListView();
            this.CozyAsciiPage = new MetroFramework.Controls.MetroTabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.MainStatusStrip = new System.Windows.Forms.StatusStrip();
            this.CurrPathStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.MainLeftTab = new MetroFramework.Controls.MetroTabControl();
            this.SelectToolPage = new MetroFramework.Controls.MetroTabPage();
            this.FillToolButton = new MetroFramework.Controls.MetroButton();
            this.StrawToolButton = new MetroFramework.Controls.MetroButton();
            this.EarserToolButton = new MetroFramework.Controls.MetroButton();
            this.LineToolButton = new MetroFramework.Controls.MetroButton();
            this.PencilToolButton = new MetroFramework.Controls.MetroButton();
            this.FileSelectPage = new MetroFramework.Controls.MetroTabPage();
            this.RefreshThumbListButton = new MetroFramework.Controls.MetroButton();
            this.ThumbListView = new CozyPixel.Controls.ImageListView();
            this.DirectorySelectButton = new MetroFramework.Controls.MetroButton();
            this.GridOpeionPage = new MetroFramework.Controls.MetroTabPage();
            this.GridAlphaTrack = new MetroFramework.Controls.MetroTrackBar();
            this.GridAlphaLabel = new System.Windows.Forms.Label();
            this.ColorPicker = new CozyPixel.Controls.ColorPickerComboBox();
            this.GridWidthLabel = new MetroFramework.Controls.MetroLabel();
            this.GridWidthBox = new System.Windows.Forms.TextBox();
            this.ShowGridCheckBox = new MetroFramework.Controls.MetroToggle();
            this.GridColorButton = new System.Windows.Forms.Button();
            this.ColorLabel = new MetroFramework.Controls.MetroLabel();
            this.PixelPainter = new CozyPixel.Controls.PixelPainter();
            this.MainStripMenu.SuspendLayout();
            this.MainTopTab.SuspendLayout();
            this.ColorPage.SuspendLayout();
            this.CozyColorPage.SuspendLayout();
            this.CozyAsciiPage.SuspendLayout();
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
            this.EditMenuItem,
            this.HelpMenuItem});
            this.MainStripMenu.Location = new System.Drawing.Point(20, 60);
            this.MainStripMenu.Name = "MainStripMenu";
            this.MainStripMenu.Size = new System.Drawing.Size(1224, 25);
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
            this.FileMenuItem.Size = new System.Drawing.Size(58, 21);
            this.FileMenuItem.Text = "文件(&F)";
            // 
            // CreateMenuItem
            // 
            this.CreateMenuItem.Name = "CreateMenuItem";
            this.CreateMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.CreateMenuItem.Size = new System.Drawing.Size(147, 22);
            this.CreateMenuItem.Text = "新建";
            this.CreateMenuItem.Click += new System.EventHandler(this.CreateMenuItem_Click);
            // 
            // OpenMenuItem
            // 
            this.OpenMenuItem.Name = "OpenMenuItem";
            this.OpenMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.OpenMenuItem.Size = new System.Drawing.Size(147, 22);
            this.OpenMenuItem.Text = "打开";
            this.OpenMenuItem.Click += new System.EventHandler(this.OpenMenuItem_Click);
            // 
            // SaveMenuItem
            // 
            this.SaveMenuItem.Name = "SaveMenuItem";
            this.SaveMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.SaveMenuItem.Size = new System.Drawing.Size(147, 22);
            this.SaveMenuItem.Text = "保存";
            this.SaveMenuItem.Click += new System.EventHandler(this.SaveMenuItem_Click);
            // 
            // ExitMenuItem
            // 
            this.ExitMenuItem.Name = "ExitMenuItem";
            this.ExitMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.ExitMenuItem.Size = new System.Drawing.Size(147, 22);
            this.ExitMenuItem.Text = "退出";
            this.ExitMenuItem.Click += new System.EventHandler(this.ExitMenuItem_Click);
            // 
            // EditMenuItem
            // 
            this.EditMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CancleMenuItem,
            this.ResumeMenuItem,
            this.toolStripSeparator1,
            this.ZoomInMenuItem,
            this.ZoomOutMenuItem,
            this.ZoomResetMenuItem});
            this.EditMenuItem.Name = "EditMenuItem";
            this.EditMenuItem.Size = new System.Drawing.Size(59, 21);
            this.EditMenuItem.Text = "编辑(&E)";
            // 
            // CancleMenuItem
            // 
            this.CancleMenuItem.Name = "CancleMenuItem";
            this.CancleMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.CancleMenuItem.Size = new System.Drawing.Size(200, 22);
            this.CancleMenuItem.Text = "撤销";
            this.CancleMenuItem.Click += new System.EventHandler(this.CancleMenuItem_Click);
            // 
            // ResumeMenuItem
            // 
            this.ResumeMenuItem.Name = "ResumeMenuItem";
            this.ResumeMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.ResumeMenuItem.Size = new System.Drawing.Size(200, 22);
            this.ResumeMenuItem.Text = "恢复";
            this.ResumeMenuItem.Click += new System.EventHandler(this.ResumeMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(197, 6);
            // 
            // ZoomInMenuItem
            // 
            this.ZoomInMenuItem.Name = "ZoomInMenuItem";
            this.ZoomInMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Oemplus)));
            this.ZoomInMenuItem.Size = new System.Drawing.Size(200, 22);
            this.ZoomInMenuItem.Text = "放大";
            this.ZoomInMenuItem.Click += new System.EventHandler(this.ZoomInMenuItem_Click);
            // 
            // ZoomOutMenuItem
            // 
            this.ZoomOutMenuItem.Name = "ZoomOutMenuItem";
            this.ZoomOutMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.OemMinus)));
            this.ZoomOutMenuItem.Size = new System.Drawing.Size(200, 22);
            this.ZoomOutMenuItem.Text = "缩小";
            this.ZoomOutMenuItem.Click += new System.EventHandler(this.ZoomOutMenuItem_Click);
            // 
            // ZoomResetMenuItem
            // 
            this.ZoomResetMenuItem.Name = "ZoomResetMenuItem";
            this.ZoomResetMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D0)));
            this.ZoomResetMenuItem.Size = new System.Drawing.Size(200, 22);
            this.ZoomResetMenuItem.Text = "恢复默认大小";
            this.ZoomResetMenuItem.Click += new System.EventHandler(this.ZoomResetMenuItem_Click);
            // 
            // HelpMenuItem
            // 
            this.HelpMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AboutMenuItem});
            this.HelpMenuItem.Name = "HelpMenuItem";
            this.HelpMenuItem.Size = new System.Drawing.Size(61, 21);
            this.HelpMenuItem.Text = "帮助(&H)";
            // 
            // AboutMenuItem
            // 
            this.AboutMenuItem.Name = "AboutMenuItem";
            this.AboutMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.AboutMenuItem.Size = new System.Drawing.Size(121, 22);
            this.AboutMenuItem.Text = "关于";
            this.AboutMenuItem.Click += new System.EventHandler(this.AboutMenuItem_Click);
            // 
            // SelectedColorButton
            // 
            this.SelectedColorButton.Location = new System.Drawing.Point(67, 22);
            this.SelectedColorButton.Name = "SelectedColorButton";
            this.SelectedColorButton.Size = new System.Drawing.Size(72, 72);
            this.SelectedColorButton.TabIndex = 5;
            // 
            // MainTopTab
            // 
            this.MainTopTab.Controls.Add(this.ColorPage);
            this.MainTopTab.Controls.Add(this.CozyColorPage);
            this.MainTopTab.Controls.Add(this.CozyAsciiPage);
            this.MainTopTab.Location = new System.Drawing.Point(397, 99);
            this.MainTopTab.Name = "MainTopTab";
            this.MainTopTab.SelectedIndex = 2;
            this.MainTopTab.Size = new System.Drawing.Size(720, 140);
            this.MainTopTab.TabIndex = 7;
            this.MainTopTab.UseSelectable = true;
            // 
            // ColorPage
            // 
            this.ColorPage.Controls.Add(this.ColorList);
            this.ColorPage.Controls.Add(this.SelectedColorButton);
            this.ColorPage.HorizontalScrollbarBarColor = true;
            this.ColorPage.HorizontalScrollbarHighlightOnWheel = false;
            this.ColorPage.HorizontalScrollbarSize = 10;
            this.ColorPage.Location = new System.Drawing.Point(4, 38);
            this.ColorPage.Name = "ColorPage";
            this.ColorPage.Padding = new System.Windows.Forms.Padding(3);
            this.ColorPage.Size = new System.Drawing.Size(712, 98);
            this.ColorPage.TabIndex = 0;
            this.ColorPage.Text = "24色";
            this.ColorPage.UseVisualStyleBackColor = true;
            this.ColorPage.VerticalScrollbarBarColor = true;
            this.ColorPage.VerticalScrollbarHighlightOnWheel = false;
            this.ColorPage.VerticalScrollbarSize = 10;
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
            // CozyColorPage
            // 
            this.CozyColorPage.Controls.Add(this.GenerateButton);
            this.CozyColorPage.Controls.Add(this.CozyColorListView);
            this.CozyColorPage.HorizontalScrollbarBarColor = true;
            this.CozyColorPage.HorizontalScrollbarHighlightOnWheel = false;
            this.CozyColorPage.HorizontalScrollbarSize = 10;
            this.CozyColorPage.Location = new System.Drawing.Point(4, 38);
            this.CozyColorPage.Name = "CozyColorPage";
            this.CozyColorPage.Size = new System.Drawing.Size(712, 98);
            this.CozyColorPage.TabIndex = 1;
            this.CozyColorPage.Text = "CozyColor";
            this.CozyColorPage.UseVisualStyleBackColor = true;
            this.CozyColorPage.VerticalScrollbarBarColor = true;
            this.CozyColorPage.VerticalScrollbarHighlightOnWheel = false;
            this.CozyColorPage.VerticalScrollbarSize = 10;
            // 
            // GenerateButton
            // 
            this.GenerateButton.Location = new System.Drawing.Point(15, 17);
            this.GenerateButton.Name = "GenerateButton";
            this.GenerateButton.Size = new System.Drawing.Size(87, 80);
            this.GenerateButton.TabIndex = 0;
            this.GenerateButton.Text = "生成";
            this.GenerateButton.UseSelectable = true;
            this.GenerateButton.Click += new System.EventHandler(this.GenerateButton_Click);
            // 
            // CozyColorListView
            // 
            this.CozyColorListView.ColorItemMargin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.CozyColorListView.ColorItemSize = new System.Drawing.Size(51, 72);
            this.CozyColorListView.Location = new System.Drawing.Point(181, 23);
            this.CozyColorListView.Name = "CozyColorListView";
            this.CozyColorListView.SelectedColor = System.Drawing.Color.Empty;
            this.CozyColorListView.Size = new System.Drawing.Size(500, 72);
            this.CozyColorListView.TabIndex = 2;
            this.CozyColorListView.ColorSelectedEventHandler += new System.EventHandler<CozyPixel.Controls.ControlEventArgs.ColorEventAgs>(this.ColorList_ColorSelectedEventHandler);
            // 
            // CozyAsciiPage
            // 
            this.CozyAsciiPage.Controls.Add(this.button1);
            this.CozyAsciiPage.HorizontalScrollbarBarColor = true;
            this.CozyAsciiPage.HorizontalScrollbarHighlightOnWheel = false;
            this.CozyAsciiPage.HorizontalScrollbarSize = 10;
            this.CozyAsciiPage.Location = new System.Drawing.Point(4, 38);
            this.CozyAsciiPage.Name = "CozyAsciiPage";
            this.CozyAsciiPage.Size = new System.Drawing.Size(712, 98);
            this.CozyAsciiPage.TabIndex = 2;
            this.CozyAsciiPage.Text = "CozyAscii";
            this.CozyAsciiPage.VerticalScrollbarBarColor = true;
            this.CozyAsciiPage.VerticalScrollbarHighlightOnWheel = false;
            this.CozyAsciiPage.VerticalScrollbarSize = 10;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(229, 27);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(290, 53);
            this.button1.TabIndex = 2;
            this.button1.Text = "降纬打击";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MainStatusStrip
            // 
            this.MainStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CurrPathStatusLabel});
            this.MainStatusStrip.Location = new System.Drawing.Point(20, 639);
            this.MainStatusStrip.Name = "MainStatusStrip";
            this.MainStatusStrip.Size = new System.Drawing.Size(1224, 22);
            this.MainStatusStrip.TabIndex = 8;
            this.MainStatusStrip.Text = "statusStrip1";
            // 
            // CurrPathStatusLabel
            // 
            this.CurrPathStatusLabel.Name = "CurrPathStatusLabel";
            this.CurrPathStatusLabel.Size = new System.Drawing.Size(88, 17);
            this.CurrPathStatusLabel.Text = "当前文件 ： 无";
            // 
            // MainLeftTab
            // 
            this.MainLeftTab.Controls.Add(this.SelectToolPage);
            this.MainLeftTab.Controls.Add(this.FileSelectPage);
            this.MainLeftTab.Controls.Add(this.GridOpeionPage);
            this.MainLeftTab.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.MainLeftTab.ItemSize = new System.Drawing.Size(18, 80);
            this.MainLeftTab.Location = new System.Drawing.Point(23, 95);
            this.MainLeftTab.Multiline = true;
            this.MainLeftTab.Name = "MainLeftTab";
            this.MainLeftTab.SelectedIndex = 0;
            this.MainLeftTab.Size = new System.Drawing.Size(300, 520);
            this.MainLeftTab.TabIndex = 6;
            this.MainLeftTab.UseSelectable = true;
            // 
            // SelectToolPage
            // 
            this.SelectToolPage.Controls.Add(this.FillToolButton);
            this.SelectToolPage.Controls.Add(this.StrawToolButton);
            this.SelectToolPage.Controls.Add(this.EarserToolButton);
            this.SelectToolPage.Controls.Add(this.LineToolButton);
            this.SelectToolPage.Controls.Add(this.PencilToolButton);
            this.SelectToolPage.HorizontalScrollbarBarColor = true;
            this.SelectToolPage.HorizontalScrollbarHighlightOnWheel = false;
            this.SelectToolPage.HorizontalScrollbarSize = 10;
            this.SelectToolPage.Location = new System.Drawing.Point(4, 84);
            this.SelectToolPage.Name = "SelectToolPage";
            this.SelectToolPage.Size = new System.Drawing.Size(292, 432);
            this.SelectToolPage.TabIndex = 2;
            this.SelectToolPage.Text = "工具选择";
            this.SelectToolPage.UseVisualStyleBackColor = true;
            this.SelectToolPage.VerticalScrollbarBarColor = true;
            this.SelectToolPage.VerticalScrollbarHighlightOnWheel = false;
            this.SelectToolPage.VerticalScrollbarSize = 10;
            // 
            // FillToolButton
            // 
            this.FillToolButton.Location = new System.Drawing.Point(88, 253);
            this.FillToolButton.Name = "FillToolButton";
            this.FillToolButton.Size = new System.Drawing.Size(129, 23);
            this.FillToolButton.TabIndex = 4;
            this.FillToolButton.Text = "填充工具 (Ctrl +5)";
            this.FillToolButton.UseSelectable = true;
            this.FillToolButton.Click += new System.EventHandler(this.FillToolButton_Click);
            // 
            // StrawToolButton
            // 
            this.StrawToolButton.Location = new System.Drawing.Point(88, 209);
            this.StrawToolButton.Name = "StrawToolButton";
            this.StrawToolButton.Size = new System.Drawing.Size(129, 23);
            this.StrawToolButton.TabIndex = 3;
            this.StrawToolButton.Text = "吸取工具 (Ctrl + 4)";
            this.StrawToolButton.UseSelectable = true;
            this.StrawToolButton.Click += new System.EventHandler(this.StrawToolButton_Click);
            // 
            // EarserToolButton
            // 
            this.EarserToolButton.Location = new System.Drawing.Point(88, 164);
            this.EarserToolButton.Name = "EarserToolButton";
            this.EarserToolButton.Size = new System.Drawing.Size(129, 23);
            this.EarserToolButton.TabIndex = 2;
            this.EarserToolButton.Text = "橡皮擦 (Ctrl + 3)";
            this.EarserToolButton.UseSelectable = true;
            this.EarserToolButton.Click += new System.EventHandler(this.EarserToolButton_Click);
            // 
            // LineToolButton
            // 
            this.LineToolButton.Location = new System.Drawing.Point(88, 113);
            this.LineToolButton.Name = "LineToolButton";
            this.LineToolButton.Size = new System.Drawing.Size(129, 23);
            this.LineToolButton.TabIndex = 1;
            this.LineToolButton.Text = "画线 (Ctrl +2)";
            this.LineToolButton.UseSelectable = true;
            this.LineToolButton.Click += new System.EventHandler(this.LineToolButton_Click);
            // 
            // PencilToolButton
            // 
            this.PencilToolButton.Location = new System.Drawing.Point(88, 65);
            this.PencilToolButton.Name = "PencilToolButton";
            this.PencilToolButton.Size = new System.Drawing.Size(129, 23);
            this.PencilToolButton.TabIndex = 0;
            this.PencilToolButton.Text = "铅笔 (Ctrl + 1)";
            this.PencilToolButton.UseSelectable = true;
            this.PencilToolButton.Click += new System.EventHandler(this.PencilToolButton_Click);
            // 
            // FileSelectPage
            // 
            this.FileSelectPage.Controls.Add(this.RefreshThumbListButton);
            this.FileSelectPage.Controls.Add(this.ThumbListView);
            this.FileSelectPage.Controls.Add(this.DirectorySelectButton);
            this.FileSelectPage.HorizontalScrollbarBarColor = true;
            this.FileSelectPage.HorizontalScrollbarHighlightOnWheel = false;
            this.FileSelectPage.HorizontalScrollbarSize = 10;
            this.FileSelectPage.Location = new System.Drawing.Point(4, 84);
            this.FileSelectPage.Name = "FileSelectPage";
            this.FileSelectPage.Padding = new System.Windows.Forms.Padding(3);
            this.FileSelectPage.Size = new System.Drawing.Size(292, 432);
            this.FileSelectPage.TabIndex = 1;
            this.FileSelectPage.Text = "文件选择";
            this.FileSelectPage.UseVisualStyleBackColor = true;
            this.FileSelectPage.VerticalScrollbarBarColor = true;
            this.FileSelectPage.VerticalScrollbarHighlightOnWheel = false;
            this.FileSelectPage.VerticalScrollbarSize = 10;
            // 
            // RefreshThumbListButton
            // 
            this.RefreshThumbListButton.Location = new System.Drawing.Point(60, 51);
            this.RefreshThumbListButton.Name = "RefreshThumbListButton";
            this.RefreshThumbListButton.Size = new System.Drawing.Size(155, 23);
            this.RefreshThumbListButton.TabIndex = 3;
            this.RefreshThumbListButton.Text = "刷新";
            this.RefreshThumbListButton.UseSelectable = true;
            this.RefreshThumbListButton.Click += new System.EventHandler(this.RefreshThumbListButton_Click);
            // 
            // ThumbListView
            // 
            this.ThumbListView.Location = new System.Drawing.Point(60, 80);
            this.ThumbListView.MaxNameLength = 12;
            this.ThumbListView.MultiSelect = false;
            this.ThumbListView.Name = "ThumbListView";
            this.ThumbListView.ShowItemToolTips = true;
            this.ThumbListView.Size = new System.Drawing.Size(155, 330);
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
            this.DirectorySelectButton.UseSelectable = true;
            this.DirectorySelectButton.Click += new System.EventHandler(this.DirectorySelectButton_Click);
            // 
            // GridOpeionPage
            // 
            this.GridOpeionPage.Controls.Add(this.GridAlphaTrack);
            this.GridOpeionPage.Controls.Add(this.GridAlphaLabel);
            this.GridOpeionPage.Controls.Add(this.ColorPicker);
            this.GridOpeionPage.Controls.Add(this.GridWidthLabel);
            this.GridOpeionPage.Controls.Add(this.GridWidthBox);
            this.GridOpeionPage.Controls.Add(this.ShowGridCheckBox);
            this.GridOpeionPage.Controls.Add(this.GridColorButton);
            this.GridOpeionPage.Controls.Add(this.ColorLabel);
            this.GridOpeionPage.ForeColor = System.Drawing.SystemColors.ControlText;
            this.GridOpeionPage.HorizontalScrollbarBarColor = true;
            this.GridOpeionPage.HorizontalScrollbarHighlightOnWheel = false;
            this.GridOpeionPage.HorizontalScrollbarSize = 10;
            this.GridOpeionPage.Location = new System.Drawing.Point(4, 84);
            this.GridOpeionPage.Name = "GridOpeionPage";
            this.GridOpeionPage.Padding = new System.Windows.Forms.Padding(3);
            this.GridOpeionPage.Size = new System.Drawing.Size(292, 432);
            this.GridOpeionPage.TabIndex = 0;
            this.GridOpeionPage.Text = "网格选项";
            this.GridOpeionPage.UseVisualStyleBackColor = true;
            this.GridOpeionPage.VerticalScrollbarBarColor = true;
            this.GridOpeionPage.VerticalScrollbarHighlightOnWheel = false;
            this.GridOpeionPage.VerticalScrollbarSize = 10;
            // 
            // GridAlphaTrack
            // 
            this.GridAlphaTrack.BackColor = System.Drawing.Color.Transparent;
            this.GridAlphaTrack.Location = new System.Drawing.Point(85, 365);
            this.GridAlphaTrack.Maximum = 255;
            this.GridAlphaTrack.Name = "GridAlphaTrack";
            this.GridAlphaTrack.Size = new System.Drawing.Size(100, 23);
            this.GridAlphaTrack.TabIndex = 8;
            this.GridAlphaTrack.Text = "GridAlpha";
            this.GridAlphaTrack.Value = 255;
            this.GridAlphaTrack.Scroll += new System.Windows.Forms.ScrollEventHandler(this.GridAlphaTrack_Scroll);
            // 
            // GridAlphaLabel
            // 
            this.GridAlphaLabel.AutoSize = true;
            this.GridAlphaLabel.Location = new System.Drawing.Point(83, 336);
            this.GridAlphaLabel.Name = "GridAlphaLabel";
            this.GridAlphaLabel.Size = new System.Drawing.Size(65, 12);
            this.GridAlphaLabel.TabIndex = 7;
            this.GridAlphaLabel.Text = "网格透明度";
            // 
            // ColorPicker
            // 
            this.ColorPicker.ColorWidth = 50;
            this.ColorPicker.DropDownWidth = 150;
            this.ColorPicker.ItemHeight = 23;
            this.ColorPicker.Location = new System.Drawing.Point(85, 277);
            this.ColorPicker.Name = "ColorPicker";
            this.ColorPicker.SelectedColor = System.Drawing.Color.Empty;
            this.ColorPicker.Size = new System.Drawing.Size(100, 29);
            this.ColorPicker.TabIndex = 5;
            this.ColorPicker.UseSelectable = true;
            this.ColorPicker.ColorPickerSelectedColorChanged += new System.EventHandler<CozyPixel.Controls.ControlEventArgs.ColorEventAgs>(this.ColorPicker_ColorPickerSelectedColorChanged);
            // 
            // GridWidthLabel
            // 
            this.GridWidthLabel.AutoSize = true;
            this.GridWidthLabel.Location = new System.Drawing.Point(85, 203);
            this.GridWidthLabel.Name = "GridWidthLabel";
            this.GridWidthLabel.Size = new System.Drawing.Size(65, 19);
            this.GridWidthLabel.TabIndex = 4;
            this.GridWidthLabel.Text = "网格宽度";
            // 
            // GridWidthBox
            // 
            this.GridWidthBox.Location = new System.Drawing.Point(85, 225);
            this.GridWidthBox.Name = "GridWidthBox";
            this.GridWidthBox.ReadOnly = true;
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
            this.ShowGridCheckBox.Location = new System.Drawing.Point(87, 44);
            this.ShowGridCheckBox.Name = "ShowGridCheckBox";
            this.ShowGridCheckBox.Size = new System.Drawing.Size(80, 16);
            this.ShowGridCheckBox.TabIndex = 2;
            this.ShowGridCheckBox.Text = "On";
            this.ShowGridCheckBox.UseSelectable = true;
            this.ShowGridCheckBox.CheckedChanged += new System.EventHandler(this.ShowGridCheckBox_CheckedChanged);
            // 
            // GridColorButton
            // 
            this.GridColorButton.BackColor = System.Drawing.Color.Black;
            this.GridColorButton.Location = new System.Drawing.Point(87, 127);
            this.GridColorButton.Name = "GridColorButton";
            this.GridColorButton.Size = new System.Drawing.Size(48, 48);
            this.GridColorButton.TabIndex = 1;
            this.GridColorButton.UseVisualStyleBackColor = false;
            this.GridColorButton.Click += new System.EventHandler(this.GridColorButton_Click);
            // 
            // ColorLabel
            // 
            this.ColorLabel.AutoSize = true;
            this.ColorLabel.Location = new System.Drawing.Point(85, 99);
            this.ColorLabel.Name = "ColorLabel";
            this.ColorLabel.Size = new System.Drawing.Size(65, 19);
            this.ColorLabel.TabIndex = 0;
            this.ColorLabel.Text = "网格颜色";
            // 
            // PixelPainter
            // 
            this.PixelPainter.AutoScroll = true;
            this.PixelPainter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PixelPainter.Cursor = System.Windows.Forms.Cursors.Cross;
            this.PixelPainter.DefaultDrawColor = System.Drawing.Color.White;
            this.PixelPainter.Image = null;
            this.PixelPainter.Location = new System.Drawing.Point(397, 261);
            this.PixelPainter.Name = "PixelPainter";
            this.PixelPainter.Size = new System.Drawing.Size(716, 354);
            this.PixelPainter.SourceImage = null;
            this.PixelPainter.TabIndex = 9;
            this.PixelPainter.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PixelPainter_MouseDown);
            this.PixelPainter.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PixelPainter_MouseMove);
            this.PixelPainter.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PixelPainter_MouseUp);
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
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CozyPixelForm";
            this.Text = "CozyPixel 0.9";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CozyPixelForm_KeyDown);
            this.MainStripMenu.ResumeLayout(false);
            this.MainStripMenu.PerformLayout();
            this.MainTopTab.ResumeLayout(false);
            this.ColorPage.ResumeLayout(false);
            this.CozyColorPage.ResumeLayout(false);
            this.CozyAsciiPage.ResumeLayout(false);
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
        private MetroFramework.Controls.MetroTabControl MainLeftTab;
        private MetroFramework.Controls.MetroTabPage GridOpeionPage;
        private System.Windows.Forms.Button GridColorButton;
        private MetroFramework.Controls.MetroToggle ShowGridCheckBox;
        private MetroFramework.Controls.MetroLabel GridWidthLabel;
        private System.Windows.Forms.TextBox GridWidthBox;
        private System.Windows.Forms.ToolStripMenuItem CreateMenuItem;
        private MetroFramework.Controls.MetroTabPage ColorPage;
        private Controls.ColorListView ColorList;
        private System.Windows.Forms.Button SelectedColorButton;
        private MetroFramework.Controls.MetroTabControl MainTopTab;

        private MetroFramework.Controls.MetroTabPage FileSelectPage;
        private MetroFramework.Controls.MetroButton DirectorySelectButton;
        private Controls.ImageListView ThumbListView;
        private System.Windows.Forms.StatusStrip MainStatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel CurrPathStatusLabel;
        private MetroFramework.Controls.MetroButton RefreshThumbListButton;
        private MetroFramework.Controls.MetroTabPage CozyColorPage;

        private MetroFramework.Controls.MetroButton GenerateButton;
        private MetroFramework.Controls.MetroTabPage SelectToolPage;
        private MetroFramework.Controls.MetroButton PencilToolButton;
        private MetroFramework.Controls.MetroButton LineToolButton;
        private MetroFramework.Controls.MetroButton EarserToolButton;
        private MetroFramework.Controls.MetroButton StrawToolButton;

        private MetroFramework.Controls.MetroButton FillToolButton;
        private Controls.PixelPainter PixelPainter;
        private Controls.ColorPickerComboBox ColorPicker;
        private MetroFramework.Controls.MetroLabel ColorLabel;
        private System.Windows.Forms.ToolStripMenuItem EditMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CancleMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ResumeMenuItem;
        private Controls.ColorListView CozyColorListView;
        private System.Windows.Forms.Label GridAlphaLabel;
        private MetroFramework.Controls.MetroTrackBar GridAlphaTrack;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem ZoomInMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ZoomOutMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ZoomResetMenuItem;
        private MetroFramework.Controls.MetroTabPage CozyAsciiPage;
        private System.Windows.Forms.Button button1;
    }
}

