using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GlobalApi
{
    public static class Serializer
    {
        public static string Serialize<T>(T obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static string GetHeader(string json)
        {
            JObject obj = JObject.Parse(json);
            if (obj.TryGetValue("header", out JToken? value))
            {
                return value.ToString();
            }
            return null;
        }
    }
}
