using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CozyLauncher.Tool.Update.Helper
{
    internal class InfrastructureLoaderHelper : MarshalByRefObject
    {
        private Assembly LoadedAsm { get; set; }
        private object LoadedObject { get; set; }
        private Type LoadedObjectType { get; set; }

        public static InfrastructureLoaderHelper Create(AppDomain ad, string Dllname)
        {
            var obj = (InfrastructureLoaderHelper)ad.CreateInstanceFromAndUnwrap("CozyLauncher.Tool.Update.exe", typeof(InfrastructureLoaderHelper).FullName);
            obj.LoadAssembly(Dllname);
            return obj;
        }

        public void LoadAssembly(string dllName)
        {
            LoadedAsm = Assembly.LoadFile(Path.GetFullPath(dllName));
        }

        public void LoadType(string typename)
        {
            if (LoadedAsm != null)
            {
                LoadedObjectType = LoadedAsm.GetType(typename);
            }
        }

        public void LoadObject()
        {
            if (LoadedAsm != null && LoadedObjectType != null)
            {
                LoadedObject = Activator.CreateInstance(LoadedObjectType);
            }
        }

        public object Invoke(string method, params object[] args)
        {
            if (LoadedAsm != null && LoadedObject != null && LoadedObjectType != null)
            {
                var methodinfo = LoadedObjectType.GetMethod(method);
                if (methodinfo != null)
                {
                    return methodinfo.Invoke(LoadedObject, args);
                }
            }
            return null;
        }

        public object InvokeStaticMethod(string methodname, params object[] args)
        {
            if (LoadedAsm != null && LoadedObjectType != null)
            {
                var methodinfo = LoadedObjectType.GetMethod(methodname);
                if (methodinfo != null)
                {
                    return methodinfo.Invoke(null, args);
                }
            }
            return null;
        }

        public void Unload()
        {
            LoadedObject = null;
            LoadedObjectType = null;
            LoadedAsm = null;
        }
    }
}
