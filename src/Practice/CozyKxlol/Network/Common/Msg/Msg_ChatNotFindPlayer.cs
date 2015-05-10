using Microsoft.Xna.Framework.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CozyKxlol.Network.Common.Msg
{
    public class Msg_ChatNotFindPlayer : MsgBase
    {
        public Msg_ChatNotFindPlayer()
            : base(MsgId.ChatNotFindPlayer)
        {
        }
    }
}
