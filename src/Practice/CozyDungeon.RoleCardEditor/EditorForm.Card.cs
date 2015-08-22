using CozyDungeon.Game.Component.Card.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyDungeon.RoleCardEditor
{
    public partial class EditorForm
    {
        private int InnerID = 0;
        public string IDMaker
        {
            get
            {
                InnerID++;
                return InnerID.ToString();
            }
        }

        private void ClearId()
        {
            InnerID = 0;
        }

        private void ResetId()
        {
            IDBox.Text = IDMaker;
        }

        private bool IsModified { get; set; }

        private void AddCard()
        {
            var card = new RoleCard()
            {
                Level   = CardLevels[(int)LevelBox.SelectedValue],
                Id      = int.Parse(IDBox.Text),
                Name    = NameBox.Text,
                Desc    = DescBox.Text,
                ATK     = int.Parse(ATKBox.Text),
                DEF     = int.Parse(DEFBox.Text),
                HP      = int.Parse(HPBox.Text),
            };
            IsModified = true;

            CardImageDictionary[card.Id] = cardPictureBox.Image;
            ListOfRoleCardList[(int)LevelBox.SelectedValue].Add(card);

            ResetInput();
            ResetId();
        }
    }
}
