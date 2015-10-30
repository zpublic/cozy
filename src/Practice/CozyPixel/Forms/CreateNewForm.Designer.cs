namespace CozyPixel.Forms
{
    partial class CreateNewForm
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
            this.WidthLabel = new MetroFramework.Controls.MetroLabel();
            this.HeightLabel = new MetroFramework.Controls.MetroLabel();
            this.WidthBox = new System.Windows.Forms.TextBox();
            this.HeightBox = new System.Windows.Forms.TextBox();
            this.OKButton = new MetroFramework.Controls.MetroButton();
            this.SuspendLayout();
            // 
            // WidthLabel
            // 
            this.WidthLabel.AutoSize = true;
            this.WidthLabel.Location = new System.Drawing.Point(76, 91);
            this.WidthLabel.Name = "WidthLabel";
            this.WidthLabel.Size = new System.Drawing.Size(51, 19);
            this.WidthLabel.TabIndex = 0;
            this.WidthLabel.Text = "宽度：";
            // 
            // HeightLabel
            // 
            this.HeightLabel.AutoSize = true;
            this.HeightLabel.Location = new System.Drawing.Point(76, 146);
            this.HeightLabel.Name = "HeightLabel";
            this.HeightLabel.Size = new System.Drawing.Size(51, 19);
            this.HeightLabel.TabIndex = 1;
            this.HeightLabel.Text = "高度：";
            // 
            // WidthBox
            // 
            this.WidthBox.Location = new System.Drawing.Point(147, 91);
            this.WidthBox.Name = "WidthBox";
            this.WidthBox.Size = new System.Drawing.Size(100, 21);
            this.WidthBox.TabIndex = 2;
            this.WidthBox.Text = "32";
            // 
            // HeightBox
            // 
            this.HeightBox.Location = new System.Drawing.Point(147, 146);
            this.HeightBox.Name = "HeightBox";
            this.HeightBox.Size = new System.Drawing.Size(100, 21);
            this.HeightBox.TabIndex = 3;
            this.HeightBox.Text = "32";
            // 
            // OKButton
            // 
            this.OKButton.Location = new System.Drawing.Point(108, 202);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 23);
            this.OKButton.TabIndex = 4;
            this.OKButton.Text = "确定";
            this.OKButton.UseSelectable = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // CreateNewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(328, 263);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.HeightBox);
            this.Controls.Add(this.WidthBox);
            this.Controls.Add(this.HeightLabel);
            this.Controls.Add(this.WidthLabel);
            this.Name = "CreateNewForm";
            this.Text = "创建新像素画";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroLabel WidthLabel;
        private MetroFramework.Controls.MetroLabel HeightLabel;
        private System.Windows.Forms.TextBox WidthBox;
        private System.Windows.Forms.TextBox HeightBox;
        private MetroFramework.Controls.MetroButton OKButton;
    }
}