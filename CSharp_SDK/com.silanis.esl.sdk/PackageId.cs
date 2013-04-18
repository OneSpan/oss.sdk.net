using System;
using Newtonsoft.Json;

namespace CSharp_SDK
{
	public class PackageId
	{
		[JsonProperty("id")]
		public string Id { get; private set; }

		public PackageId ()
		{
		}

		public PackageId (string Id)
		{
			this.Id = Id;
		}

	}
}

