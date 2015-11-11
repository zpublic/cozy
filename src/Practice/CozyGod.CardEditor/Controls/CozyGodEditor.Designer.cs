namespace CozyGod.CardEditor.Controls
{
    partial class CozyGodEditor
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
            this.InnerPictruceBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.InnerPictruceBox)).BeginInit();
            this.SuspendLayout();
            // 
            // InnerPictruceBox
            // 
            this.InnerPictruceBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.InnerPictruceBox.Location = new System.Drawing.Point(0, 0);
            this.InnerPictruceBox.Name = "InnerPictruceBox";
            this.InnerPictruceBox.Size = new System.Drawing.Size(96, 96);
            this.InnerPictruceBox.TabIndex = 2;
            this.InnerPictruceBox.TabStop = false;
            // 
            // CozyGodEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.InnerPictruceBox);
            this.Name = "CozyGodEditor";
            this.Size = new System.Drawing.Size(96, 96);
            ((System.ComponentModel.ISupportInitialize)(this.InnerPictruceBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox InnerPictruceBox;
    }
}
