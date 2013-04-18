using System;
using Newtonsoft.Json;

namespace CSharp_SDK
{
	public class FieldSummary
	{
		[JsonProperty("signerId")]
		public string SignerId { get; private set; }

		[JsonProperty("documentId")]
		public string DocumentId { get; private set; }

		[JsonProperty("fieldId")]
		public string FieldId { get; private set; }

		[JsonProperty("fieldValue")]
		public string FieldValue { get; private set; }

		public FieldSummary ()
		{
		}

	}
}

