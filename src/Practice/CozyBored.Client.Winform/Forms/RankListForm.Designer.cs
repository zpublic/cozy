namespace CozyBored.Client.Winform.Forms
{
    partial class RankListForm
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
            this.RankListView = new System.Windows.Forms.ListView();
            this.RankHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.NameHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TimeHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.OKButton = new System.Windows.Forms.Button();
            this.SupportButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // RankListView
            // 
            this.RankListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.RankHeader,
            this.NameHeader,
            this.TimeHeader});
            this.RankListView.Location = new System.Drawing.Point(12, 12);
            this.RankListView.Name = "RankListView";
            this.RankListView.Size = new System.Drawing.Size(260, 208);
            this.RankListView.TabIndex = 0;
            this.RankListView.UseCompatibleStateImageBehavior = false;
            this.RankListView.View = System.Windows.Forms.View.Details;
            // 
            // RankHeader
            // 
            this.RankHeader.Text = "排名";
            this.RankHeader.Width = 78;
            // 
            // NameHeader
            // 
            this.NameHeader.Text = "名称";
            this.NameHeader.Width = 89;
            // 
            // TimeHeader
            // 
            this.TimeHeader.Text = "时间";
            this.TimeHeader.Width = 139;
            // 
            // OKButton
            // 
            this.OKButton.Location = new System.Drawing.Point(49, 226);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 23);
            this.OKButton.TabIndex = 1;
            this.OKButton.Text = "确定";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // SupportButton
            // 
            this.SupportButton.Location = new System.Drawing.Point(159, 227);
            this.SupportButton.Name = "SupportButton";
            this.SupportButton.Size = new System.Drawing.Size(75, 23);
            this.SupportButton.TabIndex = 2;
            this.SupportButton.Text = "我要上榜";
            this.SupportButton.UseVisualStyleBackColor = true;
            this.SupportButton.Click += new System.EventHandler(this.SupportButton_Click);
            // 
            // RankListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.SupportButton);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.RankListView);
            this.Name = "RankListForm";
            this.Text = "无聊排行榜";
            this.Load += new System.EventHandler(this.RankListForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView RankListView;
        private System.Windows.Forms.ColumnHeader RankHeader;
        private System.Windows.Forms.ColumnHeader NameHeader;
        private System.Windows.Forms.ColumnHeader TimeHeader;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Button SupportButton;
    }
}