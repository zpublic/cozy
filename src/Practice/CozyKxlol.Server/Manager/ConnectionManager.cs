using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lidgren.Network;

namespace CozyKxlol.Server.Manager
{
    public class ConnectionManager
    {
        private Dictionary<NetConnection, uint> ConnectionDictionary = new Dictionary<NetConnection, uint>();
        public void Add(NetConnection conn, uint id = 0)
        {
            ConnectionDictionary[conn] = id;
        }

        public uint Get(NetConnection conn)
        {
            if(ConnectionDictionary.ContainsKey(conn))
            {
                return ConnectionDictionary[conn];
            }
            return 0;
        }

        public void Remove(NetConnection conn)
        {
            if (ConnectionDictionary.ContainsKey(conn))
            {
                ConnectionDictionary.Remove(conn);
            }
        }

        public void Modify(NetConnection conn, uint id)
        {
            if(ConnectionDictionary.ContainsKey(conn))
            {
                ConnectionDictionary[conn] = id;
            }
        }

        public NetConnection Get(uint id)
        {
            return ConnectionDictionary.First(obj => obj.Value == id).Key;
        }
    }
}
