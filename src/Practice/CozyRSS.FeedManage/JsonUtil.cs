using Newtonsoft.Json;

namespace CozyRSS.FeedManage
{
    class JsonUtil
    {
        public static FeedCategory Json2Obj(string json)
        {
            return JsonConvert.DeserializeObject<FeedCategory>(json);
        }

        public static string Obj2Json(FeedCategory root)
        {
            return JsonConvert.SerializeObject(root, Formatting.Indented);
        }
    }
}
