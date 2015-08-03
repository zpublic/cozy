using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyWeixin.Core.Models {

    public class PostModel {

        public string Signature { get; set; }
        public string Msg_Signature { get; set; }
        public string Timestamp { get; set; }
        public string Nonce { get; set; }

        public string Token { get; set; }
        public string EncodingAESKey { get; set; }
        public string AppId { get; set; }
    }
}
