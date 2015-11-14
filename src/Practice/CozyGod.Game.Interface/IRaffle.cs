using CozyGod.Game.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyGod.Game.Interface
{
    public interface IRaffle
    {
        Card Draw(int rank = 0);
        Card[] PentaDraw();
    }
}
