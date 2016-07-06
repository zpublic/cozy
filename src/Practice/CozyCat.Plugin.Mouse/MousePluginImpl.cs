using System.IO;

namespace CozyCat.Plugin.Mouse
{
    class MousePluginImpl
    {
        public static bool IsExist(string filepath)
        {
            if (File.Exists(filepath))
            {
                return true;
            }
            return false;
        }
    }
}
