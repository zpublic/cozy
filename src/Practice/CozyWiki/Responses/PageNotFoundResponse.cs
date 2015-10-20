using Nancy;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyWiki.Responses
{
    public class PageNotFoundResponse : Response
    {
        public PageNotFoundResponse()
        {
            StatusCode  = HttpStatusCode.TemporaryRedirect;
            ContentType = "text/html; charset=utf-8";

            Headers["Location"] = "/Inner/404";
        }
    }
}
