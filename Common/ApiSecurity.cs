using JWT;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public class ApiSecurity
    {
        /// <summary>
        /// Encode before web api return json result
        /// </summary>
        /// <param name="objectResult">Any Object need to encode</param>
        /// <returns>return string json</returns>
        public static string Encode(Object objectResult)
        {
            return JsonWebToken.Encode(objectResult, Constant.SecretKey, JwtHashAlgorithm.HS256);
        }

        /// <summary>
        /// Decode before get data from api
        /// </summary>
        /// <param name="sResult">string object parameter</param>
        /// <returns>return Json Object</returns>
        public static JObject Decode(string sResult)
        {
            return JObject.Parse(JsonWebToken.Decode(sResult, Constant.SecretKey, true));
        }
    }
}
