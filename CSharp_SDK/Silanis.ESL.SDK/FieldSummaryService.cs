using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Silanis.ESL.SDK
{
	/// <summary>
	/// The FieldSummaryService class provides a method to get the field summary for a package.
	/// </summary>
	public class FieldSummaryService
	{
		private string apiToken;
		private UrlTemplate template;

		/// <summary>
		/// Initializes a new instance of the <see cref="Silanis.ESL.SDK.FieldSummaryService"/> class.
		/// </summary>
		/// <param name="apiToken">API token.</param>
		/// <param name="baseUrl">Base URL.</param>
		public FieldSummaryService (string apiToken, string baseUrl)
		{
			this.apiToken = apiToken;
			template = new UrlTemplate (baseUrl);
		}

		/// <summary>
		/// Gets the field summary for the package and returns a list of field summaries.
		/// </summary>
		/// <returns>A list of field summaries.</returns>
		/// <param name="packageId">The package id.</param>
		public List<FieldSummary> GetFieldSummary (PackageId packageId)
		{
			string path = template.UrlFor (UrlTemplate.FIELD_SUMMARY_PATH)
                            .Replace ("{packageId}", packageId.Id)
                            .Build ();

			try {
				string response = Converter.ToString (HttpMethods.GetHttp (apiToken, path));
				return JsonConvert.DeserializeObject<List<FieldSummary>> (response);
			} catch (Exception e) {
				throw new EslException ("Could not get the field summary." + " Exception: " + e.Message);
			}
		}
	}
}

