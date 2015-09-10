namespace CozyQrcode
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.SaveButton = new System.Windows.Forms.Button();
            this.qrCodeImgControl1 = new Gma.QrCodeNet.Encoding.Windows.Forms.QrCodeImgControl();
            this.Cinbox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.qrCodeImgControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(13, 54);
            this.SaveButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(200, 27);
            this.SaveButton.TabIndex = 0;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // qrCodeImgControl1
            // 
            this.qrCodeImgControl1.ErrorCorrectLevel = Gma.QrCodeNet.Encoding.ErrorCorrectionLevel.M;
            this.qrCodeImgControl1.Location = new System.Drawing.Point(13, 98);
            this.qrCodeImgControl1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.qrCodeImgControl1.Name = "qrCodeImgControl1";
            this.qrCodeImgControl1.QuietZoneModule = Gma.QrCodeNet.Encoding.Windows.Render.QuietZoneModules.Two;
            this.qrCodeImgControl1.Size = new System.Drawing.Size(200, 200);
            this.qrCodeImgControl1.TabIndex = 2;
            this.qrCodeImgControl1.TabStop = false;
            this.qrCodeImgControl1.Text = "qrCodeImgControl1";
            // 
            // Cinbox
            // 
            this.Cinbox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.Cinbox.Location = new System.Drawing.Point(13, 14);
            this.Cinbox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Cinbox.Name = "Cinbox";
            this.Cinbox.Size = new System.Drawing.Size(200, 23);
            this.Cinbox.TabIndex = 3;
            this.Cinbox.TextChanged += new System.EventHandler(this.Cinbox_TextChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(233, 313);
            this.Controls.Add(this.Cinbox);
            this.Controls.Add(this.qrCodeImgControl1);
            this.Controls.Add(this.SaveButton);
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "MainForm";
            this.Text = "CozyQRCode";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.qrCodeImgControl1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button SaveButton;
        private Gma.QrCodeNet.Encoding.Windows.Forms.QrCodeImgControl qrCodeImgControl1;
        private System.Windows.Forms.TextBox Cinbox;
    }
}

