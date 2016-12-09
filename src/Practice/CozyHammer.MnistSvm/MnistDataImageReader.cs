using System.IO;

namespace CozyHammer.MnistSvm
{
    public class MnistDataImageReader
    {
        uint itemCount = 0;
        public uint ItemCount
        {
            get { return itemCount; }
            set { itemCount = value; }
        }
        public byte[][] Items { get; set; }

        public bool ParseData(string filePath)
        {
            try
            {
                var fs = new FileStream(filePath, FileMode.Open);
                var br = new BinaryReader(fs);
                br.ReadInt32();
                itemCount = Utils.ReverseUint(br.ReadUInt32());
                var itemSize = 28 * 28;
                var dataSize = itemCount * itemSize + 16;
                if (Utils.GetFileSize(filePath) == dataSize)
                {
                    Items = new byte[itemCount][];
                    for (var i = 0; i < itemCount; ++i)
                        Items[i] = br.ReadBytes(itemSize);
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
