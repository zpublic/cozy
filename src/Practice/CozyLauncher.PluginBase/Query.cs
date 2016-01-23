using System;

namespace CozyLauncher.PluginBase
{
    public class Query
    {
        private string _RawQuery;
        public string RawQuery
        {
            get { return _RawQuery; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _RawQuery = value.ToLower();
                }
                else
                {
                    _RawQuery = value;
                }
            }
        }
        public string Search { get; set; }
        public string[] Terms { get; set; }
        public string ActionKeyword { get; set; }

        public string SecondSearch => SplitSearch(1);
        public string ThirdSearch => SplitSearch(2);

        private string SplitSearch(int index)
        {
            try
            {
                return string.IsNullOrEmpty(ActionKeyword) ? Terms[index] : Terms[index + 1];
            }
            catch (IndexOutOfRangeException)
            {
                return string.Empty;
            }
        }
    }
}
