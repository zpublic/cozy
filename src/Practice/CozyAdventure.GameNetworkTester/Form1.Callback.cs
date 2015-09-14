using CozyAdventure.Protocol.Msg;
using CozyNetworkHelper;
using CozyNetworkProtocol;
using Lidgren.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CozyAdventure.GameNetworkTester
{
    public partial class Form1
    {
        [CallBack(typeof(RegisterResultMessage))]
        public void OnRegisterResultMessage(NetPeer client, NetBuffer buff, MessageBase msg)
        {
            var srv = client as NetClient;
            var im = buff as NetIncomingMessage;

            if (srv != null && im != null)
            {
                RegisterResultMessageImpl(srv, im, msg);
            }
        }

        private void RegisterResultMessageImpl(NetClient client, NetIncomingMessage im, MessageBase msg)
        {
            var registerMsg = msg as RegisterResultMessage;
            MessageBox.Show(registerMsg.Result);
        }

        [CallBack(typeof(LoginResultMessage))]
        public void OnLoginMessage(NetPeer client, NetBuffer buff, MessageBase msg)
        {
            var srv = client as NetClient;
            var im = buff as NetIncomingMessage;

            if (srv != null && im != null)
            {
                LoginResultMessageImpl(srv, im, msg);
            }
        }

        private void LoginResultMessageImpl(NetClient client, NetIncomingMessage im, MessageBase msg)
        {
            var loginMsg = msg as LoginResultMessage;
            MessageBox.Show(loginMsg.Result);
        }
    }
}
