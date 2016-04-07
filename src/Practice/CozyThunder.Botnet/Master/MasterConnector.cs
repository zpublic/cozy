using CozyThunder.Botnet.Common;
using System;
using System.Net.Sockets;
using System.Text;

namespace CozyThunder.Botnet.Master
{
    // 每一个MasterConnector作为一个tcp客户端，和一个SlavePeer进行连接
    class MasterConnector
    {
        IMasterConnectorListener listener_;
        Socket socket_;
        Peer peer_;

        public bool Connect(Peer peer, IMasterConnectorListener listener)
        {
            peer_ = peer;
            listener_ = listener;
            socket_ = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket_.BeginConnect(peer.EndPoint.Address, peer.EndPoint.Port, ConnectCallback, socket_);
            return true;
        }

        public bool DisConnect()
        {
            socket_?.Shutdown(SocketShutdown.Both);
            socket_?.Close();
            listener_?.OnDisConnect(peer_);
            return true;
        }

        public bool Send(string msg)
        {
            byte[] byteData = Encoding.ASCII.GetBytes(msg);
            socket_.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), socket_);
            return true;
        }

        void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                Socket client = (Socket)ar.AsyncState;
                client.EndConnect(ar);
                listener_?.OnConnect(peer_);

                StateObject state = new StateObject();
                state.workSocket = client;
                client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, ReadCallback, state);
            }
            catch { }
        }

        void SendCallback(IAsyncResult ar)
        {
            try
            {
                Socket client = (Socket)ar.AsyncState;
                int bytesSent = client.EndSend(ar);
            }
            catch { }
        }

        void ReadCallback(IAsyncResult ar)
        {
            StateObject state = (StateObject)ar.AsyncState;
            Socket handler = state.workSocket;
            try
            {
                int bytesRead = handler.EndReceive(ar);
                if (bytesRead > 0)
                {
                    state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));
                    String content = state.sb.ToString();
                    listener_?.OnMessage(peer_, content);
                }
                handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReadCallback), state);
            }
            catch
            {
                listener_?.OnDisConnect(peer_);
            }
        }
    }
}
