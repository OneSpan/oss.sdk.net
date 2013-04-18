using System;
using Newtonsoft.Json;

namespace Silanis.ESL.SDK
{
	public class SessionToken
	{
		[JsonProperty("sessionToken")]
		public string Token { get; private set; }

		public SessionToken ()
		{
		}
	}
}

