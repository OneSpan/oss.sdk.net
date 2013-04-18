using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CSharp_SDK
{
	public class FieldSummaryService
	{
		private string apiToken;
		private UrlTemplate template;

		public FieldSummaryService (string apiToken, string baseUrl)
		{
			this.apiToken = apiToken;
			template = new UrlTemplate (baseUrl);
		}

		public List<FieldSummary> GetFieldSummary (PackageId packageId)
		{
			string path = template.UrlFor (UrlTemplate.FIELD_SUMMARY_PATH)
                            .Replace ("{packageId}", packageId.Id)
                            .Build ();

			try {
				string response = Converter.ToString (HttpMethods.GetHttp (apiToken, path));
				return JsonConvert.DeserializeObject<List<FieldSummary>> (response);
			} catch (Exception e) {
				throw new EslException ("Could not get the field summary.");
			}
		}
	}
}

