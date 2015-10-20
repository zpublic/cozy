using Nancy;
using Nancy.ErrorHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CozyWiki.Responses;
using System.IO;

namespace CozyWiki
{
    public class Cozy404Handler : IStatusCodeHandler
    {
        public bool HandlesStatusCode(HttpStatusCode statusCode, NancyContext context)
        {
            return statusCode == HttpStatusCode.NotFound;
        }

        public void Handle(HttpStatusCode statusCode, NancyContext context)
        {
            var response = new PageNotFoundResponse();
        }
    }
}
