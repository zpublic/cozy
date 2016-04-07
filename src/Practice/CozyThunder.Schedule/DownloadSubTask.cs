namespace CozyThunder.Schedule
{
    public class DownloadSubTask
    {
        public int Id;
        public string RemotePath;
        public long from;
        public long to;
        public byte[] data;
    }
}
