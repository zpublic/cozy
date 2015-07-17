using CozyAnywhere.Plugin.WinFile.Args;
using CozyAnywhere.Protocol;
using System;
using Newtonsoft.Json;
using CozyAnywhere.PluginBase;

namespace CozyAnywhere.Plugin.WinFile
{
    public partial class FilePlugin
    {
        public PluginMethodReturnValueType Dispatch(IPluginCommandMethodArgs args)
        {
            return args.Execute(this);
        }

        public PluginMethodReturnValueType Shell(IPluginCommandMethodArgs args)
        {
            throw new Exception("Unknow Command Args");
        }

        public PluginMethodReturnValueType Shell(FileCopyArgs CopyArgs)
        {
            var result = FileUtil.FileCopy(CopyArgs.SourcePath, CopyArgs.DestPath, CopyArgs.FailIfExists);
            return new PluginMethodReturnValueType()
            {
                DataType    = PluginMethodReturnValueType.StringDataType,
                Data        = JsonConvert.SerializeObject(result),
            };
        }

        public PluginMethodReturnValueType Shell(FileDeleteArgs DeleteArgs)
        {
            var result = FileUtil.FileDelete(DeleteArgs.Path);
            return new PluginMethodReturnValueType()
            {
                DataType    = PluginMethodReturnValueType.StringDataType,
                Data        = JsonConvert.SerializeObject(result),
            };
        }

        public PluginMethodReturnValueType Shell(FileEnumArgs EnumArgs)
        {
            var result = FileUtil.DefFileEnum(EnumArgs.Path, EnumArgs.EnumSize, EnumArgs.EnumTime);
            return new PluginMethodReturnValueType()
            {
                DataType    = PluginMethodReturnValueType.StringDataType,
                Data        = JsonConvert.SerializeObject(result),
            };
        }

        public PluginMethodReturnValueType Shell(FileGetLengthArgs LengthArgs)
        {
            var result = FileUtil.GetFileLength(LengthArgs.Path);
            return new PluginMethodReturnValueType()
            {
                DataType    = PluginMethodReturnValueType.StringDataType,
                Data        = JsonConvert.SerializeObject(result),
            };
        }

        public PluginMethodReturnValueType Shell(FileGetTimesArgs TimesArgs)
        {
            var result = FileUtil.DefGetFileTimes(TimesArgs.Path);
            return new PluginMethodReturnValueType()
            {
                DataType    = PluginMethodReturnValueType.StringDataType,
                Data        = JsonConvert.SerializeObject(result),
            };
        }

        public PluginMethodReturnValueType Shell(FileIsDirectoryArgs DireArgs)
        {
            var result = FileUtil.IsDirectory(DireArgs.Path);
            return new PluginMethodReturnValueType()
            {
                DataType    = PluginMethodReturnValueType.StringDataType,
                Data        = JsonConvert.SerializeObject(result),
            };
        }

        public PluginMethodReturnValueType Shell(FileMoveArgs MoveArgs)
        {
            var result = FileUtil.FileMove(MoveArgs.SourcePath, MoveArgs.DestPath);
            return new PluginMethodReturnValueType()
            {
                DataType    = PluginMethodReturnValueType.StringDataType,
                Data        = JsonConvert.SerializeObject(result),
            };
        }

        public PluginMethodReturnValueType Shell(FilePathExistArgs ExistArgs)
        {
            var result = FileUtil.PathFileExist(ExistArgs.Path);
            return new PluginMethodReturnValueType()
            {
                DataType    = PluginMethodReturnValueType.StringDataType,
                Data        = JsonConvert.SerializeObject(result),
            };
        }
    }
}