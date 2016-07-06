using CozyLua.Plugin;
using System;

namespace CozyCat.Plugin.Mouse
{
    public class MousePlugin : CozyLuaPluginBase
    {
        public MousePlugin()
        {
            mMethods.Add("Mouse_IsExist", typeof(MousePluginImpl).GetMethod(
                    "IsExist",
                    new Type[] { typeof(string) }));
        }
    }
}
