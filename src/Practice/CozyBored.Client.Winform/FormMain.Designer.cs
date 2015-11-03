namespace CozyBored.Client.Winform
{
    partial class FormMain
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
            this.components = new System.ComponentModel.Container();
            this.TimeLabel = new System.Windows.Forms.Label();
            this.StartButton = new System.Windows.Forms.Button();
            this.ViewBoradButton = new System.Windows.Forms.Button();
            this.MainTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // TimeLabel
            // 
            this.TimeLabel.AutoSize = true;
            this.TimeLabel.Location = new System.Drawing.Point(108, 29);
            this.TimeLabel.Name = "TimeLabel";
            this.TimeLabel.Size = new System.Drawing.Size(71, 12);
            this.TimeLabel.TabIndex = 0;
            this.TimeLabel.Text = "00:00:00:00";
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(40, 71);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(208, 135);
            this.StartButton.TabIndex = 1;
            this.StartButton.Text = "鼠标按住这里开始计时";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.StartButton_MouseDown);
            this.StartButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.StartButton_MouseUp);
            // 
            // ViewBoradButton
            // 
            this.ViewBoradButton.Location = new System.Drawing.Point(173, 227);
            this.ViewBoradButton.Name = "ViewBoradButton";
            this.ViewBoradButton.Size = new System.Drawing.Size(75, 23);
            this.ViewBoradButton.TabIndex = 2;
            this.ViewBoradButton.Text = "查看排行榜";
            this.ViewBoradButton.UseVisualStyleBackColor = true;
            this.ViewBoradButton.Click += new System.EventHandler(this.ViewBoradButton_Click);
            // 
            // MainTimer
            // 
            this.MainTimer.Interval = 15;
            this.MainTimer.Tick += new System.EventHandler(this.MainTimer_Tick);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.ViewBoradButton);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.TimeLabel);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CozyBored";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label TimeLabel;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.Button ViewBoradButton;
        private System.Windows.Forms.Timer MainTimer;
    }
}

