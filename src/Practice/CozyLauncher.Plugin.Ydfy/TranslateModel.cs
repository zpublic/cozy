using Newtonsoft.Json;

namespace CozyLauncher.Plugin.Ydfy {

    public class TranslateModel {

        [JsonProperty("translation")]
        public string[] Translation { get; set; }

        [JsonProperty("query")]
        public string Query { get; set; }

        [JsonProperty("errorCode")]
        public int ErrorCode { get; set; }

    }
}

//返回格式
//{
//    "translation": [
//        "书"
//    ],
//    "basic": {
//        "us-phonetic": "bʊk",
//        "phonetic": "bʊk",
//        "uk-phonetic": "bʊk",
//        "explains": [
//            "n. 书籍；卷；帐簿；名册；工作簿",
//            "vt. 预订；登记",
//            "n. (Book)人名；(中)卜(广东话·威妥玛)；(朝)北；(英)布克；(瑞典)博克"
//        ]
//    },
//    "query": "book",
//    "errorCode": 0,
//    "web": [
//        {
//            "value": [
//                "预定",
//                "书籍",
//                "书籍"
//            ],
//            "key": "book"
//        },
//        {
//            "value": [
//                "参考书",
//                "工具书",
//                "参考工具书"
//            ],
//            "key": "reference book"
//        },
//        {
//            "value": [
//                "书籍设计",
//                "书籍设计",
//                "书籍装帧设计"
//            ],
//            "key": "Book design"
//        }
//    ]
//}
