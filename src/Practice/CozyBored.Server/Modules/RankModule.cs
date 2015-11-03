using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy;
using Nancy.ModelBinding;

namespace CozyBored.Server.Modules {

    public class RankModule : NancyModule {

        public RankModule() {

            Get["query-rank"] = param => {
                throw new NotImplementedException();
            };

            Post["save"] = param => {
                throw new NotImplementedException();
            };
        }

    }
}
