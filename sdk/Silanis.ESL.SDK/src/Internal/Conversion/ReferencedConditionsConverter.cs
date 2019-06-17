using System;
using System.Collections.Generic;

namespace Silanis.ESL.SDK
{
    internal class ReferencedConditionsConverter
    {
        public static API.ReferencedConditions ToAPI(ReferencedConditions sdkReferencedConditions) 
        {
            if (sdkReferencedConditions == null)
                return null;

            List<API.ReferencedDocument> apiReferencedDocuments = new List<API.ReferencedDocument>();
            foreach (ReferencedDocument sdkReferencedDocument in sdkReferencedConditions.Documents) 
            {
                apiReferencedDocuments.Add(ReferencedDocumentConverter.ToAPI(sdkReferencedDocument));
            }

            API.ReferencedConditions apiReferencedConditions = new API.ReferencedConditions();
            apiReferencedConditions.PackageId = sdkReferencedConditions.PackageId;
            apiReferencedConditions.Documents = apiReferencedDocuments;
            return apiReferencedConditions;
        }

        public static ReferencedConditions ToSDK(API.ReferencedConditions apiReferencedConditions) 
        {
            if (apiReferencedConditions == null)
                return null;

            List<ReferencedDocument> sdkReferencedDocuments = new List<ReferencedDocument>();
            foreach (API.ReferencedDocument apiReferenceddocument in apiReferencedConditions.Documents) 
            {
                sdkReferencedDocuments.Add(ReferencedDocumentConverter.ToSDK(apiReferenceddocument));
            }

            ReferencedConditions sdkReferencedConditions = new ReferencedConditions();
            sdkReferencedConditions.PackageId = apiReferencedConditions.PackageId;
            sdkReferencedConditions.Documents = sdkReferencedDocuments;
            return sdkReferencedConditions;
        }
    }
}
