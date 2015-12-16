using Microsoft.AspNet.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CozyAnonymous.WebSite.Chat
{
    public class ChatEndPoint : PersistentConnection
    {
        protected override Task OnConnected(IRequest request, string connectionId)
        {
            return Connection.Broadcast("Connection " + connectionId + " connected");
        }

        protected override Task OnReconnected(IRequest request, string connectionId)
        {
            return Connection.Broadcast("Client " + connectionId + " re-connected");
        }

        protected override IList<string> OnRejoiningGroups(IRequest request, IList<string> groups, string connectionId)
        {
            return groups;
        }

        protected override Task OnReceived(IRequest request, string connectionId, string data)
        {
            Connection.Send(connectionId, "hehe");
            return Connection.Broadcast("Connection " + connectionId + " sent " + data);
        }

        protected override Task OnDisconnected(IRequest request, string connectionId, bool stopCalled)
        {
            return Connection.Broadcast("Connection " + connectionId + " disconncted");
        }
    }
}