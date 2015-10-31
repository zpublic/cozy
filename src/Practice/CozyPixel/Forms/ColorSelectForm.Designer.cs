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
            this.OkButton = new MetroFramework.Controls.MetroButton();
            this.CancleButton = new MetroFramework.Controls.MetroButton();
            this.ColorSelectList = new CozyPixel.Controls.ColorListView();
            this.SuspendLayout();
            // 
            // OkButton
            // 
            this.OkButton.Location = new System.Drawing.Point(75, 175);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(75, 23);
            this.OkButton.TabIndex = 1;
            this.OkButton.Text = "确定";
            this.OkButton.UseSelectable = true;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // CancleButton
            // 
            this.CancleButton.Location = new System.Drawing.Point(275, 175);
            this.CancleButton.Name = "CancleButton";
            this.CancleButton.Size = new System.Drawing.Size(75, 23);
            this.CancleButton.TabIndex = 2;
            this.CancleButton.Text = "取消";
            this.CancleButton.UseSelectable = true;
            this.CancleButton.Click += new System.EventHandler(this.CancleButton_Click);
            // 
            // ColorSelectList
            // 
            this.ColorSelectList.ColorItemMargin = new System.Windows.Forms.Padding(2);
            this.ColorSelectList.ColorItemSize = new System.Drawing.Size(46, 20);
            this.ColorSelectList.Location = new System.Drawing.Point(25, 78);
            this.ColorSelectList.Name = "ColorSelectList";
            this.ColorSelectList.SelectedColor = System.Drawing.Color.Empty;
            this.ColorSelectList.Size = new System.Drawing.Size(400, 72);
            this.ColorSelectList.TabIndex = 0;
            // 
            // ColorSelectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(448, 221);
            this.Controls.Add(this.CancleButton);
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.ColorSelectList);
            this.Name = "ColorSelectForm";
            this.Text = "选择颜色";
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.ColorListView ColorSelectList;
        private MetroFramework.Controls.MetroButton OkButton;
        private MetroFramework.Controls.MetroButton CancleButton;
    }
}