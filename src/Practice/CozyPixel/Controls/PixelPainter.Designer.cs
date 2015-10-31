namespace CozyPixel.Controls
{
    partial class PixelPainter
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.InnerPicBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.InnerPicBox)).BeginInit();
            this.SuspendLayout();
            // 
            // InnerPicBox
            // 
            this.InnerPicBox.Location = new System.Drawing.Point(0, 0);
            this.InnerPicBox.Name = "InnerPicBox";
            this.InnerPicBox.Size = new System.Drawing.Size(100, 50);
            this.InnerPicBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.InnerPicBox.TabIndex = 0;
            this.InnerPicBox.TabStop = false;
            this.InnerPicBox.SizeChanged += new System.EventHandler(this.InnerPicBox_SizeChanged);
            this.InnerPicBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.InnerPicBox_MouseDown);
            this.InnerPicBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.InnerPicBox_MouseMove);
            this.InnerPicBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.InnerPicBox_MouseUp);
            // 
            // PixelPainter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.InnerPicBox);
            this.Name = "PixelPainter";
            ((System.ComponentModel.ISupportInitialize)(this.InnerPicBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox InnerPicBox;
    }
}
