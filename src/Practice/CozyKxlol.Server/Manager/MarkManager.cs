using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CozyKxlol.Server.Manager
{
    public class MarkManager
    {
        private Dictionary<uint, int> MarkList = new Dictionary<uint, int>();

        public int MaxSize { get; set; }

        public class MarkChangedArgs : EventArgs
        {
            public List<KeyValuePair<uint, int>> TopMarkList = new List<KeyValuePair<uint, int>>();
            public MarkChangedArgs(List<KeyValuePair<uint, int>> list)
            {
                TopMarkList = list;
            }
        }
        public event EventHandler<MarkChangedArgs> MarkChangedMessage;

        public void Update(uint id, int mark)
        {
            MarkList[id] = mark;
            MarkChangedMessage(this, new MarkChangedArgs(MarkList.ToList()));
        }

        public void Remove(uint id)
        {
            if(MarkList.ContainsKey(id))
            {
                MarkList.Remove(id);
            }
            MarkChangedMessage(this, new MarkChangedArgs(MarkList.ToList()));
        }
    }
}
