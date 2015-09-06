using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyPoker.Engine.Model
{
    public class CardCollect
    {
        Random random_ = new Random();

        private List<Card> cards_ = new List<Card>();
        public ICollection<Card> Cards
        {
            get { return cards_; }
            set { Cards = value; }
        }

        public void Shuffle()
        {
            List<Card> newList = new List<Card>();
            for (int i = 0; i < cards_.Count;)
            {
                int index = random_.Next(0, Cards.Count);
                newList.Add(cards_[index]);
                cards_.RemoveAt(index);
            }
            cards_ = newList;
        }

        public void Clear()
        {
            cards_.Clear();
        }

        public void Add(Card c)
        {
            cards_.Add(c);
        }

        public void Add(IEnumerable<Card> cs)
        {
            cards_.AddRange(cs);
        }

        public Card Get(int index = -1)
        {
            Card c = null;
            if (index == -1)
            {
                c = cards_[0];
                cards_.RemoveAt(0);
            }
            else
            {
                c = cards_[index];
                cards_.RemoveAt(index);
            }
            return c;
        }

        public List<Card> Get(SortedSet<int> indexs)
        {
            List<Card> cs = new List<Card>();
            foreach (var i in indexs.Reverse())
            {
                cs.Add(cards_[i]);
                cards_.RemoveAt(i);
            }
            return cs;
        }
    }
}
