using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Sys.Common.Utils;
using System;
using System.Collections.Generic;

namespace Sys.Common.Helper
{
    public static class JsonHelper
    {
        public static T Deserialize<T>(string input)
        {
            return JsonConvert.DeserializeObject<T>(input);
        }

        public static string Serialize<T>(IList<T> input)
        {
            return JsonConvert.SerializeObject(input);
        }

        public static string Serialize<T>(T input)
        {
            return JsonConvert.SerializeObject(input);
        }

        public static JObject ConvertStringToJson(this string input)
        {
            JObject json = JObject.Parse(input);
            return json;
        }
    }

    public class CreateShortDynamicLinkRequestConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.NullValueHandling = NullValueHandling.Ignore;
            var t = JToken.FromObject(value);
            var modified = t.RemoveFields("ETag");

            modified.WriteTo(writer);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override bool CanRead => false;
    }
}