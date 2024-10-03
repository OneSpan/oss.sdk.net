using System;
using System.Collections.Generic;
using OneSpanSign.Sdk.Internal;
using Newtonsoft.Json;
using System.Data.SqlTypes;

namespace OneSpanSign.Sdk
{
    internal class FieldSummaryApiClient
    {
        private RestClient restClient;
        private string baseUrl;

        public FieldSummaryApiClient(RestClient restClient, string baseUrl)
        {
            this.restClient = restClient;
            this.baseUrl = baseUrl;
        }
        
        public List<FieldSummary> GetFieldSummary (string packageId)
        {
            string path = new UrlTemplate(baseUrl).UrlFor (UrlTemplate.FIELD_SUMMARY_PATH)
                            .Replace ("{packageId}", packageId)
                            .Build ();

            try {
                string response = restClient.Get(path);
                return JsonConvert.DeserializeObject<List<FieldSummary>> (response);
            }
            catch (OssServerException e) {
                throw new OssServerException ("Could not get the field summary." + " Exception: " + e.Message,e.ServerError,e);
            }
            catch (Exception e) {
                throw new OssException ("Could not get the field summary." + " Exception: " + e.Message,e);
            }
        }
    }
}

