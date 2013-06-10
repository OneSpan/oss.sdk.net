using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Silanis.ESL.API;

namespace Silanis.ESL.SDK.Internal
{
	/// <summary>
	/// For internal use.
	/// </summary>
	public class RoleList
	{
		[JsonProperty("results")]
		public List<Role> Roles { get; private set; }

		[JsonProperty("count")]
		public int Count { get; private set; }

	}
}

