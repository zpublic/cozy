namespace CozyGod.CardEditor
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
            CozyGod.Model.CozyGodElement cozyGodElement1 = new CozyGod.Model.CozyGodElement();
            this.NameTextBox = new System.Windows.Forms.TextBox();
            this.CN_NameTextBox = new System.Windows.Forms.TextBox();
            this.LevelTextBox = new System.Windows.Forms.TextBox();
            this.PictureTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SelectPictureButton = new System.Windows.Forms.Button();
            this.GenButton = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.cozyGodEditor1 = new CozyGod.CardEditor.Controls.CozyGodEditor();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // NameTextBox
            // 
            this.NameTextBox.Location = new System.Drawing.Point(86, 25);
            this.NameTextBox.Name = "NameTextBox";
            this.NameTextBox.Size = new System.Drawing.Size(100, 21);
            this.NameTextBox.TabIndex = 1;
            this.NameTextBox.TextChanged += new System.EventHandler(this.NameTextBox_TextChanged);
            // 
            // CN_NameTextBox
            // 
            this.CN_NameTextBox.Location = new System.Drawing.Point(86, 61);
            this.CN_NameTextBox.Name = "CN_NameTextBox";
            this.CN_NameTextBox.Size = new System.Drawing.Size(100, 21);
            this.CN_NameTextBox.TabIndex = 2;
            this.CN_NameTextBox.TextChanged += new System.EventHandler(this.CN_NameTextBox_TextChanged);
            // 
            // LevelTextBox
            // 
            this.LevelTextBox.Location = new System.Drawing.Point(86, 96);
            this.LevelTextBox.Name = "LevelTextBox";
            this.LevelTextBox.Size = new System.Drawing.Size(100, 21);
            this.LevelTextBox.TabIndex = 3;
            this.LevelTextBox.TextChanged += new System.EventHandler(this.LevelTextBox_TextChanged);
            // 
            // PictureTextBox
            // 
            this.PictureTextBox.Location = new System.Drawing.Point(86, 130);
            this.PictureTextBox.Name = "PictureTextBox";
            this.PictureTextBox.ReadOnly = true;
            this.PictureTextBox.Size = new System.Drawing.Size(100, 21);
            this.PictureTextBox.TabIndex = 4;
            this.PictureTextBox.TextChanged += new System.EventHandler(this.PictureTextBox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "CN_Name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 99);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "Level";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 133);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "Picture";
            // 
            // SelectPictureButton
            // 
            this.SelectPictureButton.Location = new System.Drawing.Point(14, 180);
            this.SelectPictureButton.Name = "SelectPictureButton";
            this.SelectPictureButton.Size = new System.Drawing.Size(172, 23);
            this.SelectPictureButton.TabIndex = 9;
            this.SelectPictureButton.Text = "选择图片";
            this.SelectPictureButton.UseVisualStyleBackColor = true;
            this.SelectPictureButton.Click += new System.EventHandler(this.SelectPictureButton_Click);
            // 
            // GenButton
            // 
            this.GenButton.Location = new System.Drawing.Point(230, 180);
            this.GenButton.Name = "GenButton";
            this.GenButton.Size = new System.Drawing.Size(96, 23);
            this.GenButton.TabIndex = 10;
            this.GenButton.Text = "生成";
            this.GenButton.UseVisualStyleBackColor = true;
            this.GenButton.Click += new System.EventHandler(this.GenButton_Click);
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(230, 221);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(96, 23);
            this.SaveButton.TabIndex = 11;
            this.SaveButton.Text = "保存";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // cozyGodEditor1
            // 
            this.cozyGodEditor1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            cozyGodElement1.CN_Name = null;
            cozyGodElement1.Level = 0;
            cozyGodElement1.Name = null;
            cozyGodElement1.Picture = null;
            this.cozyGodEditor1.Element = cozyGodElement1;
            this.cozyGodEditor1.ElementBorder = null;
            this.cozyGodEditor1.Location = new System.Drawing.Point(230, 25);
            this.cozyGodEditor1.Name = "cozyGodEditor1";
            this.cozyGodEditor1.NameFont = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cozyGodEditor1.Size = new System.Drawing.Size(96, 96);
            this.cozyGodEditor1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(14, 221);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(172, 23);
            this.button1.TabIndex = 12;
            this.button1.Text = "选择边框";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(353, 257);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.GenButton);
            this.Controls.Add(this.SelectPictureButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PictureTextBox);
            this.Controls.Add(this.LevelTextBox);
            this.Controls.Add(this.CN_NameTextBox);
            this.Controls.Add(this.NameTextBox);
            this.Controls.Add(this.cozyGodEditor1);
            this.Name = "MainForm";
            this.Text = "CozyGodEditor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.CozyGodEditor cozyGodEditor1;
        private System.Windows.Forms.TextBox NameTextBox;
        private System.Windows.Forms.TextBox CN_NameTextBox;
        private System.Windows.Forms.TextBox LevelTextBox;
        private System.Windows.Forms.TextBox PictureTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button SelectPictureButton;
        private System.Windows.Forms.Button GenButton;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button button1;
    }
}

