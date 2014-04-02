using System;
using Newtonsoft.Json;

namespace Silanis.ESL.SDK
{
	/// <summary>
	/// An authentication token for a user.
	/// </summary>
    public class AuthenticationToken
    {
		[JsonProperty("value")]
		public string Token { get; private set; }

		public override string ToString ()
		{
			return Token;
		}
    }
}