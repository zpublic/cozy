using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyWallpaper.Core.json
{
    public class H
    {
        public string desc { get; set; }
        public string link { get; set; }
        public string query { get; set; }
        public int locx { get; set; }
        public int locy { get; set; }
    }

    public class Images
    {
        public string startdate { get; set; }
        public string fullstartdate { get; set; }
        public string enddate { get; set; }
        public string url { get; set; }
        public string urlbase { get; set; }
        public string copyright { get; set; }
        public string copyrightlink { get; set; }
        public bool wp { get; set; }
        public string hsh { get; set; }
        public int drk { get; set; }
        public int top { get; set; }
        public int bot { get; set; }
        public List<H> hs { get; set; }
        public List<object> msg { get; set; }
    }

    public class Tooltips
    {
        public string loading { get; set; }
        public string previous { get; set; }
        public string next { get; set; }
        public string walle { get; set; }
        public string walls { get; set; }
    }

    public class BingWallpaperObject
    {
        public List<Images> images { get; set; }
        public Tooltips tooltips { get; set; }
    }
}
