namespace CozyGod.Tool.CardGenerator
{
    partial class MainForm
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
            this.SelectImagePathButton = new System.Windows.Forms.Button();
            this.SelectCraftTableButton = new System.Windows.Forms.Button();
            this.SelectTranslateTableButton = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.GenButton = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.SelectOutputPathButton = new System.Windows.Forms.Button();
            this.ImagePathBox = new System.Windows.Forms.TextBox();
            this.CraftTableBox = new System.Windows.Forms.TextBox();
            this.TranslateTableBox = new System.Windows.Forms.TextBox();
            this.OutputPathBox = new System.Windows.Forms.TextBox();
            this.SelectLevelBackgroundButton = new System.Windows.Forms.Button();
            this.SelectBorderButton = new System.Windows.Forms.Button();
            this.LevelBackgroundBox = new System.Windows.Forms.TextBox();
            this.BorderBackgroundBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // SelectImagePathButton
            // 
            this.SelectImagePathButton.Location = new System.Drawing.Point(12, 21);
            this.SelectImagePathButton.Name = "SelectImagePathButton";
            this.SelectImagePathButton.Size = new System.Drawing.Size(100, 23);
            this.SelectImagePathButton.TabIndex = 0;
            this.SelectImagePathButton.Text = "选择图片路径";
            this.SelectImagePathButton.UseVisualStyleBackColor = true;
            this.SelectImagePathButton.Click += new System.EventHandler(this.SelectImageDireButton_Click);
            // 
            // SelectCraftTableButton
            // 
            this.SelectCraftTableButton.Location = new System.Drawing.Point(12, 50);
            this.SelectCraftTableButton.Name = "SelectCraftTableButton";
            this.SelectCraftTableButton.Size = new System.Drawing.Size(100, 23);
            this.SelectCraftTableButton.TabIndex = 1;
            this.SelectCraftTableButton.Text = "选择合成表";
            this.SelectCraftTableButton.UseVisualStyleBackColor = true;
            this.SelectCraftTableButton.Click += new System.EventHandler(this.SelectCraftTablePathButton_Click);
            // 
            // SelectTranslateTableButton
            // 
            this.SelectTranslateTableButton.Location = new System.Drawing.Point(12, 79);
            this.SelectTranslateTableButton.Name = "SelectTranslateTableButton";
            this.SelectTranslateTableButton.Size = new System.Drawing.Size(100, 23);
            this.SelectTranslateTableButton.TabIndex = 2;
            this.SelectTranslateTableButton.Text = "选择翻译表";
            this.SelectTranslateTableButton.UseVisualStyleBackColor = true;
            this.SelectTranslateTableButton.Click += new System.EventHandler(this.SelectTranslateTablePathButton_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // GenButton
            // 
            this.GenButton.Location = new System.Drawing.Point(12, 196);
            this.GenButton.Name = "GenButton";
            this.GenButton.Size = new System.Drawing.Size(100, 23);
            this.GenButton.TabIndex = 3;
            this.GenButton.Text = "生成";
            this.GenButton.UseVisualStyleBackColor = true;
            this.GenButton.Click += new System.EventHandler(this.GenButton_Click);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "*.json | *.json";
            // 
            // SelectOutputPathButton
            // 
            this.SelectOutputPathButton.Location = new System.Drawing.Point(12, 108);
            this.SelectOutputPathButton.Name = "SelectOutputPathButton";
            this.SelectOutputPathButton.Size = new System.Drawing.Size(100, 23);
            this.SelectOutputPathButton.TabIndex = 4;
            this.SelectOutputPathButton.Text = "选择输出目录";
            this.SelectOutputPathButton.UseVisualStyleBackColor = true;
            this.SelectOutputPathButton.Click += new System.EventHandler(this.SelectOutputPathButton_Click);
            // 
            // ImagePathBox
            // 
            this.ImagePathBox.Location = new System.Drawing.Point(145, 21);
            this.ImagePathBox.Name = "ImagePathBox";
            this.ImagePathBox.ReadOnly = true;
            this.ImagePathBox.Size = new System.Drawing.Size(100, 21);
            this.ImagePathBox.TabIndex = 5;
            // 
            // CraftTableBox
            // 
            this.CraftTableBox.Location = new System.Drawing.Point(145, 52);
            this.CraftTableBox.Name = "CraftTableBox";
            this.CraftTableBox.ReadOnly = true;
            this.CraftTableBox.Size = new System.Drawing.Size(100, 21);
            this.CraftTableBox.TabIndex = 6;
            // 
            // TranslateTableBox
            // 
            this.TranslateTableBox.Location = new System.Drawing.Point(145, 81);
            this.TranslateTableBox.Name = "TranslateTableBox";
            this.TranslateTableBox.ReadOnly = true;
            this.TranslateTableBox.Size = new System.Drawing.Size(100, 21);
            this.TranslateTableBox.TabIndex = 7;
            // 
            // OutputPathBox
            // 
            this.OutputPathBox.Location = new System.Drawing.Point(145, 110);
            this.OutputPathBox.Name = "OutputPathBox";
            this.OutputPathBox.ReadOnly = true;
            this.OutputPathBox.Size = new System.Drawing.Size(100, 21);
            this.OutputPathBox.TabIndex = 8;
            // 
            // SelectLevelBackgroundButton
            // 
            this.SelectLevelBackgroundButton.Location = new System.Drawing.Point(12, 137);
            this.SelectLevelBackgroundButton.Name = "SelectLevelBackgroundButton";
            this.SelectLevelBackgroundButton.Size = new System.Drawing.Size(100, 23);
            this.SelectLevelBackgroundButton.TabIndex = 9;
            this.SelectLevelBackgroundButton.Text = "选择Level背景";
            this.SelectLevelBackgroundButton.UseVisualStyleBackColor = true;
            this.SelectLevelBackgroundButton.Click += new System.EventHandler(this.SelectLevelBackgroundButton_Click);
            // 
            // SelectBorderButton
            // 
            this.SelectBorderButton.Location = new System.Drawing.Point(12, 167);
            this.SelectBorderButton.Name = "SelectBorderButton";
            this.SelectBorderButton.Size = new System.Drawing.Size(100, 23);
            this.SelectBorderButton.TabIndex = 10;
            this.SelectBorderButton.Text = "设置边框";
            this.SelectBorderButton.UseVisualStyleBackColor = true;
            this.SelectBorderButton.Click += new System.EventHandler(this.SelectBorderButton_Click);
            // 
            // LevelBackgroundBox
            // 
            this.LevelBackgroundBox.Location = new System.Drawing.Point(145, 138);
            this.LevelBackgroundBox.Name = "LevelBackgroundBox";
            this.LevelBackgroundBox.ReadOnly = true;
            this.LevelBackgroundBox.Size = new System.Drawing.Size(100, 21);
            this.LevelBackgroundBox.TabIndex = 11;
            // 
            // BorderBackgroundBox
            // 
            this.BorderBackgroundBox.Location = new System.Drawing.Point(145, 166);
            this.BorderBackgroundBox.Name = "BorderBackgroundBox";
            this.BorderBackgroundBox.ReadOnly = true;
            this.BorderBackgroundBox.Size = new System.Drawing.Size(100, 21);
            this.BorderBackgroundBox.TabIndex = 12;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(314, 261);
            this.Controls.Add(this.BorderBackgroundBox);
            this.Controls.Add(this.LevelBackgroundBox);
            this.Controls.Add(this.SelectBorderButton);
            this.Controls.Add(this.SelectLevelBackgroundButton);
            this.Controls.Add(this.OutputPathBox);
            this.Controls.Add(this.TranslateTableBox);
            this.Controls.Add(this.CraftTableBox);
            this.Controls.Add(this.ImagePathBox);
            this.Controls.Add(this.SelectOutputPathButton);
            this.Controls.Add(this.GenButton);
            this.Controls.Add(this.SelectTranslateTableButton);
            this.Controls.Add(this.SelectCraftTableButton);
            this.Controls.Add(this.SelectImagePathButton);
            this.Name = "MainForm";
            this.Text = "CardGenerator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button SelectImagePathButton;
        private System.Windows.Forms.Button SelectCraftTableButton;
        private System.Windows.Forms.Button SelectTranslateTableButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button GenButton;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button SelectOutputPathButton;
        private System.Windows.Forms.TextBox ImagePathBox;
        private System.Windows.Forms.TextBox CraftTableBox;
        private System.Windows.Forms.TextBox TranslateTableBox;
        private System.Windows.Forms.TextBox OutputPathBox;
        private System.Windows.Forms.Button SelectLevelBackgroundButton;
        private System.Windows.Forms.Button SelectBorderButton;
        private System.Windows.Forms.TextBox LevelBackgroundBox;
        private System.Windows.Forms.TextBox BorderBackgroundBox;
    }
}

