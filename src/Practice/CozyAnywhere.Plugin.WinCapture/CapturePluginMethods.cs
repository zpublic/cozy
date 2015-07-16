using CozyAnywhere.Protocol;
using System;
using Newtonsoft.Json;
using CozyAnywhere.Plugin.WinCapture.Args;
using System.Text;

namespace CozyAnywhere.Plugin.WinCapture
{
    public partial class CapturePlugin
    {
        public string Dispatch(IPluginCommandMethodArgs args)
        {
            return args.Execute(this);
        }

        public string Shell(IPluginCommandMethodArgs args)
        {
            throw new Exception("Unknow Command Args");
        }

        public string Shell(GetCaptureDataArgs CopyArgs)
        {
            var bitmapData = CaptureUtil.DefGetCaptureData();
            if (bitmapData != null)
            {
                var jpedData = CaptureUtil.ConvertBmpToJpeg(bitmapData);
                if(jpedData != null)
                {
                    return Convert.ToBase64String(jpedData);
                }
            }
            return null;
        }
    }
}
