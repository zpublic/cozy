using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyLauncher.Tool.Update.Helper
{
    internal class InfrastructureLoader : IDisposable
    {
        private InfrastructureLoaderHelper helper { get; set; }
        public AppDomain CurrAppDomain { get; set; }

        public static InfrastructureLoader Create(string Dllname)
        {
            var ad          = AppDomain.CreateDomain("__CozyAppDomain_" + Dllname);
            var loadhelper  = InfrastructureLoaderHelper.Create(ad, Dllname);

            return new InfrastructureLoader()
            {
                CurrAppDomain   = ad,
                helper          = loadhelper,
            };
        }

        public void LoadType(string typename)
        {
            helper?.LoadType(typename);
        }

        public void LoadObject()
        {
            helper?.LoadObject();
        }

        public object Invoke(string method, params object[] args)
        {
            if (helper != null)
            {
                return helper?.Invoke(method, args);
            }
            return null;
        }

        public object InvokeStaticMethod(string methodname, params object[] args)
        {
            if (helper != null)
            {
                return helper?.InvokeStaticMethod(methodname, args);
            }
            return null;
        }

        public void Dispose()
        {
            if (helper != null)
            {
                helper.Unload();
                AppDomain.Unload(CurrAppDomain);
            }
        }
    }
}
