using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozySql.Configure
{
    public class SelectPropertyNodeItem
    {
        public string ShowIcon { get; set; }

        public string DisplayName { get; set; }

        public List<SelectPropertyNodeItem> Children { get; set; }

        public SelectPropertyNodeItem()
        {
            Children = new List<SelectPropertyNodeItem>();
        }
    }
}
