using System;
using OneSpanSign.API;
using OneSpanSign.Sdk.Builder;

namespace OneSpanSign.Sdk
{
    internal class DocumentConverter
    {
        private Document sdkDocument;
        private OneSpanSign.API.Document apiDocument;
        private OneSpanSign.API.Package apiPackage;
        /*
         * Construct with API objects
         */
        public DocumentConverter(OneSpanSign.API.Document apiDocument, OneSpanSign.API.Package apiPackage)
        {
            this.apiDocument = apiDocument;
            this.apiPackage = apiPackage;
        }
        /*
         * Construct with SDK object
         */
        public DocumentConverter(Document sdkDocument)
        {
            this.sdkDocument = sdkDocument;
        }

        internal Document ToSDKDocument()
        {
            if (apiDocument == null)
            {
                return sdkDocument;
            }

            DocumentBuilder documentBuilder = DocumentBuilder.NewDocumentNamed(apiDocument.Name)
                .WithId(apiDocument.Id)
                .AtIndex(apiDocument.Index.Value)
                .WithDescription(apiDocument.Description)
                .WithData(apiDocument.Data);
            documentBuilder.WithExternal(new ExternalConverter(apiDocument.External).ToSDKExternal());
            foreach(string extractionType in apiDocument.ExtractionTypes) {
                documentBuilder.WithExtractionType((ExtractionType)Enum.Parse(typeof(ExtractionType), extractionType));
            }
            foreach (Approval apiApproval in apiDocument.Approvals)
            {
                documentBuilder.WithSignature(new SignatureConverter(apiApproval, apiPackage).ToSDKSignature());
            }

            foreach (OneSpanSign.API.Field apiField in apiDocument.Fields)
            {
                Field sdkField = new FieldConverter(apiField).ToSDKField();
                if (!FieldStyle.BOUND_QRCODE.getApiValue().Equals(apiField.Subtype))
                {
                    documentBuilder.WithInjectedField(sdkField);
                }
                else
                {
                    documentBuilder.WithQRCode(sdkField);
                }
            }

            Document document = documentBuilder.Build();
            if ( apiDocument.Pages != null && apiDocument.Pages.Count > 0 ) 
            {
                document.NumberOfPages = apiDocument.Pages.Count;
            }

            if ( apiDocument.Tagged != null ) 
            {
                document.Tagged = apiDocument.Tagged;
            }

            return document;
        }

        internal OneSpanSign.API.Document ToAPIDocument(OneSpanSign.API.Package apiPackage)
        {
            if (sdkDocument == null)
            {
                return apiDocument;
            }

            OneSpanSign.API.Document doc = ToAPIDocument();

            foreach (Signature signature in sdkDocument.Signatures)
            {
                OneSpanSign.API.Approval approval = new SignatureConverter(signature).ToAPIApproval();

                if (signature.IsPlaceholderSignature())
                {
                    approval.Role = signature.RoleId.Id;
                }
                else if (signature.IsGroupSignature())
                {
                    approval.Role = FindRoleIdForGroup(signature.GroupId, apiPackage);
                }
                else
                {
                    approval.Role = FindRoleIdForSigner(signature.SignerEmail, apiPackage);
                }
                doc.AddApproval(approval);
            }

            foreach (Field field in sdkDocument.Fields)
            {
                doc.AddField(new FieldConverter(field).ToAPIField());
            }

            foreach (Field field in sdkDocument.QRCodes)
            {
                doc.AddField(new FieldConverter(field).ToAPIField());
            }

            return doc;
        }

        internal OneSpanSign.API.Document ToAPIDocument()
        {
            if (sdkDocument == null)
            {
                return apiDocument;
            }

            OneSpanSign.API.Document doc = new OneSpanSign.API.Document();

            doc.Name = sdkDocument.Name;
            doc.Index = sdkDocument.Index;
            doc.Extract = sdkDocument.Extract;
            doc.ExtractionTypes = sdkDocument.ExtractionTypes;
            doc.External = new ExternalConverter(sdkDocument.External).ToAPIExternal();
            doc.Data = sdkDocument.Data;
            doc.Tagged = sdkDocument.Tagged;

            if (sdkDocument.Id != null)
            {
                doc.Id = sdkDocument.Id;
            }

            if (sdkDocument.Description != null)
            {
                doc.Description = sdkDocument.Description;
            }

            return doc;
        }

        private string FindRoleIdForGroup(GroupId groupId, OneSpanSign.API.Package createdPackage)
        {
            foreach (OneSpanSign.API.Role role in createdPackage.Roles)
            {
                if (role.Signers.Count > 0 && role.Signers[0].Group != null)
                {
                    if (groupId.Id.Equals(role.Signers[0].Group.Id))
                    {
                        return role.Id;
                    }
                }
            }

            throw new OssException(String.Format("No Role found for group with id {0}", groupId.Id), null);
        }

        private string FindRoleIdForSigner(string signerEmail, OneSpanSign.API.Package createdPackage)
        {
            foreach (OneSpanSign.API.Role role in createdPackage.Roles)
            {
                if (role.Signers.Count > 0 && role.Signers[0].Email != null)
                {
                    if (signerEmail.Equals(role.Signers[0].Email))
                    {
                        return role.Id;
                    }
                }
            }

            throw new OssException(String.Format("No Role found for signer email {0}", signerEmail), null);
        }
    }
}

