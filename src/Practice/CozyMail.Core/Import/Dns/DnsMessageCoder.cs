using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace QiHe.CodeLib.Net.Dns
{
    internal class DnsMessageCoder
    {
        #region Encode

        /*
        All communications inside of the domain protocol are carried in a single
        format called a message.  The top level format of message is divided
        into 5 sections (some of which are empty in certain cases) shown below:
        +---------------------+
        |        Header       |
        +---------------------+
        |       Question      | the question for the name server
        +---------------------+
        |        Answer       | RRs answering the question
        +---------------------+
        |      Authority      | RRs pointing toward an authority
        +---------------------+
        |      Additional     | RRs holding additional information
        +---------------------+
         */
        public static byte[] EncodeDnsMessage(DnsMessage message)
        {
            MemoryStream stream = new MemoryStream(512);
            BinaryWriter writer = new BinaryWriter(stream);

            byte[] header = EncodeHeader(message);
            writer.Write(header);

            foreach (DnsQuery query in message.Querys)
            {
                writer.Write(EncodeDnsQuery(query));
            }

            foreach (DnsResource answer in message.Answers)
            {
                writer.Write(EncodeDnsResource(answer));
            }

            foreach (DnsResource record in message.AuthorityRecords)
            {
                writer.Write(EncodeDnsResource(record));
            }

            foreach (DnsResource record in message.AdditionalRecords)
            {
                writer.Write(EncodeDnsResource(record));
            }

            return stream.ToArray();
        }

        /*
         * Header Structure, the bit labeled 0 is the most significant bit.
          
          0  1  2  3  4  5  6  7  8  9  A  B  C  D  E  F
         +--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+
         |                      ID                       |
         +--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+
         |QR|   Opcode  |AA|TC|RD|RA|   Z    |   RCODE   |
         +--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+
         |                    QDCOUNT                    |
         +--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+
         |                    ANCOUNT                    |
         +--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+
         |                    NSCOUNT                    |
         +--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+
         |                    ARCOUNT                    |
         +--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+
        */
        public static byte[] EncodeHeader(DnsMessage message)
        {
            MemoryStream stream = new MemoryStream(12);
            BinaryWriter writer = new BinaryWriter(stream);

            WriteUInt16BE(writer, message.ID);

            int fields = (int)message.ResponseCode;
            if (message.IsRecursionAvailable)
            {
                fields |= 0x80;
            }
            if (message.IsRecursionDesired)
            {
                fields |= 0x100;
            }
            if (message.IsTruncated)
            {
                fields |= 0x200;
            }
            if (message.IsRecursionAvailable)
            {
                fields |= 0x400;
            }

            fields |= (int)message.QueryKind << 11; //Opcode

            if (message.Type == MessageType.Response)
            {
                fields |= 0x8000;
            }

            WriteUInt16BE(writer, (UInt16)fields);
            WriteUInt16BE(writer, (UInt16)message.Querys.Count);
            WriteUInt16BE(writer, (UInt16)message.Answers.Count);
            WriteUInt16BE(writer, (UInt16)message.AuthorityRecords.Count);
            WriteUInt16BE(writer, (UInt16)message.AdditionalRecords.Count);

            return stream.ToArray();
        }

        /* 	4.1.2. Question section format
                                      1  1  1  1  1  1
        0  1  2  3  4  5  6  7  8  9  0  1  2  3  4  5
        +--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+
        |                                               |
        /                     QNAME                     /
        /                                               /
        +--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+
        |                     QTYPE                     |
        +--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+
        |                     QCLASS                    |
        +--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+
			
        QNAME
            a domain name represented as a sequence of labels, where
         each label consists of a length octet followed by that
            number of octets.  The domain name terminates with the
         zero length octet for the null label of the root.  Note
            that this field may be an odd number of octets; no
         padding is used.
        */
        public static byte[] EncodeDnsQuery(DnsQuery query)
        {
            MemoryStream stream = new MemoryStream(256);
            BinaryWriter writer = new BinaryWriter(stream);

            //QNAME
            string[] labels = query.DomainName.Split('.');
            foreach (string label in labels)
            {
                writer.Write((byte)label.Length);
                writer.Write(Encoding.ASCII.GetBytes(label));
            }
            writer.Write((byte)0);

            //QTYPE
            WriteUInt16BE(writer, (UInt16)query.QueryType);

            //QCLASS
            WriteUInt16BE(writer, (UInt16)query.QueryClass);

            return stream.ToArray();
        }

        /*
         Each resource record has the following format:
                                         1  1  1  1  1  1
           0  1  2  3  4  5  6  7  8  9  0  1  2  3  4  5
         +--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+
         |                                               |
         /                                               /
         /                      NAME                     /
         |                                               |
         +--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+
         |                      TYPE                     |
         +--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+
         |                     CLASS                     |
         +--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+
         |                      TTL                      |
         |                                               |
         +--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+
         |                   RDLENGTH                    |
         +--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--|
         /                     RDATA                     /
         /                                               /
         +--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+
        */
        public static byte[] EncodeDnsResource(DnsResource record)
        {
            MemoryStream stream = new MemoryStream(256);
            BinaryWriter writer = new BinaryWriter(stream);

            //NAME
            string[] labels = record.Name.Split('.');
            foreach (string label in labels)
            {
                writer.Write((byte)label.Length);
                writer.Write(Encoding.ASCII.GetBytes(label));
            }
            writer.Write((byte)0);

            //TYPE
            WriteUInt16BE(writer, (UInt16)record.QueryType);
            //CLASS
            WriteUInt16BE(writer, (UInt16)record.QueryClass);
            //TTL
            WriteInt32BE(writer, record.TimeToLive);
            //RDLENGTH
            WriteUInt16BE(writer, record.DataLength);
            //RDATA
            writer.Write(record.Data);

            return stream.ToArray();
        }

        #endregion

        #region Decode
        public static DnsMessage DecodeDnsMessage(byte[] msgData)
        {
            DnsMessage message = new DnsMessage();
            message.ID = (UInt16)(msgData[0] << 8 | msgData[1]);
            message.Type = (MessageType)(msgData[2] >> 7);
            message.QueryKind = (QueryKind)((msgData[2] >> 3) & 15);
            message.ResponseCode = (ResponseCode)(msgData[3] & 15);

            MemoryStream stream = new MemoryStream(msgData);
            stream.Position = 4;
            BinaryReader reader = new BinaryReader(stream);

            int queryCount = ReadUInt16BE(reader);
            int answerCount = ReadUInt16BE(reader);
            int nsCount = ReadUInt16BE(reader);
            int arCount = ReadUInt16BE(reader);

            for (int i = 0; i < queryCount; i++)
            {
                message.Querys.Add(ReadDnsQuery(reader));
            }
            for (int i = 0; i < answerCount; i++)
            {
                message.Answers.Add(ReadResourceRecord(reader));
            }
            for (int i = 0; i < nsCount; i++)
            {
                message.AuthorityRecords.Add(ReadResourceRecord(reader));
            }
            for (int i = 0; i < arCount; i++)
            {
                message.AdditionalRecords.Add(ReadResourceRecord(reader));
            }

            return message;
        }

        public static DnsQuery ReadDnsQuery(BinaryReader reader)
        {
            String domainName = ReadDomainName(reader);
            QueryType qtype = (QueryType)ReadUInt16BE(reader);
            QueryClass qclass = (QueryClass)ReadUInt16BE(reader);

            DnsQuery query = new DnsQuery(domainName, qtype, qclass);
            return query;
        }

        public static DnsResource ReadResourceRecord(BinaryReader reader)
        {
            DnsResource record = new DnsResource();
            record.Name = ReadDomainName(reader);
            record.QueryType = (QueryType)ReadUInt16BE(reader);
            record.QueryClass = (QueryClass)ReadUInt16BE(reader);
            record.TimeToLive = ReadInt32BE(reader);
            record.DataLength = ReadUInt16BE(reader);

            long pos = reader.BaseStream.Position;
            DecodeRecordContent(record, reader);
            if (reader.BaseStream.Position != pos + record.DataLength)
            {
                throw new Exception("Error in decoding content of ResourceRecord");
            }
            return record;
        }

        private static void DecodeRecordContent(DnsResource record, BinaryReader reader)
        {
            switch (record.QueryType)
            {
                case QueryType.Address:
                    record.Content = reader.ReadBytes(4);
                    break;
                case QueryType.NameServer:
                case QueryType.CanonicalName:
                    record.Content = ReadDomainName(reader);
                    break;
                case QueryType.MailExchange:
                    record.Content = ReadMailExchange(reader);
                    break;
                default:
                    record.Data = reader.ReadBytes(record.DataLength);
                    break;
            }
        }

        private static MailExchange ReadMailExchange(BinaryReader reader)
        {
            MailExchange mailExchange = new MailExchange();
            mailExchange.Preference = ReadUInt16BE(reader);
            mailExchange.HostName = ReadDomainName(reader);
            return mailExchange;
        }

        private static string ReadDomainName(BinaryReader reader)
        {
            StringBuilder domainName = new StringBuilder();
            byte len = reader.ReadByte();
            while (len > 0)
            {
                if (len <= 63)
                {
                    byte[] bytes = reader.ReadBytes(len);
                    string label = Encoding.ASCII.GetString(bytes);
                    domainName.Append(label);
                }
                else if ((len & 0xC0) == 0xC0)// a pointer
                {
                    byte b = reader.ReadByte();
                    int offset = (len & 0x3F) << 8 | b;
                    domainName.Append(ReadPointedDomainName(reader, offset));
                    break; // pointer ends the name
                }
                len = reader.ReadByte();
                if (len > 0)
                {
                    domainName.Append('.');
                }
            }
            return domainName.ToString();
        }

        private static string ReadPointedDomainName(BinaryReader reader, int offset)
        {
            long current_offset = reader.BaseStream.Position;
            reader.BaseStream.Position = offset;
            string domainName = ReadDomainName(reader);
            reader.BaseStream.Position = current_offset;
            return domainName;
        }

        #endregion

        #region Helper

        static void WriteUInt16BE(BinaryWriter writer, UInt16 value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            writer.Write(bytes[1]);
            writer.Write(bytes[0]);
        }

        static void WriteInt32BE(BinaryWriter writer, Int32 value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            writer.Write(bytes[3]);
            writer.Write(bytes[2]);
            writer.Write(bytes[1]);
            writer.Write(bytes[0]);
        }

        static UInt16 ReadUInt16BE(BinaryReader reader)
        {
            byte b1 = reader.ReadByte();
            byte b2 = reader.ReadByte();
            return (UInt16)(b1 << 8 | b2);
        }

        static Int32 ReadInt32BE(BinaryReader reader)
        {
            byte b1 = reader.ReadByte();
            byte b2 = reader.ReadByte();
            byte b3 = reader.ReadByte();
            byte b4 = reader.ReadByte();
            return b1 << 24 | b2 << 16 | b3 << 8 | b4;
        }

        #endregion
    }
}
