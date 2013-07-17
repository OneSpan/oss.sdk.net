using System;
using Newtonsoft.Json;

namespace Silanis.ESL.SDK
{
	/// <summary>
	/// The session token for a signer.
	/// </summary>
	public class SessionToken
	{
		[JsonProperty("sessionToken")]
		public string Token { get; private set; }
	}
}