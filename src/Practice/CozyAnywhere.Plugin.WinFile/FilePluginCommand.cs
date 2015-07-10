using CozyAnywhere.Plugin.WinFile.ArgsFactory;
using CozyAnywhere.Protocol;
using System.Collections.Generic;
using System.Text;

namespace CozyAnywhere.Plugin.WinFile
{
    public partial class FilePlugin
    {
        private Dictionary<string, IPluginCommandMethodArgsFactory> MethodDictionary
                = new Dictionary<string, IPluginCommandMethodArgsFactory>();

        private void RegisterMethod()
        {
            MethodDictionary["FileCopy"] = new FileCopyArgsFactory(); ;
            MethodDictionary["FileDelete"] = new FileDeleteArgsFactory(); ;
            MethodDictionary["FileEnum"] = new FileEnumArgsFactory(); ;
            MethodDictionary["FileGetLength"] = new FileGetLengthArgsFactory(); ;
            MethodDictionary["FileGetTimes"] = new FileGetTimesArgsFactory(); ;
            MethodDictionary["FileIsDirectory"] = new FileIsDirectoryArgsFactory(); ;
            MethodDictionary["FileMove"] = new FileMoveArgsFactory(); ;
            MethodDictionary["FilePathExist"] = new FilePathExistArgsFactory(); ;
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