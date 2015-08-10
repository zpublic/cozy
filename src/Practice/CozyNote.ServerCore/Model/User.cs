using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyNote.ServerCore.Model
{
    public class User
    {
        public int id { get; set; }
        public string nickname { get; set; }
        public int[] notebook_list { get; set; }
    }
}
