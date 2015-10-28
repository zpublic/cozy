namespace CozyPixel.Forms
{
    partial class ColorSelectForm
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
            this.ColorSelectList = new CozyPixel.Controls.ColorListView();
            this.OkButton = new System.Windows.Forms.Button();
            this.CancleButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ColorSelectList
            // 
            this.ColorSelectList.ColorItemMargin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.ColorSelectList.ColorItemSize = new System.Drawing.Size(46, 20);
            this.ColorSelectList.Location = new System.Drawing.Point(26, 26);
            this.ColorSelectList.Name = "ColorSelectList";
            this.ColorSelectList.SelectedColor = System.Drawing.Color.Empty;
            this.ColorSelectList.Size = new System.Drawing.Size(400, 72);
            this.ColorSelectList.TabIndex = 0;
            // 
            // OkButton
            // 
            this.OkButton.Location = new System.Drawing.Point(84, 122);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(75, 23);
            this.OkButton.TabIndex = 1;
            this.OkButton.Text = "确定";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // CancleButton
            // 
            this.CancleButton.Location = new System.Drawing.Point(261, 122);
            this.CancleButton.Name = "CancleButton";
            this.CancleButton.Size = new System.Drawing.Size(75, 23);
            this.CancleButton.TabIndex = 2;
            this.CancleButton.Text = "取消";
            this.CancleButton.UseVisualStyleBackColor = true;
            this.CancleButton.Click += new System.EventHandler(this.CancleButton_Click);
            // 
            // ColorSelectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(448, 166);
            this.Controls.Add(this.CancleButton);
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.ColorSelectList);
            this.Name = "ColorSelectForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "选择颜色";
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.ColorListView ColorSelectList;
        private System.Windows.Forms.Button OkButton;
        private System.Windows.Forms.Button CancleButton;
    }
}