using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyThunder.DistributedDownload.MasterGui.Common
{
    public class MessageBuffer
    {
        private List<byte> BufferData { get; set; } = new List<byte>();

        public void Append(byte[] data)
        {
            BufferData.AddRange(data);
        }

        public void Append(byte[] data, int length)
        {
            BufferData.AddRange(data.Take(length));
        }

        public void Append(byte[] data, int offset, int length)
        {
            BufferData.AddRange(data.Skip(offset).Take(length));
        }

        public void Clear()
        {
            BufferData.Clear();
        }

        public int Length
        {
            get
            {
                return BufferData.Count;
            }
        }

        public byte[] RawData
        {
            get
            {
                return BufferData.ToArray();
            }
        }
    }
}
