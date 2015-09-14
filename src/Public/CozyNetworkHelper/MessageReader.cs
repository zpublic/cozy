using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyNetworkProtocol;
using System.Reflection;
using Lidgren.Network;

namespace CozyNetworkHelper
{
    public static class MessageReader
    {
        private static Dictionary<uint, Type> MessageMapping = new Dictionary<uint, Type>();
        private static object objLocker = new object();

        public static void RegisterMessage(uint id, Type msg)
        {
            lock (objLocker)
            {
                if (MessageMapping.ContainsKey(id))
                {
                    throw new Exception("message is already register");
                }
                MessageMapping[id] = msg;
            }
        }

        public static void RegisterMessage<T>()
            where T : MessageBase, new()
        {
            var instance = new T();
            RegisterMessage(instance.Id, typeof(T));
        }

        public static void RegisterTypeWithAssembly(string Ass)
        {
            Assembly asm = Assembly.Load(Ass);
            if (asm != null)
            {
                foreach (Type type in asm.GetTypes())
                {
                    if (type.BaseType == typeof(MessageBase))
                    {
                        uint id = ((MessageBase)Activator.CreateInstance(type)).Id;
                        RegisterMessage(id, type);
                    }
                }
            }
        }

        public static MessageBase GetMessageInstance(NetBuffer msg)
        {
            Type instanceType   = null;
            uint id             = msg.ReadUInt32();

            lock (objLocker)
            {
                if (!MessageMapping.ContainsKey(id))
                {
                    throw new KeyNotFoundException("unknow message");
                }
                instanceType = MessageMapping[id];
            }

            if (instanceType != null)
            {
                var instance = (MessageBase)Activator.CreateInstance(instanceType);
                instance.Read(msg);
                return instance;
            }
            return null;
        }
    }
}
