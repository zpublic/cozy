using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CozyLauncher.Infrastructure.ProcessMutex
{
    public class ProcessMutexMgr
    {
        public static ProcessMutexMgr Instance { get; set; } = new ProcessMutexMgr();

        private Mutex Mutex;

        public bool CheckExist(string name)
        {
            bool retVal = Mutex.TryOpenExisting(name, out Mutex);

            if(!retVal)
            {
                Mutex = new Mutex(true, name);
            }
            return retVal;
        }

        public void Destory()
        {
            Mutex?.ReleaseMutex();
            Mutex = null;
        }
    }
}
