using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using OneSpanSign.Sdk.Internal;

namespace OneSpanSign.Sdk.Services
{
	/// <summary>
	/// The AuditService class provides a method to get the audit trail for a package.
	/// </summary>
	public class AuditService
    {
        private RestClient restClient;
        private UrlTemplate template;

        /// <summary>
        /// Initializes a new instance of the <see cref="OneSpanSign.Sdk.AuditService"/> class.
        /// </summary>
        /// <param name="apiToken">API token.</param>
        /// <param name="baseUrl">Base URL.</param>
        public AuditService (RestClient restClient, string baseUrl)
		{
            this.restClient = restClient;
            template = new UrlTemplate(baseUrl);
        }

		/// <summary>
		/// Gets the audit trail for a package and returns a list of audits.
		/// </summary>
		/// <returns>A list of audits.</returns>
		/// <param name="packageId">The package id.</param>
		public List<Audit> GetAudit (PackageId packageId)
		{
			string path = template.UrlFor (UrlTemplate.AUDIT_PATH)
				.Replace ("{packageId}", packageId.Id)
					.Build ();

			try {
                string response = restClient.Get(path);
                Dictionary<string, object> eventList = JsonConvert.DeserializeObject<Dictionary<string, object>>(response);
                if (eventList.ContainsKey ("audit-events")) {
					return JsonConvert.DeserializeObject<List<Audit>> (eventList ["audit-events"].ToString ());
				}
				return null;
			}
            catch (OssServerException e) {
                throw new OssServerException ("Could not get audit." + " Exception: " + e.Message,e.ServerError,e);
            }
            catch (Exception e) {
				throw new OssException ("Could not get audit." + " Exception: " + e.Message,e);
			}
		}

	}
}

