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

namespace CozyDungeon.RoleCardEditor.CozyForm
{
    public partial class CreateCardForm : MetroForm
    {
        public class CardCreateEventArgs : EventArgs
        {
            public RoleCard Card { get; set; }

            public CozyCardImage CardImage { get; set; }

            public CardCreateEventArgs(RoleCard card, CozyCardImage cardimage)
            {
                Card        = card;
                CardImage   = cardimage;
            }
        }

        public EventHandler<CardCreateEventArgs> CardCreateEventHandler;

        public CreateCardForm(List<RoleCardLevel> cardLevels, int id)
        {
            InitializeComponent();

            cardInfoControl1.CardLevels = cardLevels;
            cardInfoControl1.Id         = id;
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
