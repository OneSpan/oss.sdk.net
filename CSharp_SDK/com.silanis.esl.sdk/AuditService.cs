using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CSharp_SDK
{
	public class AuditService
	{
		private string apiToken;
		private UrlTemplate template;

		public AuditService (string apiToken, string baseUrl)
		{
			this.apiToken = apiToken;
			template = new UrlTemplate (baseUrl);
		}

		public List<Audit> GetAudit (PackageId packageId)
		{
			string path = template.UrlFor (UrlTemplate.AUDIT_PATH)
				.Replace ("{packageId}", packageId.Id)
					.Build ();

			try {
				string response = Converter.ToString (HttpMethods.GetHttp (apiToken, path));
				Dictionary<string,object> eventList = JsonConvert.DeserializeObject <Dictionary<string,object>> (response);
				if (eventList.ContainsKey ("audit-events")) {
					return JsonConvert.DeserializeObject<List<Audit>> (eventList ["audit-events"].ToString ());
				}
				return null;
			} catch (Exception e) {
				throw new EslException ("Could not get audit.");
			}
		}

	}
}

