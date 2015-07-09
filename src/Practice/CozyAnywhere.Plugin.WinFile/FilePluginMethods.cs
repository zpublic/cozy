using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyAnywhere.Protocol;
using CozyAnywhere.Plugin.WinFile.Args;

namespace CozyAnywhere.Plugin.WinFile
{
    public partial class FilePlugin
    {
        private object OnFileCopy(IPluginCommandMethodArgs args)
        {
            var CopyArgs = args as FileCopyArgs;
            if(CopyArgs != null)
            {
                return FileUtil.FileCopy(CopyArgs.SourcePath, CopyArgs.DestPath, CopyArgs.FailIfExists);
            }
            return PluginCommand.NullReturnValue;
        }

        private object OnFileDelete(IPluginCommandMethodArgs args)
        {
            var DeleteArgs = args as FileDeleteArgs;
            if (DeleteArgs != null)
            {
                return FileUtil.FileDelete(DeleteArgs.Path);
            }
            return PluginCommand.NullReturnValue;
        }

        private object OnFileEnum(IPluginCommandMethodArgs args)
        {
            var EnumArgs = args as FileEnumArgs;
            if(EnumArgs != null)
            {
                return FileUtil.DefFileEnum(EnumArgs.Path, EnumArgs.EnumSize, EnumArgs.EnumTime);
            }
            return PluginCommand.NullReturnValue;
        }

        private object OnFileGetLength(IPluginCommandMethodArgs args)
        {
            var LengthArgs = args as FileGetLengthArgs;
            if(LengthArgs != null)
            {
                return FileUtil.GetFileLength(LengthArgs.Path);
            }
            return PluginCommand.NullReturnValue;
        }

        private object OnFileGetTimes(IPluginCommandMethodArgs args)
        {
            var TimesArgs = args as FileGetTimesArgs;
            if(TimesArgs != null)
            {
                return FileUtil.DefGetFileTimes(TimesArgs.Path);
            }
            return PluginCommand.NullReturnValue;
        }

        private object OnFileIsDire(IPluginCommandMethodArgs args)
        {
            var DireArgs = args as FileIsDirectoryArgs;
            if(DireArgs != null)
            {
                return FileUtil.IsDirectory(DireArgs.Path);
            }
            return PluginCommand.NullReturnValue; 
        }

        private object OnFileMove(IPluginCommandMethodArgs args)
        {
            var MoveArgs = args as FileMoveArgs;
            if(MoveArgs != null)
            {
                return FileUtil.FileMove(MoveArgs.SourcePath, MoveArgs.DestPath);
            }
            return PluginCommand.NullReturnValue; 
        }

        private object OnFilePathExist(IPluginCommandMethodArgs args)
        {
            var ExistArgs = args as FilePathExistArgs;
            if(ExistArgs != null)
            {
                return FileUtil.PathFileExist(ExistArgs.Path);
            }
            return PluginCommand.NullReturnValue; 
        }
    }
}
