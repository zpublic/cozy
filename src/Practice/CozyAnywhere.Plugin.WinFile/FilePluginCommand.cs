using CozyAnywhere.Plugin.WinFile.Args;
using CozyAnywhere.Protocol;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Reflection;
using System;
using PluginHelper;

namespace CozyAnywhere.Plugin.WinFile
{
    public partial class FilePlugin
    {
        private Dictionary<string, IPluginCommandMethodArgsFactory> MethodDictionary
                = new Dictionary<string, IPluginCommandMethodArgsFactory>();

        private void RegisterMethod()
        {
            var asm         = Assembly.GetExecutingAssembly();
            var factorylist = ArgsFactoryLoader.LoadArgsFactory(asm, "CozyAnywhere.Plugin.WinFile.ArgsFactory");

            foreach (var obj in factorylist)
            {
                var factory                 = (IPluginCommandMethodArgsFactory)Activator.CreateInstance(obj.Item2);
                MethodDictionary[obj.Item1] = factory;
            }
        }

        public static string MakeFileCopyCommand(string sourcePath, string destPath, bool failIfExists)
        {
            var args = new FileCopyArgs()
            {
                SourcePath      = sourcePath,
                DestPath        = destPath,
                FailIfExists    = failIfExists,
            };
            var argsSerialize = JsonConvert.SerializeObject(args);
            return PluginCommandSerializeMaker.MakeCommand(InnerPluginName, "FileCopy", argsSerialize);
        }

        public static string MakeFileDeleteCommand(string path)
        {
            var args = new FileDeleteArgs()
            {
                Path            = path,
            };
            var argsSerialize = JsonConvert.SerializeObject(args);
            return PluginCommandSerializeMaker.MakeCommand(InnerPluginName, "FileDelete", argsSerialize);
        }

        public static string MakeFileEnumCommand(string path, bool enumSize, bool enumTime)
        {
            var args = new FileEnumArgs()
            {
                Path            = path,
                EnumSize        = enumSize,
                EnumTime        = enumTime,
            };
            var argsSerialize = JsonConvert.SerializeObject(args);
            return PluginCommandSerializeMaker.MakeCommand(InnerPluginName, "FileEnum", argsSerialize);
        }

        public static string MakeFileGetLengthCommand(string path)
        {
            var args = new FileGetLengthArgs()
            {
                Path            = path,
            };
            var argsSerialize = JsonConvert.SerializeObject(args);
            return PluginCommandSerializeMaker.MakeCommand(InnerPluginName, "FileGetLength", argsSerialize);
        }

        public static string MakeFileGetTimesCommand(string path)
        {
            var args = new FileGetTimesArgs()
            {
                Path            = path,
            };
            var argsSerialize = JsonConvert.SerializeObject(args);
            return PluginCommandSerializeMaker.MakeCommand(InnerPluginName, "FileGetTimes", argsSerialize);
        }

        public static string MakeFileIsDirectoryCommand(string path)
        {
            var args = new FileIsDirectoryArgs()
            {
                Path            = path,
            };
            var argsSerialize = JsonConvert.SerializeObject(args);
            return PluginCommandSerializeMaker.MakeCommand(InnerPluginName, "FileIsDirectory", argsSerialize);
        }

        public static string MakeFileMoveCommand(string sourcePath, string destPath)
        {
            var args = new FileMoveArgs()
            {
                SourcePath      = sourcePath,
                DestPath        = destPath,
            };
            var argsSerialize = JsonConvert.SerializeObject(args);
            return PluginCommandSerializeMaker.MakeCommand(InnerPluginName, "FileMove", argsSerialize);
        }

        public static string MakeFilePathExistCommand(string path)
        {
            var args = new FilePathExistArgs()
            {
                Path            = path,
            };
            var argsSerialize = JsonConvert.SerializeObject(args);
            return PluginCommandSerializeMaker.MakeCommand(InnerPluginName, "FilePathExist", argsSerialize);
        }

        public static string MakeFileGetCurrentDirectoryCommand()
        {
            var args            = new FileGetCurrentDirectoryArgs();
            var argsSerialize   = JsonConvert.SerializeObject(args);
            return PluginCommandSerializeMaker.MakeCommand(InnerPluginName, "FileGetCurrentDirectory", argsSerialize);
        }
    }
}