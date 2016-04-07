using CozyThunder.Schedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyThunder.Protocol.FileBlock
{
    public class FileBlockTask : Packet
    {
        internal static readonly int PacketId = 10004;
        private const int PacketLength = 32;
        public DownloadSubTask task_;

        public FileBlockTask()
        {
        }

        public FileBlockTask(DownloadSubTask task)
        {
            task_ = task;
        }

        public override void Encode(byte[] buffer, int offset)
        {
            int written = offset;
            written += Write(buffer, written, PacketLength + task_.RemotePath.Length);
            written += Write(buffer, written, PacketId);
            written += Write(buffer, written, task_.Id);
            written += Write(buffer, written, task_.from);
            written += Write(buffer, written, task_.to);
            written += Write(buffer, written, task_.RemotePath.Length);
            written += WriteAscii(buffer, written, task_.RemotePath);
        }

        public override void Decode(byte[] buffer, int offset, int length)
        {
            task_ = new DownloadSubTask();
            ReadInt(buffer, ref offset);
            ReadInt(buffer, ref offset);
            task_.Id = ReadInt(buffer, ref offset);
            task_.from = ReadLong(buffer, ref offset);
            task_.to = ReadLong(buffer, ref offset);
            var len = ReadInt(buffer, ref offset);
            task_.RemotePath = ReadString(buffer, offset, len);
        }

        public override int ByteLength
        {
            get { return PacketLength + task_.RemotePath.Length; }
        }
    }
}
