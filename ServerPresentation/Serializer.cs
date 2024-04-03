using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerPresentation
{
    internal class Serializer
    {
        public static string Serialize<T>(T obj)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }

        public static T Deserialize<T>(string json)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
        }

        public static string GetCommandHeader(string json)
        {
            JObject jObject = JObject.Parse(json);
            if (jObject.ContainsKey("Header"))
            {
                return (string)jObject["Header"];
            }
            return null;
        }
    }
}
