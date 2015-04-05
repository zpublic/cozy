using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cozy.LearnCSharp.M.Details
{
    public class ResourceHolder : IDisposable
    {

        private bool isDisposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    // Cleanup managed objects by calling their 
                    // Dispose() methods.
                }
                // Cleanup unmanaged objects
            }
            isDisposed = true;
        }

        ~ResourceHolder()
        {
            Dispose(false);
        }

        public void SomeMethod()
        {
            // Ensure object not already disposed before execution of any method
            if (isDisposed)
            {
                throw new ObjectDisposedException("ResourceHolder");
            }

            // method implementation…
        }
    }
}
