using CozyAnywhere.Plugin.WinFile.Args;
using CozyAnywhere.Protocol;
using System;
using Newtonsoft.Json;

namespace CozyAnywhere.Plugin.WinFile
{
    public partial class FilePlugin
    {
        public string Dispatch(IPluginCommandMethodArgs args)
        {
            return args.Execute(this);
        }

        public string Shell(IPluginCommandMethodArgs args)
        {
            throw new Exception("Unknow Command Args");
        }

        public string Shell(FileCopyArgs CopyArgs)
        {
            var result = FileUtil.FileCopy(CopyArgs.SourcePath, CopyArgs.DestPath, CopyArgs.FailIfExists);
            return JsonConvert.SerializeObject(result);
        }

        public string Shell(FileDeleteArgs DeleteArgs)
        {
            var result = FileUtil.FileDelete(DeleteArgs.Path);
            return JsonConvert.SerializeObject(result);
        }

        public string Shell(FileEnumArgs EnumArgs)
        {
            var result = FileUtil.DefFileEnum(EnumArgs.Path, EnumArgs.EnumSize, EnumArgs.EnumTime);
            return JsonConvert.SerializeObject(result);
        }

        public string Shell(FileGetLengthArgs LengthArgs)
        {
            var result = FileUtil.GetFileLength(LengthArgs.Path);
            return JsonConvert.SerializeObject(result);
        }

        public string Shell(FileGetTimesArgs TimesArgs)
        {
            var result = FileUtil.DefGetFileTimes(TimesArgs.Path);
            return JsonConvert.SerializeObject(result);
        }

        public string Shell(FileIsDirectoryArgs DireArgs)
        {
            var result = FileUtil.IsDirectory(DireArgs.Path);
            return JsonConvert.SerializeObject(result);
        }

        public string Shell(FileMoveArgs MoveArgs)
        {
            var result = FileUtil.FileMove(MoveArgs.SourcePath, MoveArgs.DestPath);
            return JsonConvert.SerializeObject(result);
        }

        public string Shell(FilePathExistArgs ExistArgs)
        {
            var result = FileUtil.PathFileExist(ExistArgs.Path);
            return JsonConvert.SerializeObject(result);
        }
    }
}