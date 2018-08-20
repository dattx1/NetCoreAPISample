using JWT;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using System;
using System.Reflection;

namespace Common
{
    public class RequestData
    {
        /// <summary>
        /// value of parameter data
        /// </summary>
        public string value { get; set; }


    }

    public class DecodeData<T>
    {
        /// <summary>
        /// Dynamic object deserialize to Json convert
        /// </summary>
        /// <returns></returns>
        public static T ConvertJsonToObject(RequestData data)
        {
            var obj = ApiSecurity.Decode(data.value);
            return JsonConvert.DeserializeObject<T>(obj.ToString());
        }

        public static string getLoggingData(T forParameter)
        {
            var paramDataLogging = new StringBuilder();
            Type type = forParameter.GetType();
            PropertyInfo[] properties = type.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                paramDataLogging.Append(string.Format("{0}:{1},", property.Name, property.GetValue(forParameter, null)));
            }
            return paramDataLogging.ToString();
        }
    }
}
