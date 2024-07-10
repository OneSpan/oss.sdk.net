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

            String payload = Encoding.UTF8.GetString(Convert.FromBase64String(chunks[1]));

            JObject payloadJson = JObject.Parse(payload);

            long unixEpochTime = (long)payloadJson["exp"];
            DateTime tokenExpiresAt = DateTime.UtcNow.AddSeconds(unixEpochTime);
            DateTimeOffset now = DateTimeOffset.Now;

            return now > tokenExpiresAt.Subtract(TimeSpan.FromSeconds(AccessTokenExpirationLeeway));
        }
    }
}
