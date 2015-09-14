using CozyNetworkProtocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lidgren.Network;
using System.Reflection;

namespace CozyNetworkHelper
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class CallBackAttribute : Attribute
    {
        public CallBackAttribute(Type MsgType)
        {
            var instance = (MessageBase)Activator.CreateInstance(MsgType);
            if (instance != null)
            {
                msgId = instance.Id;
            }
        }

        private uint msgId;
        public uint MsgId
        {
            get
            {
                return msgId;
            }
        }
    }

    public static class MessageCallbackManager
    {
        public delegate void CallbackAction(NetPeer peer, NetBuffer buff, MessageBase msg);

        private static Dictionary<uint, CallbackAction> CallbackMapping 
            = new Dictionary<uint, CallbackAction>();

        public static void RegisterCallback(object res)
        {
            var type = res.GetType();
            var methods = type.GetMethods();
            foreach(var method in methods)
            {
                var att = method.GetCustomAttribute<CallBackAttribute>();
                if (att != null)
                {
                    CallbackMapping[att.MsgId] = (CallbackAction)method.CreateDelegate(typeof(CallbackAction), res);
                }
            }
        }

        public static void ShellCallback(MessageBase msg, NetPeer peer, NetBuffer buff)
        {
            if(CallbackMapping.ContainsKey(msg.Id))
            {
                CallbackMapping[msg.Id](peer, buff, msg);
            }
        }
    }
}
