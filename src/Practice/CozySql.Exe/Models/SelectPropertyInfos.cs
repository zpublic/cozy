using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozySql.Exe.Models
{
    public class SelectPropertyInfo
    {
        public string Name { get; set; }

        public List<SelectPropertyInfo> Children { get; set; }

        public SelectPropertyInfo()
        {
            Children = new List<SelectPropertyInfo>();
        }
    }
}
