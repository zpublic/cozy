using CozyLauncher.PluginBase;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using YAMP;

namespace CozyLauncher.Plugin.Calculator
{
    public class Main : BasePlugin
    {
        private PluginInitContext context_;
        private static Regex regValidExpressChar = new Regex(
                       @"^(" +
                       @"sin|cos|ceil|floor|exp|pi|max|min|det|arccos|abs|" +
                       @"eigval|eigvec|eig|sum|polar|plot|round|sort|real|zeta|" +
                       @"bin2dec|hex2dec|oct2dec|" +
                       @"==|~=|&&|\|\||" +
                       @"[ei]|[0-9]|[\+\-\*\/\^\., ""]|[\(\)\|\!\[\]]" +
                       @")+$", RegexOptions.Compiled);
        private static Regex regBrackets = new Regex(@"[\(\)\[\]]", RegexOptions.Compiled);
        private static ParseContext yampContext = null;

        public override PluginInfo Init(PluginInitContext context)
        {
            context_ = context;
            yampContext = Parser.PrimaryContext;
            Parser.InteractiveMode = false;
            Parser.UseScripting = false;
            var info = new PluginInfo();
            info.Keyword = "*";
            return info;
        }

        public override List<Result> Query(Query query)
        {
            if (query.RawQuery.Length <= 2          // don't affect when user only input "e" or "i" keyword
                || !regValidExpressChar.IsMatch(query.RawQuery)
                || !IsBracketComplete(query.RawQuery))
                return null;

            try
            {
                var result = yampContext.Run(query.RawQuery);

                if (result.Output != null && !string.IsNullOrEmpty(result.Result))
                {
                    var rl = new List<Result>();
                    var r = new Result();
                    r.Title = result.Result;
                    r.SubTitle = "Copy this number to the clipboard";
                    r.IcoPath = "[Res]:calc";
                    r.Score = 100;
                    r.Action = e =>
                    {
                        context_.Api.HideAndClear();
                        try
                        {
                            Clipboard.SetText(result.Result);
                            return true;
                        }
                        catch (System.Runtime.InteropServices.ExternalException)
                        {
                            return false;
                        }
                    };
                    rl.Add(r);
                    return rl;
                }
            }
            catch
            { }
            return null;
        }

        private bool IsBracketComplete(string query)
        {
            var matchs = regBrackets.Matches(query);
            var leftBracketCount = 0;
            foreach (Match match in matchs)
            {
                if (match.Value == "(" || match.Value == "[")
                {
                    leftBracketCount++;
                }
                else
                {
                    leftBracketCount--;
                }
            }
            return leftBracketCount == 0;
        }
    }
}
