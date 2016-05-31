using CozyLauncher.PluginBase;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace CozyLauncher.Plugin.Express
{
    /// <summary>
    /// 快递查询
    /// </summary>
    public class Main : BasePlugin
    {
        private PluginInitContext context_;

        //private const string __UID = "66900";

        //private const string __KEY = "83967ace18fe4a96acbc349731913f92";

        //private const string __URL = @"http://www.kuaidiapi.cn/rest/?";

        //private string __PARAM = @"uid=" + __UID + "&key=" + __KEY;

        //private string __TEST = @"uid=66900&key=83967ace18fe4a96acbc349731913f92&order=881754461664541778&id=yuantong";

        private const string __URL = "http://api.open.baidu.com/pae/channel/data/asyncqury?appid=4001";

        private const string __KEYWORD = "kdcx";

        private const string __TIPS = @"请输入快递缩写,例如顺丰:shunfeng+空格单号";

        /// <summary>
        /// 快递信息
        /// </summary>
        private List<ExpressDataInfo> m_listInfo = new List<ExpressDataInfo>();

        public override PluginInfo Init(PluginInitContext context)
        {
            this.context_ = context;
            var info = new PluginInfo();
            info.Keyword = __KEYWORD;

            // init Info
            string _filePath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "/Resources/ExpressInfo.json";

            if (File.Exists(_filePath))
            {
                string _txtStr = File.ReadAllText(_filePath);
                object _object = JsonConvert.DeserializeObject<List<ExpressDataInfo>>(_txtStr);
                m_listInfo = _object as List<ExpressDataInfo>;
            }

            return info;
        }

        public override List<Result> Query(Query query)
        {
            if (query.RawQuery.StartsWith(__KEYWORD + " "))
            {
                List<Result> _list = new List<Result>();

                // 判断快递名称
                var _str = query.RawQuery.Substring(__KEYWORD.Length + 1);

                // 需要查询的信息
                string[] _info = _str.Split(' ');

                if (_info.Length < 2 ||
                    _info[1].Equals(""))
                {
                    _list.Add(new Result
                    {
                        Title = __TIPS,
                        SubTitle = "",
                        IcoPath = "txt",
                        Score = 20,
                        Action = e =>
                        {
                            return true;
                        }
                    });

                    return _list;
                }
                else
                {
                    // 查询快递id
                    string _id = _info[0];

                    // 快递公司名
                    string _expressName = "未知快递";

                    foreach (ExpressDataInfo i in m_listInfo)
                    {
                        if (_id.Equals(i.Key))
                        {
                            _id = i.Key;
                            _expressName = i.Name;

                            break;
                        }
                    }

                    // 单号
                    string _order = _info[1];

                    // 开始查询
                    if (_order.Length > 0)
                    {
                        _list.Add(new Result
                        {
                            Title = "查询" + _expressName + "_单号:" + _order,
                            SubTitle = "点击开始查询",
                            IcoPath = "txt",
                            Score = 20,
                            Action = e =>
                            {
                                QueryExpress(_id, _order);

                                return true;
                            }
                        });
                    }

                    return _list;
                }
            }

            if (query.RawQuery.StartsWith("k"))
            {
                return new List<Result> {  new Result
                                            {
                                                Title = __TIPS,
                                                SubTitle = "",
                                                IcoPath = "txt",
                                                Score = 80,
                                                Action = e =>
                                                {
                                                    return true;
                                                }
                                            }};
            }

            return null;
        }

        /// <summary>
        /// 查询快递
        /// </summary>
        /// <param name="_id">快递id</param>
        /// <param name="_order">单号</param>
        /// <returns></returns>
        private List<Result> QueryExpress(string _id, string _order)
        {
            List<Result> _list = new List<Result>();

            HttpClientHandler _handler = new HttpClientHandler();

            // 这里为false表示不采用HttpClient的默认Cookie,而是采用httpRequestmessage的Cookie
            _handler.UseCookies = false;

            using (var _client = new HttpClient(_handler))
            {
                string _cookie = GetCookie(_id, _order, _client);

                var request = new HttpRequestMessage(HttpMethod.Get,
                                                   __URL +
                                                   //__PARAM +
                                                   //@"&order=" + _order +
                                                   //@"&id=" + _id);
                                                   @"&com=" + _id +
                                                   @"&nu=" + _order +
                                                   @"&qq-pf-to=pcqq.c2c");

                request.Headers.Add("Cookie", _cookie);

                HttpResponseMessage respones = _client.SendAsync(request).Result;

                var jsonString = respones.Content.ReadAsStringAsync().Result;

                var _expressModel = JsonConvert.DeserializeObject<ExpressModel>(jsonString);

                // 是否是错误
                if (_expressModel.Status == 0)
                {
                    // 第一条状态信息
                    Result _result = new Result
                    {
                        Title = "快递状态:" + _expressModel.Data.Info.GetStatus(),
                        SubTitle = "Copy this text to the clipboard",
                        IcoPath = "txt",
                        Score = 20,
                        Action = e =>
                        {
                            context_.Api.HideAndClear();

                            try
                            {
                                return false;
                            }
                            catch (System.Runtime.InteropServices.ExternalException)
                            {
                                return false;
                            }
                        }
                    };

                    _list.Add(_result);

                    // 表单
                    //for (int i = _expressModel.Data.Info.Context.Length - 1; i > 0; --i)
                    for (int i = 0; i < _expressModel.Data.Info.Context.Length; ++i)
                    {
                        ExpressContext _data = _expressModel.Data.Info.Context[i];

                        _result = new Result
                        {
                            Title = _data.Time() + "_" + _data.Desc,
                            SubTitle = "Copy this text to the clipboard",
                            IcoPath = "txt",
                            Score = 20,
                            Action = e =>
                            {
                                context_.Api.HideAndClear();

                                try
                                {
                                    Clipboard.SetText(_data.Time() + "_" + _data.Desc);

                                    return true;
                                }
                                catch (System.Runtime.InteropServices.ExternalException)
                                {
                                    return false;
                                }
                            }
                        };

                        _list.Add(_result);
                    }
                }
                else
                {
                    Result _result = new Result
                    {
                        Title = "查询出现了错误:" + _expressModel.Message,
                        SubTitle = "Copy this text to the clipboard",
                        IcoPath = "txt",
                        Score = 20,
                        Action = e =>
                        {
                            context_.Api.HideAndClear();

                            try
                            {
                                Clipboard.SetText(_expressModel.Message);

                                return true;
                            }
                            catch (System.Runtime.InteropServices.ExternalException)
                            {
                                return false;
                            }
                        }
                    };

                    _list.Add(_result);
                }

                context_.Api.PushResults(_list);

                return _list;
            }
        }

        private static string GetCookie(string _id, string _order, HttpClient _client)
        {
            var _msg = new HttpRequestMessage(HttpMethod.Get,
                                              __URL +
                                              @"&com=" + _id +
                                              @"&nu=" + _order +
                                              @"&qq-pf-to=pcqq.c2c");

            HttpResponseMessage _respones = _client.SendAsync(_msg).Result;

            IEnumerable<string> _cookies = _respones.Headers.GetValues("Set-Cookie");

            string _cookie = "";

            foreach (string _str in _cookies)
            {
                _cookie = GetCookieValue(_str);

                break;
            }

            return _cookie;
        }

        private static string GetCookieValue(string _cookie)
        {
            Regex _regex = new Regex(".*?;");
            Match _value = _regex.Match(_cookie);
            string _cookieValue = _value.Groups[0].Value;

            return _cookieValue.Substring(0, _cookieValue.Length - 1);
        }
    }
}