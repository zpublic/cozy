using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyThunder.DistributedDownload.MasterGui.MessageCenter
{
    internal class GlobalMessageCenter
    {
        internal static GlobalMessageCenter Instance { get; set; } = new GlobalMessageCenter();

        internal class Message
        {
            internal MessageCallback_0 callback_0;
            internal MessageCallback_1 callback_1;
            internal MessageCallback_2 callback_2;
            internal MessageCallback_3 callback_3;

            internal delegate void MessageCallback_0();
            internal delegate void MessageCallback_1(object arg1);
            internal delegate void MessageCallback_2(object arg1, object arg2);
            internal delegate void MessageCallback_3(object arg1, object arg2, object arg3);
        }

        private Dictionary<string, Message> MessageCollection { get; set; } = new Dictionary<string, Message>();

        internal void RegistMessage(string message, Message.MessageCallback_0 callback)
        {
            if(!MessageCollection.ContainsKey(message))
            {
                Message msg = new Message();
                MessageCollection.Add(message, msg);
            }

            MessageCollection[message].callback_0 -= callback;
            MessageCollection[message].callback_0 += callback;
        }

        internal void RegistMessage(string message, Message.MessageCallback_1 callback)
        {
            if (!MessageCollection.ContainsKey(message))
            {
                Message msg = new Message();
                MessageCollection.Add(message, msg);
            }

            MessageCollection[message].callback_1 -= callback;
            MessageCollection[message].callback_1 += callback;
        }

        internal void RegistMessage(string message, Message.MessageCallback_2 callback)
        {
            if (!MessageCollection.ContainsKey(message))
            {
                Message msg = new Message();
                MessageCollection.Add(message, msg);
            }

            MessageCollection[message].callback_2 -= callback;
            MessageCollection[message].callback_2 += callback;
        }

        internal void RegistMessage(string message, Message.MessageCallback_3 callback)
        {
            if (!MessageCollection.ContainsKey(message))
            {
                Message msg = new Message();
                MessageCollection.Add(message, msg);
            }

            MessageCollection[message].callback_3 -= callback;
            MessageCollection[message].callback_3 += callback;
        }

        internal void UnregistMessage(string message, Message.MessageCallback_0 callback)
        {
            if (!MessageCollection.ContainsKey(message))
            {
                return;
            }

            MessageCollection[message].callback_0 -= callback;
        }

        internal void UnregistMessage(string message, Message.MessageCallback_1 callback)
        {
            if (!MessageCollection.ContainsKey(message))
            {
                return;
            }

            MessageCollection[message].callback_1 -= callback;
        }

        internal void UnregistMessage(string message, Message.MessageCallback_2 callback)
        {
            if (!MessageCollection.ContainsKey(message))
            {
                return;
            }

            MessageCollection[message].callback_2 -= callback;
        }

        internal void UnregistMessage(string message, Message.MessageCallback_3 callback)
        {
            if (!MessageCollection.ContainsKey(message))
            {
                return;
            }

            MessageCollection[message].callback_3 -= callback;
        }

        internal void Send(string message)
        {
            if (!MessageCollection.ContainsKey(message))
            {
                return;
            }

            Message msg = MessageCollection[message];
            if (msg == null)
            {
                return;
            }

            msg.callback_0.Invoke();
        }

        internal void Send(string message, object args)
        {
            if (!MessageCollection.ContainsKey(message))
            {
                return;
            }

            Message msg = MessageCollection[message];
            if (msg == null)
            {
                return;
            }

            msg.callback_1.Invoke(args);
        }

        internal void Send(string message, object arg1, object arg2)
        {
            if (!MessageCollection.ContainsKey(message))
            {
                return;
            }

            Message msg = MessageCollection[message];
            if (msg == null)
            {
                return;
            }

            msg.callback_2.Invoke(arg1, arg2);
        }

        internal void Send(string message, object arg1, object arg2, object arg3)
        {
            if (!MessageCollection.ContainsKey(message))
            {
                return;
            }

            Message msg = MessageCollection[message];
            if (msg == null)
            {
                return;
            }

            msg.callback_3.Invoke(arg1, arg2, arg3);
        }
    }
}
