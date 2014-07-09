using System;
using Newtonsoft.Json;
using Silanis.ESL.SDK.Internal;

namespace Silanis.ESL.SDK
{
    internal class ApprovalApiClient
    {
        private UrlTemplate template;
        private RestClient restClient;

        public ApprovalApiClient(RestClient restClient, string baseUrl)
        {
            this.restClient = restClient;
            template = new UrlTemplate (baseUrl);
        }        
        
        public void DeleteApproval(string packageId, string documentId, string approvalId)
        {
            string path = template.UrlFor(UrlTemplate.APPROVAL_ID_PATH)
                .Replace("{packageId}", packageId)
                    .Replace("{documentId}", documentId)
                    .Replace("{approvalId}", approvalId)
                    .Build();
            try {
                restClient.Delete(path);
            }
            catch (EslServerException e) {
                throw new EslServerException("Could not delete signature.\t" + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e) {
                throw new EslException("Could not delete signature.\t" + " Exception: " + e.Message, e);
            }
        }

    }
}

