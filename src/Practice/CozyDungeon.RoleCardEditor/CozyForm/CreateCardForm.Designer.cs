namespace CozyDungeon.RoleCardEditor.CozyForm
{
    partial class CreateCardForm
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
            CozyDungeon.Game.Component.Card.Model.RoleCard roleCard1 = new CozyDungeon.Game.Component.Card.Model.RoleCard();
            this.cardInfoControl1 = new CozyDungeon.RoleCardEditor.CardInfoControl();
            this.SuspendLayout();
            // 
            // cardInfoControl1
            // 
            this.cardInfoControl1.CardLevels = null;
            this.cardInfoControl1.Id = 0;
            this.cardInfoControl1.Location = new System.Drawing.Point(41, 63);
            this.cardInfoControl1.Name = "cardInfoControl1";
            roleCard1.ATK = 1;
            roleCard1.DEF = 0;
            roleCard1.Desc = null;
            roleCard1.Element = CozyDungeon.Game.Component.Card.Enum.FiveLine.Gold;
            roleCard1.HP = 1;
            roleCard1.Id = 0;
            roleCard1.Level = CozyDungeon.Game.Component.Card.Enum.RoleCardLevel.LevelInvalid;
            roleCard1.Name = "无效名字";
            this.cardInfoControl1.RoleCard = roleCard1;
            this.cardInfoControl1.Size = new System.Drawing.Size(540, 447);
            this.cardInfoControl1.TabIndex = 0;
            // 
            // CreateCardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 528);
            this.Controls.Add(this.cardInfoControl1);
            this.Name = "CreateCardForm";
            this.Text = "CreateCardForm";
            this.ResumeLayout(false);

        }

        #endregion

        private CardInfoControl cardInfoControl1;
    }
}