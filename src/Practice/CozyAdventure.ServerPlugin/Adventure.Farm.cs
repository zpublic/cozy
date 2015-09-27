using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Lidgren.Network;
using CozyAdventure.ServerPlugin.Model;
using CozyAdventure.Protocol.Msg;
using CozyNetworkHelper;

namespace CozyAdventure.ServerPlugin
{
    public partial class AdventurePlugin
    {
        private Timer FarmTimer { get; set; } = new Timer();

        private Dictionary<NetConnection, FarmStorage> FarmList { get; set; } = new Dictionary<NetConnection, FarmStorage>();
        private readonly object Locker = new object();

        private void InitFarm()
        {
            FarmTimer.Elapsed += new ElapsedEventHandler(OnFarmEvent);
            FarmTimer.Interval = 10000;
        }

        public bool AddFarmObj(NetConnection conn, int money, int exp)
        {
            lock(Locker)
            {
                if (FarmList.ContainsKey(conn)) return false;
                FarmList[conn] = new FarmStorage()
                {
                    CurrTime    = DateTime.Now,
                    Exp         = exp,
                    Money       = money,
                };
                if (!FarmTimer.Enabled) FarmTimer.Enabled = true;
            }
            return true;
        }

        public bool RemoveFarmObj(NetConnection conn)
        {
            var now = DateTime.Now;
            lock (Locker)
            {
                if (FarmList.ContainsKey(conn))
                {
                    var obj = FarmList[conn];

                    var e = obj.Exp * (int)(now - obj.CurrTime).TotalSeconds;
                    var m = obj.Money * (int)(now - obj.CurrTime).TotalSeconds;
                    SendFarmIncomeMsg(conn, e, m);
                    FarmList.Remove(conn);
                    if (FarmList.Count == 0)
                    {
                        FarmTimer.Enabled = false;
                    }
                }
                else
                {
                    return false;
                }
            }
            return true;
        }


        private void OnFarmEvent(object sender, ElapsedEventArgs msg)
        {
            var now = DateTime.Now;
            lock (Locker)
            {
                foreach(var obj in FarmList)
                {
                    var e = obj.Value.Exp * (int)(now - obj.Value.CurrTime).TotalSeconds;
                    var m = obj.Value.Money * (int)(now - obj.Value.CurrTime).TotalSeconds;

                    SendFarmIncomeMsg(obj.Key, e, m);
                    obj.Value.CurrTime = now;
                }
            }
        }

        private void SendFarmIncomeMsg(NetConnection conn, int exp, int money)
        {
            var msg = new FarmIncomeMessage()
            {
                Exp     = exp,
                Money   = money,
            };
            SharedServer.SendMessage(msg, conn);
        }
    }
}
