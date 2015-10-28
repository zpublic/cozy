﻿namespace CozyPixel
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
            this.MainStripMenu = new System.Windows.Forms.MenuStrip();
            this.FileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.verticalTabControl1 = new CozyPixel.Controls.VerticalTabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.ShowGridCheckBox = new System.Windows.Forms.CheckBox();
            this.GridColorButton = new System.Windows.Forms.Button();
            this.ColorLabel = new System.Windows.Forms.Label();
            this.PixelPainter = new CozyPixel.Controls.PixelPaintControl();
            this.ColorList = new CozyPixel.Controls.ColorListView();
            this.GridWidthBox = new System.Windows.Forms.TextBox();
            this.GridWidthLabel = new System.Windows.Forms.Label();
            this.MainStripMenu.SuspendLayout();
            this.verticalTabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
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
            this.MainStripMenu.Size = new System.Drawing.Size(784, 25);
            this.MainStripMenu.TabIndex = 1;
            this.MainStripMenu.Text = "menuStrip1";
            // 
            // FileMenuItem
            // 
            this.FileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenMenuItem,
            this.SaveMenuItem,
            this.ExitMenuItem});
            this.FileMenuItem.Name = "FileMenuItem";
            this.FileMenuItem.Size = new System.Drawing.Size(44, 21);
            this.FileMenuItem.Text = "文件";
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
            // verticalTabControl1
            // 
            this.verticalTabControl1.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.verticalTabControl1.Controls.Add(this.tabPage1);
            this.verticalTabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.verticalTabControl1.Location = new System.Drawing.Point(12, 28);
            this.verticalTabControl1.Multiline = true;
            this.verticalTabControl1.Name = "verticalTabControl1";
            this.verticalTabControl1.SelectedIndex = 0;
            this.verticalTabControl1.Size = new System.Drawing.Size(202, 451);
            this.verticalTabControl1.TabIndex = 6;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.GridWidthLabel);
            this.tabPage1.Controls.Add(this.GridWidthBox);
            this.tabPage1.Controls.Add(this.ShowGridCheckBox);
            this.tabPage1.Controls.Add(this.GridColorButton);
            this.tabPage1.Controls.Add(this.ColorLabel);
            this.tabPage1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tabPage1.Location = new System.Drawing.Point(22, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(176, 443);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "网格选项";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // ShowGridCheckBox
            // 
            this.ShowGridCheckBox.AutoSize = true;
            this.ShowGridCheckBox.Location = new System.Drawing.Point(57, 64);
            this.ShowGridCheckBox.Name = "ShowGridCheckBox";
            this.ShowGridCheckBox.Size = new System.Drawing.Size(72, 16);
            this.ShowGridCheckBox.TabIndex = 2;
            this.ShowGridCheckBox.Text = "显示网格";
            this.ShowGridCheckBox.UseVisualStyleBackColor = true;
            this.ShowGridCheckBox.CheckedChanged += new System.EventHandler(this.ShowGridCheckBox_CheckedChanged);
            // 
            // GridColorButton
            // 
            this.GridColorButton.FlatAppearance.BorderSize = 0;
            this.GridColorButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.GridColorButton.Location = new System.Drawing.Point(57, 147);
            this.GridColorButton.Name = "GridColorButton";
            this.GridColorButton.Size = new System.Drawing.Size(48, 48);
            this.GridColorButton.TabIndex = 1;
            this.GridColorButton.UseVisualStyleBackColor = true;
            this.GridColorButton.Click += new System.EventHandler(this.GridColorButton_Click);
            // 
            // ColorLabel
            // 
            this.ColorLabel.AutoSize = true;
            this.ColorLabel.Location = new System.Drawing.Point(55, 119);
            this.ColorLabel.Name = "ColorLabel";
            this.ColorLabel.Size = new System.Drawing.Size(53, 12);
            this.ColorLabel.TabIndex = 0;
            this.ColorLabel.Text = "网格颜色";
            // 
            // PixelPainter
            // 
            this.PixelPainter.Cursor = System.Windows.Forms.Cursors.Cross;
            this.PixelPainter.Location = new System.Drawing.Point(262, 119);
            this.PixelPainter.Name = "PixelPainter";
            this.PixelPainter.ShowGraphics = null;
            this.PixelPainter.Size = new System.Drawing.Size(480, 360);
            this.PixelPainter.SourceImage = null;
            this.PixelPainter.TabIndex = 3;
            this.PixelPainter.TabStop = false;
            this.PixelPainter.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PictureBox_MouseDown);
            // 
            // ColorList
            // 
            this.ColorList.ColorItemMargin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.ColorList.ColorItemSize = new System.Drawing.Size(48, 48);
            this.ColorList.Location = new System.Drawing.Point(262, 28);
            this.ColorList.Name = "ColorList";
            this.ColorList.SelectedColor = System.Drawing.Color.Empty;
            this.ColorList.Size = new System.Drawing.Size(480, 48);
            this.ColorList.TabIndex = 2;
            // 
            // GridWidthBox
            // 
            this.GridWidthBox.Location = new System.Drawing.Point(57, 210);
            this.GridWidthBox.Name = "GridWidthBox";
            this.GridWidthBox.Size = new System.Drawing.Size(100, 21);
            this.GridWidthBox.TabIndex = 3;
            this.GridWidthBox.TextChanged += new System.EventHandler(this.GridWidthBox_TextChanged);
            // 
            // GridWidthLabel
            // 
            this.GridWidthLabel.AutoSize = true;
            this.GridWidthLabel.Location = new System.Drawing.Point(57, 195);
            this.GridWidthLabel.Name = "GridWidthLabel";
            this.GridWidthLabel.Size = new System.Drawing.Size(53, 12);
            this.GridWidthLabel.TabIndex = 4;
            this.GridWidthLabel.Text = "网格宽度";
            // 
            // CozyPixelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 511);
            this.Controls.Add(this.verticalTabControl1);
            this.Controls.Add(this.PixelPainter);
            this.Controls.Add(this.ColorList);
            this.Controls.Add(this.MainStripMenu);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CozyPixelForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CozyPixel " + Program.Version;
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.MainStripMenu.ResumeLayout(false);
            this.MainStripMenu.PerformLayout();
            this.verticalTabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
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
        private Controls.ColorListView ColorList;
        private Controls.PixelPaintControl PixelPainter;
        private Controls.VerticalTabControl verticalTabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button GridColorButton;
        private System.Windows.Forms.Label ColorLabel;
        private System.Windows.Forms.CheckBox ShowGridCheckBox;
        private System.Windows.Forms.Label GridWidthLabel;
        private System.Windows.Forms.TextBox GridWidthBox;
    }
}
