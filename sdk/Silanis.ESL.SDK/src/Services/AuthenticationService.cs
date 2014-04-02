using System;
using Silanis.ESL.SDK.Internal;
using Newtonsoft.Json;

namespace Silanis.ESL.SDK
{
    public class AuthenticationService
    {
		private RestClient restClient;
		private UrlTemplate template;

		public AuthenticationService (RestClient restClient, string baseUrl)
		{
			this.restClient = restClient;
			template = new UrlTemplate (baseUrl);
		}

		public AuthenticationToken CreateAuthenticationToken ()
		{
			string path = template.UrlFor (UrlTemplate.AUTHENTICATION_TOKEN_PATH).Build ();

			try {
				string response = restClient.Post(path, null);				
				return JsonConvert.DeserializeObject<AuthenticationToken> (response);
			} catch (Exception e) {
				throw new EslException ("Could not create an authentication token for signer." + " Exception: " + e.Message);
			}
		}
    }
}