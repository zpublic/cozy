using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyAnywhere.PluginBase;
using CozyAnywhere.Protocol.Commands;

namespace CozyAnywhere.Plugin.WinFile
{
    public class FilePlugin : BasePlugin
    {
        public override string PluginName { get { return "FilePlugin"; } }

        public void Shell(FileDeleteCommand command)
        {
            FileUtil.FileDelete(command.Path);
            // TODO On FileDeleteCommand
        }

        public void Shell(FileCopyCommand command)
        {
            FileUtil.FileCopy(command.SourcePath, command.DestPath, command.FailIfExists);
        }

        public void Shell(FileMoveCommand command)
        {
            FileUtil.FileMove(command.SourcePath, command.DestPath);
        }

        public void Shell(FilePathExistCommand command)
        {
            FileUtil.PathFileExist(command.Path);
        }

        public void Shell(FileIsDirectoryCommand command)
        {
            FileUtil.IsDirectory(command.Path);
        }

        public void Shell(FileGetLengthCommand command)
        {
            FileUtil.GetFileLength(command.Path);
        }

        public void Shell(FileGetTimesCommand command)
        {
            ulong CreationTime      = 0;
            ulong LastAccessTime    = 0;
            ulong LastWriteTime     = 0;

            FileUtil.GetFileTimes(command.Path, ref CreationTime, ref LastAccessTime, ref LastWriteTime);

            command.CreationTime    = CreationTime;
            command.LastAccessTime  = LastAccessTime;
            command.LastWriteTime   = LastWriteTime;
        }

        public void Shell(FileEnumCommand command)
        {
            FileUtil.FileEnum(command.Path, (x, b) => { return command.EnumFunc(x, b); });
        }
    }
}
