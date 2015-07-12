using CozyAnywhere.Plugin.WinMouse.Args;
using CozyAnywhere.Protocol;
using Newtonsoft.Json;
using System;

namespace CozyAnywhere.Plugin.WinMouse
{
    public partial class MousePlugin
    {
        public string Dispatch(PluginCommandMethodArgs args)
        {
            return args.Execute(this);
        }

        public string Shell(PluginCommandMethodArgs args)
        {
            throw new Exception("Unknow Command Args");
        }

        public string Shell(MouseClickArgs args)
        {
            MouseUtil.MouseClick(args.Tag, args.X, args.Y);
            return null;
        }

        public string Shell(MouseCursorClipArgs args)
        {
            var result = MouseUtil.CursorClip(args.Left, args.Top, args.Right, args.Bottom);
            return JsonConvert.SerializeObject(result);
        }

        public string Shell(MouseCursorUnClipArgs args)
        {
            var result = MouseUtil.CursorUnClip();
            return JsonConvert.SerializeObject(result);
        }

        public string Shell(MouseGetCurrsorPositionArgs args)
        {
            var result = MouseUtil.DefGetCursorPosition();
            return JsonConvert.SerializeObject(result);
        }

        public string Shell(MouseLeftClickArgs args)
        {
            MouseUtil.LeftClick(args.X, args.Y);
            return null;
        }

        public string Shell(MouseMiddleClickArgs args)
        {
            MouseUtil.MiddleClick(args.X, args.Y);
            return null;
        }

        public string Shell(MouseEventArgs args)
        {
            MouseUtil.MouseEvent(args.Tag, args.X, args.Y, args.Data, args.ExtInfo);
            return null;
        }

        public string Shell(MouseRightClickArgs args)
        {
            MouseUtil.RightClick(args.X, args.Y);
            return null;
        }

        public string Shell(MouseSetCursorPositionArgs args)
        {
            var result = MouseUtil.SetCursorPosition(args.X, args.Y);
            return JsonConvert.SerializeObject(result);
        }
    }
}