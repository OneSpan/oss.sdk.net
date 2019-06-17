using System;
using System.Collections.Generic;

namespace Silanis.ESL.SDK
{
    internal class ReferencedDocumentConverter
    {
        public static API.ReferencedDocument ToAPI(ReferencedDocument sdkReferencedDocument) 
        {
            if (sdkReferencedDocument == null)
                return null;

            List<API.ReferencedField> apiReferencedFields = new List<API.ReferencedField>();
            foreach (ReferencedField sdkReferencedField in sdkReferencedDocument.Fields) 
            {
                apiReferencedFields.Add(ReferencedFieldConverter.ToAPI(sdkReferencedField));
            }

            API.ReferencedDocument apiReferencedDocument = new API.ReferencedDocument();
            apiReferencedDocument.DocumentId = sdkReferencedDocument.DocumentId;
            apiReferencedDocument.Fields = apiReferencedFields;
            return apiReferencedDocument;
        }

        public static ReferencedDocument ToSDK(API.ReferencedDocument apiReferencedDocument) 
        {
            if (apiReferencedDocument == null)
                return null;

            List<ReferencedField> sdkReferencedFields = new List<ReferencedField>();
            foreach (API.ReferencedField apiReferencedField in apiReferencedDocument.Fields) 
            {
                sdkReferencedFields.Add(ReferencedFieldConverter.ToSDK(apiReferencedField));
            }

            ReferencedDocument sdkReferencedDocument = new ReferencedDocument();
            sdkReferencedDocument.DocumentId = apiReferencedDocument.DocumentId;
            sdkReferencedDocument.Fields = sdkReferencedFields;
            return sdkReferencedDocument;
        }
    }
}
