using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace AdForm.Core
{
    public class Utility
    {
        protected Utility()
        {

        }

        public static JsonSerializerOptions GetSerializerOptions()
        {
            JsonSerializerOptions jsonSerializer = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            return jsonSerializer;
        }

        public static string Serialize(params object[] param)
        {
            try
            {
                return JsonSerializer.Serialize(param);
            }
            catch
            {
                return Newtonsoft.Json.JsonConvert.SerializeObject(param);
            }
        }

    }
}
