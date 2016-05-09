using Newtonsoft.Json;

namespace CozyRSS.FeedManage
{
    public class JsonUtil
    {
        public static FeedCategory Json2Obj(string json)
        {
            return JsonConvert.DeserializeObject<FeedCategory>(json);
        }

        public static string Obj2Json(FeedCategory root)
        {
            return JsonConvert.SerializeObject(root);
        }
    }
}
