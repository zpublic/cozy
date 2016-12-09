using System.IO;

namespace CozyHammer.MnistSvm
{
    public class MnistDataLabelReader
    {
        uint itemCount = 0;
        public uint ItemCount
        {
            get { return itemCount; }
            set { itemCount = value; }
        }
        public byte[] Items { get; set; }

        public bool ParseData(string filePath)
        {
            try
            {
                var fs = new FileStream(filePath, FileMode.Open);
                var br = new BinaryReader(fs);
                br.ReadInt32();
                itemCount = Utils.ReverseUint(br.ReadUInt32());
                var dataSize = itemCount + 8;
                if (Utils.GetFileSize(filePath) == dataSize)
                {
                    Items = br.ReadBytes((int)itemCount);
                }
                br.Close();
                fs.Close();
            }
            catch (IOException)
            {
                return false;
            }
            return true;
        }
    }
}
