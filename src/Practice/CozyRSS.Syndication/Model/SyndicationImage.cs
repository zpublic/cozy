namespace CozyRSS.Syndication.Model
{
    public class SyndicationImage
    {
        // 必备
        public string url { get; set; }
        public string title { get; set; }
        public string link { get; set; }

        /*
        // 可选
        int width; // 最大144,默认88
        int height; // 最大400，默认31
        string description;
        */
    }
}
