using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyWifi.Operator
{
    public interface IWifiOperator
    {
        void StartWifi();
        void StopWifi();
        void SetWifiProperty(string name, string password);
        bool WifiStateQuery();
    }
}
