using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyNote.Model.ObjectModel
{
    public class Notebook
    {
        public int id { get; set; }
        public string pass { get; set; }
        public string name { get; set; }
        public List<int> note_list { get; set; }
        public List<int> user_list { get; set; }

        public Notebook()
        {
            note_list = new List<int>();
            user_list = new List<int>();
        }
    }
}
