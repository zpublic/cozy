using Lidgren.Network;
using NetworkProtocol;
using System;
using System.Collections.Generic;

namespace NetworkHelper
{
    public static class MessageReader
    {
        private static Dictionary<uint, Type> IdToTypeDictionary    = new Dictionary<uint, Type>();
        private static object ObjLocker                             = new object();

        public static void RegisterType<T>(uint id)
            where T : IMessage
        {
            Type t = typeof(T);
            lock (ObjLocker)
            {
                IdToTypeDictionary[id] = t;
            }
        }

        public static void RegisterType(Type type, uint id)
        {
            lock(ObjLocker)
            {
                IdToTypeDictionary[id] = type;
            }
        }

        public static IMessage GetTypeInstanceByStream(NetIncomingMessage stream)
        {
            var id = stream.ReadUInt32();
            lock (ObjLocker)
            {
                if (IdToTypeDictionary.ContainsKey(id))
                {
                    var instance = (IMessage)Activator.CreateInstance(IdToTypeDictionary[id]);
                    instance.Read(stream);
                    return instance;
                }
                throw new KeyNotFoundException("Unknow Type");
            }
        }
    }
}