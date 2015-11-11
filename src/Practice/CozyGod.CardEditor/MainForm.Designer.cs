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
            this.cozyGodEditor1 = new CozyGod.CardEditor.Controls.CozyGodEditor();
            this.SuspendLayout();
            // 
            // NameTextBox
            // 
            this.NameTextBox.Location = new System.Drawing.Point(30, 42);
            this.NameTextBox.Name = "NameTextBox";
            this.NameTextBox.Size = new System.Drawing.Size(100, 21);
            this.NameTextBox.TabIndex = 1;
            this.NameTextBox.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
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
            this.cozyGodEditor1.Location = new System.Drawing.Point(163, 25);
            this.cozyGodEditor1.Name = "cozyGodEditor1";
            this.cozyGodEditor1.NameFont = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cozyGodEditor1.Size = new System.Drawing.Size(96, 96);
            this.cozyGodEditor1.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.NameTextBox);
            this.Controls.Add(this.cozyGodEditor1);
            this.Name = "MainForm";
            this.Text = "CozyGodEditor";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.CozyGodEditor cozyGodEditor1;
        private System.Windows.Forms.TextBox NameTextBox;
    }
}

