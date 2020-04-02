using System;
using System.Collections.Generic;
using OneSpanSign.Sdk.Internal;
using Newtonsoft.Json;

namespace OneSpanSign.Sdk
{
    internal class FieldSummaryApiClient
    {
        private string apiToken;
        private UrlTemplate template;

         public FieldSummaryApiClient (string apiToken, string baseUrl)
        {
            this.apiToken = apiToken;
            template = new UrlTemplate (baseUrl);                                                                   
        }
        
        public List<FieldSummary> GetFieldSummary (string packageId)
        {
            string path = template.UrlFor (UrlTemplate.FIELD_SUMMARY_PATH)
                            .Replace ("{packageId}", packageId)
                            .Build ();

            try {
                string response = Converter.ToString (HttpMethods.GetHttp (apiToken, path));
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

