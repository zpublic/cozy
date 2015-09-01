using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetroFramework.Forms;
using System.Windows.Forms;
using CozyDungeon.Game.Component.Card.Enum;
using CozyDungeon.Game.Component.Card.Model;
using CozyDungeon.RoleCardEditor.EventArg;

namespace CozyDungeon.RoleCardEditor.CozyForm
{
    public partial class CreateCardForm : MetroForm
    {
        public EventHandler<CardCreateEventArgs> CardCreateEventHandler;

        public CreateCardForm(List<RoleCardLevel> cardLevels)
        {
            InitializeComponent();

            cardInfoControl1.CardLevels = cardLevels;
        }

        private void CompleteButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            if (CardCreateEventHandler != null)
            {
                CardCreateEventHandler(this, new CardCreateEventArgs(cardInfoControl1.RoleCard,cardInfoControl1.Images));
            }
            this.Close();
        }
    }
}
