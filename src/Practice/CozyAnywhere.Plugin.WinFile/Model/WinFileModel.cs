namespace CozyAnywhere.Plugin.WinFile.Model
{
    public class WinFileModel
    {
        public string Name { get; set; }

        public bool IsFolder { get; set; }

        public ulong Size { get; set; }

        public WinFileTimeModel Times { get; set; }
    }
}
