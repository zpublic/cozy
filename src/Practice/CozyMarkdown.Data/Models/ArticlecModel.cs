using System;

namespace CozyMarkdown.Data.Models {

    public class ArticlecModel : BaseModel {

        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }
        public bool IsDraft { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
