namespace CozyPixel.Forms
{
    partial class AboutForm
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
            this.AboutLabel = new MetroFramework.Controls.MetroLabel();
            this.labelAuthor = new MetroFramework.Controls.MetroLabel();
            this.SuspendLayout();
            // 
            // OkButton
            // 
            this.OkButton.Location = new System.Drawing.Point(66, 211);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(87, 23);
            this.OkButton.TabIndex = 0;
            this.OkButton.Text = "Ok";
            this.OkButton.UseSelectable = true;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // AboutLabel
            // 
            this.AboutLabel.AutoSize = true;
            this.AboutLabel.Location = new System.Drawing.Point(66, 60);
            this.AboutLabel.Name = "AboutLabel";
            this.AboutLabel.Size = new System.Drawing.Size(65, 19);
            this.AboutLabel.TabIndex = 1;
            this.AboutLabel.Text = "CozyPixel";
            // 
            // labelAuthor
            // 
            this.labelAuthor.AutoSize = true;
            this.labelAuthor.Location = new System.Drawing.Point(66, 89);
            this.labelAuthor.Name = "labelAuthor";
            this.labelAuthor.Size = new System.Drawing.Size(77, 19);
            this.labelAuthor.TabIndex = 2;
            this.labelAuthor.Text = "labelAuthor";
            // 
            // AboutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(225, 257);
            this.Controls.Add(this.labelAuthor);
            this.Controls.Add(this.AboutLabel);
            this.Controls.Add(this.OkButton);
            this.Name = "AboutForm";
            this.Text = "关于我们";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroButton OkButton;
        private MetroFramework.Controls.MetroLabel AboutLabel;
        private MetroFramework.Controls.MetroLabel labelAuthor;
    }
}