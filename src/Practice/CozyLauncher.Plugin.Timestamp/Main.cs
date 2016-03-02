using System;
using System.Collections.Generic;
using CozyLauncher.PluginBase;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace CozyLauncher.Plugin.Timestamp
{
    public class Main : BasePlugin
    {
        private static readonly Regex regTimestamp = new Regex(@"^1\d{9}$", RegexOptions.IgnoreCase);
        PluginInitContext context_;
        public override PluginInfo Init(PluginInitContext context)
        {
            context_ = context;
            var info = new PluginInfo();
            info.Keyword = "tt";
            return info;
        }

        public static DateTime UnixTimeStampToDateTime(int unixTimeStamp)
        {
            var dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds((double)unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        public int DateTimeToUnixTimeStamp(DateTime date)
        {
            var timeSpan = (date.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0));
            return (int)timeSpan.TotalSeconds;
        }
        public override List<Result> Query(Query query)
        {
            var list = new List<Result>();
            if(!query.RawQuery.StartsWith("tt"))
            {
                return list;
            }
            var r = new Result();
            var q = query.RawQuery.Remove(0, 2);
            q = q.Trim();
            if (q.Length == 10)
            {
                var match = regTimestamp.Match(q);
                if (!match.Success)
                {
                    return list;
                }

                var timestamp = int.Parse(q);
                var date = UnixTimeStampToDateTime(timestamp).ToString();
                r.IcoPath = "[Res]:app";
                r.Title = date;
                r.SubTitle = "Copy date to clipboard";
                r.Action = e =>
                {
                    context_.Api.HideAndClear();
                    try
                    {
                        Clipboard.SetText(r.Title);
                        return true;
                    }
                    catch (System.Runtime.InteropServices.ExternalException)
                    {
                        return false;
                    }
                };
                list.Add(r);
                return list;
            }
            if (q.Length > 0)
            {
                DateTime date;
                if (DateTime.TryParse(q, out date))
                {
                    var timestamp = DateTimeToUnixTimeStamp(date).ToString();
                    r.IcoPath = "[Res]:app";
                    r.Title = timestamp;
                    r.SubTitle = "Copy unix timestamp to clipboard";
                    r.Action = e =>
                    {
                        context_.Api.HideAndClear();
                        try
                        {
                            Clipboard.SetText(r.Title);
                            return true;
                        }
                        catch (System.Runtime.InteropServices.ExternalException)
                        {
                            return false;
                        }
                    };
                    list.Add(r);
                    return list;
                }
            }

            // show current timestamp
            if (q == "")
            {
                var timestamp = DateTimeToUnixTimeStamp(DateTime.Now).ToString();
                r.IcoPath = "[Res]:app";
                r.Title = timestamp;
                r.SubTitle = "Copy unix timestamp to clipboard";
                r.Score = 90;
                r.Action = e =>
                {
                    context_.Api.HideAndClear();
                    try
                    {
                        Clipboard.SetText(r.Title);
                        return true;
                    }
                    catch (System.Runtime.InteropServices.ExternalException)
                    {
                        return false;
                    }
                };
                list.Add(r);
                return list;
            }

            return list;
        }
    }
}
