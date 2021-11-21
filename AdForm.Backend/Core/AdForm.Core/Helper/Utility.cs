using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace AdForm.Core
{
    public class Utility
    {
        public static T Deserialize<T>(string input)
        {
            return JsonSerializer.Deserialize<T>(input);
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

        public static string Serialize(MethodBase methodBase, params object[] param)
        {
            StringBuilder json = new StringBuilder("{ ");
            if (methodBase != null)
            {
                var props = methodBase.GetParameters();
                for (var i = 0; i < props?.Length; i++)
                {
                    if (i != 0)
                        //join all the param values using comma
                        json.Append(", ");

                    var value = props[i].ParameterType.IsClass ? Serialize(param[i]) :
                        param[i].ToString();
                    json.Append($"{props[i].Name} : {value}");
                }
            }
            json.Append(" }");
            return json.ToString();
        }

        public static double GetElapsedMilliseconds(long start, long stop)
        {
            return (stop - start) * 1000 / (double)Stopwatch.Frequency;
        }
    }
}
