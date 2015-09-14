namespace CozyAdventure.GameNetworkTester
{
    partial class Form1
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
            this.Register = new System.Windows.Forms.Button();
            this.user = new System.Windows.Forms.TextBox();
            this.nickname = new System.Windows.Forms.TextBox();
            this.Login = new System.Windows.Forms.Button();
            this.pass = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Register
            // 
            this.Register.Location = new System.Drawing.Point(26, 120);
            this.Register.Name = "Register";
            this.Register.Size = new System.Drawing.Size(100, 23);
            this.Register.TabIndex = 0;
            this.Register.Text = "注册";
            this.Register.UseVisualStyleBackColor = true;
            this.Register.Click += new System.EventHandler(this.Register_Click);
            // 
            // user
            // 
            this.user.Location = new System.Drawing.Point(26, 29);
            this.user.Name = "user";
            this.user.Size = new System.Drawing.Size(214, 21);
            this.user.TabIndex = 1;
            // 
            // nickname
            // 
            this.nickname.Location = new System.Drawing.Point(26, 56);
            this.nickname.Name = "nickname";
            this.nickname.Size = new System.Drawing.Size(214, 21);
            this.nickname.TabIndex = 2;
            // 
            // Login
            // 
            this.Login.Location = new System.Drawing.Point(140, 120);
            this.Login.Name = "Login";
            this.Login.Size = new System.Drawing.Size(100, 23);
            this.Login.TabIndex = 3;
            this.Login.Text = "登陆";
            this.Login.UseVisualStyleBackColor = true;
            this.Login.Click += new System.EventHandler(this.Login_Click);
            // 
            // pass
            // 
            this.pass.Location = new System.Drawing.Point(26, 83);
            this.pass.Name = "pass";
            this.pass.Size = new System.Drawing.Size(214, 21);
            this.pass.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.pass);
            this.Controls.Add(this.Login);
            this.Controls.Add(this.nickname);
            this.Controls.Add(this.user);
            this.Controls.Add(this.Register);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Register;
        private System.Windows.Forms.TextBox user;
        private System.Windows.Forms.TextBox nickname;
        private System.Windows.Forms.Button Login;
        private System.Windows.Forms.TextBox pass;
    }
}

