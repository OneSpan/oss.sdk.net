using System;
using Newtonsoft.Json;

namespace CSharp_SDK
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

