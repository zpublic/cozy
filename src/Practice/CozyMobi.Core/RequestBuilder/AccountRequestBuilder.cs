using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace CozyMobi.Core.RequestBuilder
{
    class AccountRequestBuilder
    {
        public HttpContent Login(string email, string password, string j_captcha = null)
        {
            var v = new Dictionary<string, string>();
            v.Add("email", email);
            v.Add("password", RequestBuilderCommon.EncryptToSHA1(password));
            if (j_captcha != null)
            {
                v.Add("j_captcha", j_captcha);
            }
            v.Add("remember_me", "1");
            HttpContent content = new FormUrlEncodedContent(v);
            return content;
        }
    }
}
