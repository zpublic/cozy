using CozyAnywhere.Plugin.WinFile.ArgsFactory;
using CozyAnywhere.Protocol;
using System.Collections.Generic;
using System.Text;

namespace CozyAnywhere.Plugin.WinFile
{
    public partial class FilePlugin
    {
        private Dictionary<string, PluginCommandMethodPacket> MethodDictionary
                = new Dictionary<string, PluginCommandMethodPacket>();

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

            MethodDictionary["FileCopy"] = CopyPacket;
            MethodDictionary["FileDelete"] = DeletePacket;
            MethodDictionary["FileEnum"] = EnumPacket;
            MethodDictionary["FileGetLength"] = GetLengthPacket;
            MethodDictionary["FileGetTimes"] = GetTimesPacket;
            MethodDictionary["FileIsDirectory"] = IsDirePacket;
            MethodDictionary["FileMove"] = MovePacket;
            MethodDictionary["FilePathExist"] = PathExistPacket;
        }

        public static string MakeFileCopyCommand(string SourcePath, string DestPath, bool FailIfExists)
        {
            var result = new StringBuilder();
            CommandMake.AppendHeader(result, InnerPluginName);
            CommandMake.AppendMethodName(result, "FileCopy");
            CommandMake.AppendArguments(result, SourcePath, DestPath, FailIfExists);
            CommandMake.AppendFooter(result);
            return result.ToString();
        }

        public static string MakeFileDeleteCommand(string Path)
        {
            var result = new StringBuilder();
            CommandMake.AppendHeader(result, InnerPluginName);
            CommandMake.AppendMethodName(result, "FileDelete");
            CommandMake.AppendArguments(result, Path);
            CommandMake.AppendFooter(result);
            return result.ToString();
        }

        public static string MakeFileEnumCommand(string Path, bool EnumSize, bool EnumTime)
        {
            var result = new StringBuilder();
            CommandMake.AppendHeader(result, InnerPluginName);
            CommandMake.AppendMethodName(result, "FileEnum");
            CommandMake.AppendArguments(result, Path, EnumSize, EnumTime);
            CommandMake.AppendFooter(result);
            return result.ToString();
        }

        public static string MakeFileGetLengthCommand(string Path)
        {
            var result = new StringBuilder();
            CommandMake.AppendHeader(result, InnerPluginName);
            CommandMake.AppendMethodName(result, "FileGetLength");
            CommandMake.AppendArguments(result, Path);
            CommandMake.AppendFooter(result);
            return result.ToString();
        }

        public static string MakeFileGetTimesCommand(string Path)
        {
            var result = new StringBuilder();
            CommandMake.AppendHeader(result, InnerPluginName);
            CommandMake.AppendMethodName(result, "FileGetTimes");
            CommandMake.AppendArguments(result, Path);
            CommandMake.AppendFooter(result);
            return result.ToString();
        }

        public static string MakeFileIsDirectoryCommand(string Path)
        {
            var result = new StringBuilder();
            CommandMake.AppendHeader(result, InnerPluginName);
            CommandMake.AppendMethodName(result, "FileIsDirectory");
            CommandMake.AppendArguments(result, Path);
            CommandMake.AppendFooter(result);
            return result.ToString();
        }

        public static string MakeFileMoveCommand(string SourcePath, string DestPath)
        {
            var result = new StringBuilder();
            CommandMake.AppendHeader(result, InnerPluginName);
            CommandMake.AppendMethodName(result, "FileMove");
            CommandMake.AppendArguments(result, SourcePath, DestPath);
            CommandMake.AppendFooter(result);
            return result.ToString();
        }

        public static string MakeFilePathExistCommand(string Path)
        {
            var result = new StringBuilder();
            CommandMake.AppendHeader(result, InnerPluginName);
            CommandMake.AppendMethodName(result, "FilePathExist");
            CommandMake.AppendArguments(result, Path);
            CommandMake.AppendFooter(result);
            return result.ToString();
        }
    }
}