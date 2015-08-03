using System;
using System.Collections.Generic;
using NetworkProtocol;
using Lidgren.Network;
using System.Reflection;

namespace NetworkHelper
{
    public static class MessageCallbackInvoker
    {
        private static Dictionary<uint, Action<IMessage, NetConnection>> CallbackList 
            = new Dictionary<uint, Action<IMessage, NetConnection>>();
        private static object ObjLocker = new object();

        public static Action<IMessage, NetConnection> DefaultAction { get; set; }

        public static void LoadMessage(string Ass, string Ns)
        {
            Assembly asm = Assembly.Load(Ass);
            if (asm != null)
            {
                foreach (Type type in asm.GetTypes())
                {
                    if (type.Namespace == Ns)
                    {
                        uint id = ((IMessage)Activator.CreateInstance(type)).Id;
                        RegisterCallback(id, null);
                    }
                }
            }
        }

        public static void RegisterCallback<T>(Action<IMessage, NetConnection> callback)
            where T : IMessage, new()
        {
            var t   = new T();
            uint id = t.Id;
            lock (ObjLocker)
            {
                CallbackList[id] = callback;
            }
        }

        public static void RegisterCallback(uint id, Action<IMessage, NetConnection> callback)
        {
            lock(ObjLocker)
            {
                CallbackList[id] = callback;
            }
        }

        public static void Invoke(IMessage msg, NetConnection conn)
        {
            uint id                                 = msg.Id;
            Action<IMessage, NetConnection> action  = null;
            lock (ObjLocker)
            {
                if (CallbackList.ContainsKey(id))
                {
                    action = CallbackList[id];
                }
            }
            if(action != null)
            {
                action(msg, conn);
            }
            else
            {
                if(DefaultAction != null)
                {
                    DefaultAction(msg, conn);
                }
            }
        }
    }
}
