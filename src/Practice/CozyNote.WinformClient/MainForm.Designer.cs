namespace CozyNote.WinformClient {
    partial class MainForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.metroPanelLeft = new MetroFramework.Controls.MetroPanel();
            this.metroPanelMain = new MetroFramework.Controls.MetroPanel();
            this.metroTabControl = new MetroFramework.Controls.MetroTabControl();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.metroPanelLeft.SuspendLayout();
            this.metroPanelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // metroPanelLeft
            // 
            this.metroPanelLeft.Controls.Add(this.flowLayoutPanel1);
            this.metroPanelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.metroPanelLeft.HorizontalScrollbarBarColor = true;
            this.metroPanelLeft.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanelLeft.HorizontalScrollbarSize = 10;
            this.metroPanelLeft.Location = new System.Drawing.Point(20, 60);
            this.metroPanelLeft.Name = "metroPanelLeft";
            this.metroPanelLeft.Size = new System.Drawing.Size(213, 675);
            this.metroPanelLeft.TabIndex = 0;
            this.metroPanelLeft.VerticalScrollbarBarColor = true;
            this.metroPanelLeft.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanelLeft.VerticalScrollbarSize = 10;
            // 
            // metroPanelMain
            // 
            this.metroPanelMain.Controls.Add(this.metroTabControl);
            this.metroPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroPanelMain.HorizontalScrollbarBarColor = true;
            this.metroPanelMain.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanelMain.HorizontalScrollbarSize = 10;
            this.metroPanelMain.Location = new System.Drawing.Point(233, 60);
            this.metroPanelMain.Name = "metroPanelMain";
            this.metroPanelMain.Size = new System.Drawing.Size(660, 675);
            this.metroPanelMain.TabIndex = 1;
            this.metroPanelMain.VerticalScrollbarBarColor = true;
            this.metroPanelMain.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanelMain.VerticalScrollbarSize = 10;
            // 
            // metroTabControl
            // 
            this.metroTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroTabControl.Location = new System.Drawing.Point(0, 0);
            this.metroTabControl.Name = "metroTabControl";
            this.metroTabControl.Size = new System.Drawing.Size(660, 675);
            this.metroTabControl.TabIndex = 2;
            this.metroTabControl.UseSelectable = true;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(213, 675);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(913, 755);
            this.Controls.Add(this.metroPanelMain);
            this.Controls.Add(this.metroPanelLeft);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.metroPanelLeft.ResumeLayout(false);
            this.metroPanelMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroPanel metroPanelLeft;
        private MetroFramework.Controls.MetroPanel metroPanelMain;
        private MetroFramework.Controls.MetroTabControl metroTabControl;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}