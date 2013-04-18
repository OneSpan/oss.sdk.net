using System;
using Newtonsoft.Json;

namespace Silanis.ESL.SDK
{
	public class SessionService
	{
		private string apiToken;
		private UrlTemplate template;

		public SessionService (string apiToken, string baseUrl)
		{
			this.apiToken = apiToken;
			template = new UrlTemplate (baseUrl);
		}

		public SessionToken CreateSessionToken (PackageId packageId, Signer signer)
		{
			string path = template.UrlFor (UrlTemplate.SESSION_PATH)
                .Replace ("{packageId}", packageId.Id)
                .Replace ("{signerId}", signer.Id)
                .Build ();

			try {
				string response = Converter.ToString (HttpMethods.PostHttp (apiToken, path, new byte[0]));
				return JsonConvert.DeserializeObject<SessionToken> (response);
			} catch (Exception e) {
				throw new EslException ("Could not create a session token for signer.");
			}
		}
	}
}

