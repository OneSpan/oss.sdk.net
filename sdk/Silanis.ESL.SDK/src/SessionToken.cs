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

		/// <summary>
		/// Initializes a new instance of the <see cref="Silanis.ESL.SDK.SessionToken"/> class.
		/// </summary>
		public SessionToken ()
		{
		}
	}
}

