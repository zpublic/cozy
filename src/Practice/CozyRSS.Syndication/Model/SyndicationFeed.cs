using System.Collections.Generic;

namespace CozyRSS.Syndication.Model
{
    public class SyndicationFeed
    {
        public bool IsValid()
        {
            if (title?.Length > 0 && description?.Length > 0 && link?.Length > 0)
                return true;
            return false;
        }

        #region 关注的elements
        // 必备
        public string title;
        public string description;
        public string link;

        // 可选
        public SyndicationImage image;
        public string pubDate; // rfc 822格式
        public string lastBuildDate;
        public List<SyndicationItem> items = new List<SyndicationItem>();
        #endregion

        #region 暂不关注的elements
        /*
        string language;
        string copyright;
        string managingEditor;
        string webMaster;
        string category;
        string generator;
        string docs;
        string cloud; // 列表
        string ttl;
        string rating;
        string textInput; // 列表
        string skipHours;
        string skipDays;
        */
        #endregion
    }
}
