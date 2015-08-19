using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyDungeon.Game.Component.Card.Model
{
    public class BaseCard : ICloneable
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; } = "无效名字";
        public string Desc { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
