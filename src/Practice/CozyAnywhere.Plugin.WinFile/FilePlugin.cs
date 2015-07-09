using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyAnywhere.PluginBase;
using CozyAnywhere.Protocol;
using CozyAnywhere.Plugin.WinFile.ArgsFactory;

namespace CozyAnywhere.Plugin.WinFile
{
    public partial class FilePlugin : BasePlugin
    {
        private Dictionary<string, PluginCommandMethodPacket> MethodDictionary
            = new Dictionary<string, PluginCommandMethodPacket>();

        public override string PluginName { get { return "FilePlugin"; } }

        public override object Shell(string commandContent)
        {
            var context     = PluginCommandMethod.Create(commandContent);
            var methodName  = context.MethodName;
            var methodArgs  = context.MethodArgs;
            if(MethodDictionary.ContainsKey(methodName))
            {
                var packet  = MethodDictionary[methodName];
                var func    = packet.Function;
                var args    = packet.ArgsFactory.Create(methodArgs);
                return func(args);
            }
            return PluginCommand.NullReturnValue;
        }

        public FilePlugin()
        {
            RegisterMethod();
        }

        private void RegisterMethod()
        {
            var CopyPacket      = PluginCommandMethodPacket.Create(OnFileCopy, new FileCopyArgsFactory());
            var DeletePacket    = PluginCommandMethodPacket.Create(OnFileDelete, new FileDeleteArgsFactory());
            var EnumPacket      = PluginCommandMethodPacket.Create(OnFileEnum, new FileEnumArgsFactory());
            var GetLengthPacket = PluginCommandMethodPacket.Create(OnFileGetLength, new FileGetLengthArgsFactory());
            var GetTimesPacket  = PluginCommandMethodPacket.Create(OnFileGetTimes, new FileGetTimesArgsFactory());
            var IsDirePacket    = PluginCommandMethodPacket.Create(OnFileIsDire, new FileIsDirectoryArgsFactory());
            var MovePacket      = PluginCommandMethodPacket.Create(OnFileMove, new FileMoveArgsFactory());
            var PathExistPacket = PluginCommandMethodPacket.Create(OnFilePathExist, new FilePathExistArgsFactory());

            MethodDictionary["FileCopy"]            = CopyPacket;
            MethodDictionary["FileDelete"]          = DeletePacket;
            MethodDictionary["FileEnum"]            = EnumPacket;
            MethodDictionary["FileGetLength"]       = GetLengthPacket;
            MethodDictionary["FileGetTimes"]        = GetTimesPacket;
            MethodDictionary["FileIsDirectory"]     = IsDirePacket;
            MethodDictionary["FileMove"]            = MovePacket;
            MethodDictionary["FilePathExist"]       = PathExistPacket;
        }
    }
}
