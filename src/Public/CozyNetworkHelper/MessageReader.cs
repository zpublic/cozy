using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyAdventure.Protocol;
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
            where T : IMessage, new()
        {
            var instance = new T();
            RegisterMessage(instance.Id, typeof(T));
        }

        public static void RegisterTypeWithAssembly(string Ass, string Ns)
        {
            Assembly asm = Assembly.Load(Ass);
            if (asm != null)
            {
                foreach (Type type in asm.GetTypes())
                {
                    if (type.Namespace == Ns)
                    {
                        uint id = ((IMessage)Activator.CreateInstance(type)).Id;
                        RegisterMessage(id, type);
                    }
                }
            }
        }

        public static IMessage GetMessageInstance(uint id, NetBuffer msg)
        {
            Type instanceType = null;
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
                var instance = (IMessage)Activator.CreateInstance(instanceType);
                instance.Read(msg);
                return instance;
            }
            return null;
        }
    }
}
