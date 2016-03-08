using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CozyLauncher.Plugin.DailySentence
{
    /*
    {
        "sid": "1881",
        "tts": "http://news.iciba.com/admin/tts/2016-03-08-day.mp3",
        "content": "It’s better to be alone than to be with someone you’re not happy to be with.",
        "note": "宁愿一个人呆着，也不要跟不合拍的人呆一块。",
        "love": "1592",
        "translation": "词霸小编：很多同学不知道怎么进行投稿。小编再跟大家说一下吧~编辑你想投稿的内容（一句话+这句话的中文翻译+你的昵称）发送到hiciba@wps.cn小编会挑选精彩的投稿推送到每日一句跟大家分享哦~如果小伙伴们对词霸有什么建议和想法也可以发送邮件到这个邮箱，你的每一个建议对我们来说都是一种鼓励，所以大家快快行动起来吧~",
        "picture": "http://cdn.iciba.com/news/word/2016-03-08.jpg",
        "picture2": "http://cdn.iciba.com/news/word/big_2016-03-08b.jpg",
        "caption": "词霸每日一句",
        "dateline": "2016-03-08",
        "s_pv": "3187",
        "sp_pv": "62",
        "tags": [
            {
                "id": "25",
                "name": "友情"
            },
            {
                "id": "33",
                "name": "人生感悟"
            }
        ],
        "fenxiang_img": "http://cdn.iciba.com/web/news/longweibo/imag/2016-03-08.jpg"
    }
    */
    public class DailySentenceModel
    {
        public string content;
        public string note;
    }
}
