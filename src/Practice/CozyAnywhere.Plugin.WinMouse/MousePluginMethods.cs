using CozyAnywhere.Plugin.WinMouse.Args;
using CozyAnywhere.Protocol;
using Newtonsoft.Json;
using CozyAnywhere.PluginBase;
using System;

namespace CozyAnywhere.Plugin.WinMouse
{
    public partial class MousePlugin
    {
        public PluginMethodReturnValueType Dispatch(IPluginCommandMethodArgs args)
        {
            return args.Execute(this);
        }

        public PluginMethodReturnValueType Shell(IPluginCommandMethodArgs args)
        {
            throw new Exception("Unknow Command Args");
        }

        public PluginMethodReturnValueType Shell(MouseClickArgs args)
        {
            MouseUtil.MouseClick(args.Tag, args.X, args.Y);
            return new PluginMethodReturnValueType()
            {
                DataType    = PluginMethodReturnValueType.NoDataType,
            };
        }

        public PluginMethodReturnValueType Shell(MouseCursorClipArgs args)
        {
            var result = MouseUtil.CursorClip(args.Left, args.Top, args.Right, args.Bottom);
            return new PluginMethodReturnValueType()
            {
                DataType    = PluginMethodReturnValueType.StringDataType,
                Data        = JsonConvert.SerializeObject(result),
            };
        }

        public PluginMethodReturnValueType Shell(MouseCursorUnClipArgs args)
        {
            var result = MouseUtil.CursorUnClip();
            return new PluginMethodReturnValueType()
            {
                DataType    = PluginMethodReturnValueType.StringDataType,
                Data        = JsonConvert.SerializeObject(result),
            };
        }

        public PluginMethodReturnValueType Shell(MouseGetCurrsorPositionArgs args)
        {
            var result = MouseUtil.DefGetCursorPosition();
            return new PluginMethodReturnValueType()
            {
                DataType    = PluginMethodReturnValueType.StringDataType,
                Data        = JsonConvert.SerializeObject(result),
            };
        }

        public PluginMethodReturnValueType Shell(MouseLeftClickArgs args)
        {
            MouseUtil.LeftClick(args.X, args.Y);
            return new PluginMethodReturnValueType()
            {
                DataType    = PluginMethodReturnValueType.NoDataType,
            };
        }

        public PluginMethodReturnValueType Shell(MouseMiddleClickArgs args)
        {
            MouseUtil.MiddleClick(args.X, args.Y);
            return new PluginMethodReturnValueType()
            {
                DataType    = PluginMethodReturnValueType.NoDataType,
            };
        }

        public PluginMethodReturnValueType Shell(MouseEventArgs args)
        {
            MouseUtil.MouseEvent(args.Tag, args.X, args.Y, args.Data, args.ExtInfo);
            return new PluginMethodReturnValueType()
            {
                DataType    = PluginMethodReturnValueType.NoDataType,
            };
        }

        public PluginMethodReturnValueType Shell(MouseRightClickArgs args)
        {
            MouseUtil.RightClick(args.X, args.Y);
            return new PluginMethodReturnValueType()
            {
                DataType    = PluginMethodReturnValueType.NoDataType,
            };
        }

        public PluginMethodReturnValueType Shell(MouseSetCursorPositionArgs args)
        {
            var result = MouseUtil.SetCursorPosition(args.X, args.Y);
            return new PluginMethodReturnValueType()
            {
                DataType    = PluginMethodReturnValueType.StringDataType,
                Data        = JsonConvert.SerializeObject(result),
            };
        }
    }
}