using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CozyKxlol.Network
{
    public enum ConnectionStatus
    {
        None = 0,
        InitiatedConnect = 1,
        ReceivedInitiation = 2,
        RespondedAwaitingApproval = 3,
        RespondedConnect = 4,
        Connected = 5,
        Disconnecting = 6,
        Disconnected = 7,
    }
}
