using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace CozyAdventure.ServerPlugin
{
    public partial class AdventurePlugin
    {
        private int _ObjectId;
        public int ObjectId
        {
            get
            {
                return Interlocked.Increment(ref _ObjectId);
            }
            set
            {
                Interlocked.Exchange(ref _ObjectId, value);
            }
        }
    }
}
