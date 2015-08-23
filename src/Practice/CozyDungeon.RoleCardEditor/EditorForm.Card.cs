using CozyDungeon.Game.Component.Card.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CozyDungeon.RoleCardEditor
{
    public partial class EditorForm
    {
        private int InnerID = 0;
        public int IDMaker
        {
            get
            {
                return InnerID++;
            }
        }

        private void ClearId()
        {
            InnerID = 0;
        }
        private int CardIdCache { get; set; }

        private void CreateCard()
        {
            var form = new CozyForm.CreateCardForm(CardLevels, CardIdCache);
            form.CardCreateEventHandler += (s, msg) =>
            {
                AddCard(msg.Card, msg.CardImage);
                CardTabControl.SelectedIndex = (int)msg.Card.Level;
            };
            if (form.ShowDialog() == DialogResult.OK)
            {
                CardIdCache = IDMaker;
            }
        }

        private void AddCard(RoleCard card, CozyCardImage cardImage)
        {
            IsModified = true;

            CardImageDictionary[card.Id] = cardImage;
            ListOfRoleCardList[(int)card.Level].Add(card);
        }
    }
}
