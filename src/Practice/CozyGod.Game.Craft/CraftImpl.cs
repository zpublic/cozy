using CozyGod.Game.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyGod.Model;

namespace CozyGod.Game.Craft
{
    public class CraftImpl : ICraft
    {
        private ICardLibrary mCL;

        public void SetCardLibrary(ICardLibrary cl)
        {
            mCL = cl;
        }

        public Card Craft(Card a, Card b)
        {
            throw new NotImplementedException();
        }

        public bool TryCraft(Card a, Card b)
        {
            throw new NotImplementedException();
        }
    }
}
