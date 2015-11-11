using CozyGod.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyGod.Game.Interface
{
    public interface ICraft
    {
        bool TryCraft(Card a, Card b);

        Card Craft(Card a, Card b);
    }
}
