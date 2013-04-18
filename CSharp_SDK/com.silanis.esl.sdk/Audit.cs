using System;
using Newtonsoft.Json;

namespace CSharp_SDK
{
	public class Audit
	{
		[JsonProperty("type")]
		public string type { get; private set; }
		[JsonProperty("date-time")]
		public string dateTime { get; private set; }
		[JsonProperty("target")]
		public string target { get; private set; }
		[JsonProperty("user")]
		public string user { get; private set; }
		[JsonProperty("user-email")]
		public string email { get; private set; }
		[JsonProperty("user-ip")]
		public string ip { get; private set; }
		[JsonProperty("data")]
		public string data { get; private set; }

		public Audit (string type, string dateTime, string target, string user, string email, string ip, string data)
		{
			this.type = type;
			this.dateTime = dateTime;
			this.target = target;
			this.user = user;
			this.email = email;
			this.ip = ip;
			this.data = data;
		}
	

	}
}

