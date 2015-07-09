namespace CozyAnywhere.Protocol
{
    public static class CommandId
    {
        // FileCommand
        public const int FileDeleteCommand          = 1000;
        public const int FileCopyCommand            = 1001;
        public const int FileMoveCommand            = 1002;
        public const int FilePathExistCommand       = 1003;
        public const int FileIsDirectoryCommand     = 1004;
        public const int FileGetLengthCommand       = 1005;
        public const int FileGetTimesCommand        = 1006;
        public const int FileEnumCommand            = 1007;
    }
}