namespace CozyRSS.Syndication.Model
{
    public class SyndicationItem
    {
        public bool IsValid()
        {
            if (title?.Length > 0 || description?.Length > 0)
                return true;
            return false;
        }

        #region 关注的elements
        // 所有都是可选的，但是标题和描述必须有一个
        public string title { get; set; }
        public string description { get; set; }
        public string link { get; set; }
        public string pubDate { get; set; } // rfc 822格式
        #endregion

        #region 暂不关注的elements
        /*
        string author;
        string category;
        string comments;
        string enclosure;
        string guid;
        string source;
        */
        #endregion
    }
}
