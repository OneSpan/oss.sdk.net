using System;
using Newtonsoft.Json;

namespace OneSpanSign.Sdk
{
	/// <summary>
	/// The package id.
	/// </summary>
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

		public override string ToString ()
		{
			return Id;
		}
	}
}