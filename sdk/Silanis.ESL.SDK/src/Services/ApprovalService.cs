using System;
using Silanis.ESL.SDK.Internal;
using Newtonsoft.Json;

namespace Silanis.ESL.SDK
{
    public class ApprovalService
    {
        private ApprovalApiClient apiClient;
        
        internal ApprovalService(ApprovalApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public void DeleteApproval(PackageId packageId, string documentId, string approvalId){
        
            apiClient.DeleteApproval( packageId.Id, documentId, approvalId );
        }
    }
}

