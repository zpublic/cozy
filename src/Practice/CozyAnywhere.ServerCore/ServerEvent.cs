using NetworkHelper.Event;
using System;

namespace CozyAnywhere.ServerCore
{
    public partial class AnywhereServer
    {
        public void InitServerEvent()
        {
            if (client != null)
            {
                client.StatusMessage    += new EventHandler<StatusMessageArgs>(OnStatusMessage);
                client.DataMessage      += new EventHandler<DataMessageArgs>(OnDataMessage);
                client.InternalMessage  += new EventHandler<InternalMessageArgs>(OnInternalMessage);
            }
        }

        private void OnStatusMessage(object sender, StatusMessageArgs msg)
        {
        }

        private void OnDataMessage(object sender, DataMessageArgs msg)
        {
        }

        private void OnInternalMessage(object sender, InternalMessageArgs msg)
        {
        }
    }
}