using CommonMark;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace CozyMarkDonw.Mvc.Models {

    public class ArticleModel {
        public int Id { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public DateTime CreateDate { get; set; }
        public string ImagePath { get; set; }
        public string ArticleContent { get; set; }
        public string MdToHtmlContent {
            get {
                return CommonMarkConverter.Convert(this.ArticleContent);
            }
        }

        public static List<ArticleModel> TestData() {
            return Enumerable.Range(0, 5).Select(x => new ArticleModel {
                Id = x,
                Title = string.Format("博客标题{0}", x),
                SubTitle = string.Format("副标题{0}", x),
                CreateDate = DateTime.Now,
                ArticleContent = File.ReadAllText(HttpContext.Current.Server.MapPath("~/Test.md"))
            }).ToList();
        }
    }
}
