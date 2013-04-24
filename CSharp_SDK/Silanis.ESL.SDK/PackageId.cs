using System;
using Newtonsoft.Json;

namespace Silanis.ESL.SDK
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

