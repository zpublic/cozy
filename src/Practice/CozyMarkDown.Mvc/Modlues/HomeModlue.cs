using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy;

namespace CozyMarkDown.Mvc.Modlues {

    public class HomeModlue : NancyModule {

        public HomeModlue() {
            

            Get["/"] = x => {
                return View["/Index"];
            };
        }
    }
}
