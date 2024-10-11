using System;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OneSpanSign.Sdk.Oauth
{
    public class Oauth2TokenManager
    {

        public const int AccessTokenExpirationLeeway = 10;

        private JsonSerializer objectMapper = new JsonSerializer();


        public bool IsOAuth2TokenExpired(string oAuthAccessToken)
        {
            String[] chunks = oAuthAccessToken.Split('.');

            string base64Token = chunks[1];

            // Add padding if necessary
            switch (base64Token.Length % 4)
            {
                case 2: base64Token += "=="; break;
                case 3: base64Token += "="; break;
            }

            String payload = Encoding.UTF8.GetString(Convert.FromBase64String(base64Token));

            JObject payloadJson = JObject.Parse(payload);

            long unixEpochTime = (long)payloadJson["exp"];
            DateTime tokenExpiresAt = DateTime.UtcNow.AddSeconds(unixEpochTime);
            DateTimeOffset now = DateTimeOffset.Now;

            return now > tokenExpiresAt.Subtract(TimeSpan.FromSeconds(AccessTokenExpirationLeeway));
        }
    }
}
