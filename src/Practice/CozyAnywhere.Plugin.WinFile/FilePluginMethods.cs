using CozyAnywhere.Plugin.WinFile.Args;
using CozyAnywhere.Protocol;
using System;

namespace CozyAnywhere.Plugin.WinFile
{
    public partial class FilePlugin
    {
        public object Dispatch(PluginCommandMethodArgs args)
        {
            return args.Execute(this);
        }

        public object Shell(PluginCommandMethodArgs args)
        {
            throw new Exception("Unknow Command Args");
        }

        public object Shell(FileCopyArgs CopyArgs)
        {
            return FileUtil.FileCopy(CopyArgs.SourcePath, CopyArgs.DestPath, CopyArgs.FailIfExists);
        }

        public object Shell(FileDeleteArgs DeleteArgs)
        {
            return FileUtil.FileDelete(DeleteArgs.Path);
        }

        public object Shell(FileEnumArgs EnumArgs)
        {
            return FileUtil.DefFileEnum(EnumArgs.Path, EnumArgs.EnumSize, EnumArgs.EnumTime);
        }

        public object Shell(FileGetLengthArgs LengthArgs)
        {
            return FileUtil.GetFileLength(LengthArgs.Path);
        }

        public object Shell(FileGetTimesArgs TimesArgs)
        {
            return FileUtil.DefGetFileTimes(TimesArgs.Path);
        }

        public object Shell(FileIsDirectoryArgs DireArgs)
        {
            return FileUtil.IsDirectory(DireArgs.Path);
        }

        public object Shell(FileMoveArgs MoveArgs)
        {
            return FileUtil.FileMove(MoveArgs.SourcePath, MoveArgs.DestPath);
        }

        public object Shell(FilePathExistArgs ExistArgs)
        {
            return FileUtil.PathFileExist(ExistArgs.Path);
        }
    }
}