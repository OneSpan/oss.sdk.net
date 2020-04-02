using System;
using System.Web;
using Newtonsoft.Json;
using OneSpanSign.Sdk.Internal;

namespace OneSpanSign.Sdk.Services
{
	/// <summary>
	/// The SessionService class provides a method to create a session token for a signer.
	/// </summary>
	public class SessionService
	{
		private string apiToken;
		private UrlTemplate template;
		private AuthenticationTokenService authenticationService;

		/// <summary>
		/// Initializes a new instance of the <see cref="OneSpanSign.Sdk.SessionService"/> class.
		/// </summary>
		/// <param name="apiToken">API token.</param>
		/// <param name="baseUrl">Base URL.</param>
		public SessionService (string apiToken, string baseUrl)
		{
			this.apiToken = apiToken;
			template = new UrlTemplate (baseUrl);
			authenticationService = new AuthenticationTokenService(new RestClient(apiToken), baseUrl);

		}

		public SessionToken CreateSessionToken (PackageId packageId, string signerId)
		{
			return CreateSignerSessionToken(packageId, signerId);
		}

		[Obsolete("Call AuthenticationTokenService.CreateSenderAuthenticationToken() instead.")]
		public SessionToken CreateSenderSessionToken()
		{
			AuthenticationToken token = authenticationService.CreateAuthenticationToken();

			return new SessionToken(token.Token);
		}

		/// <summary>
		/// Creates a session token for a signer and returns the session token.
		/// </summary>
		/// <returns>The session token for signer.</returns>
		/// <param name="packageId">The package id.</param>
		/// <param name="signer">The signer to create a session token for.</param>
		public SessionToken CreateSignerSessionToken (PackageId packageId, string signerId)
		{

			string path = template.UrlFor (UrlTemplate.SESSION_PATH)
                .Replace ("{packageId}", packageId.Id)
                .Replace ("{signerId}", HttpUtility.UrlEncode(signerId))
                .Build ();

			try {
				string response = Converter.ToString (HttpMethods.PostHttp (apiToken, path, new byte[0]));
				return JsonConvert.DeserializeObject<SessionToken> (response);
            }
            catch (OssServerException e) {
                throw new OssServerException ("Could not create a session token for signer." + " Exception: " + e.Message, e.ServerError, e);
            } 
            catch (Exception e) {
				throw new OssException ("Could not create a session token for signer." + " Exception: " + e.Message, e);
			}
		}
	}
}

