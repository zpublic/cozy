using CozyDungeon.Game.Component.Card.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyDungeon.RoleCardEditor.EventArg
{
    public class CardCreateEventArgs : EventArgs
    {
        public RoleCard Card { get; set; }

        public CozyCardImage CardImage { get; set; }

        public CardCreateEventArgs(RoleCard card, CozyCardImage cardimage)
        {
            Card = card;
            CardImage = cardimage;
        }
    }
}
