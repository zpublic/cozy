using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyNote.Model.ObjectModel
{
    public class User
    {
        public int id { get; set; }
        public string nickname { get; set; }
        public string pass { get; set; }
        public List<int> notebook_list { get; set; }
    }
}
