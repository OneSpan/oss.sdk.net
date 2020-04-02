using System;
using NUnit.Framework;
using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Builder;
using System.Collections.Generic;

namespace SDK.Tests
{
    [TestFixture()]
    public class ReferencedDocumentConverterTest : ReferencedConditionsConverterTest
    {
        [Test()]
        public override void ConvertNullSDKToAPI()
        {
            OneSpanSign.API.ReferencedDocument api = ReferencedDocumentConverter.ToAPI(null);
            Assert.IsNull(api);
        }

        [Test()]
        public override void ConvertNullAPIToSDK()
        {
            ReferencedDocument sdk = ReferencedDocumentConverter.ToSDK(null);
            Assert.IsNull(sdk);
        }

        [Test()]
        public override void ConvertAPIToSDK()
        {
            List<OneSpanSign.API.ReferencedField> apiFields = createApiReferencedFieldsForTest();

            OneSpanSign.API.ReferencedDocument api = new OneSpanSign.API.ReferencedDocument();
            api.DocumentId = DOCUMENT_ID;
            api.Fields = apiFields;

            ReferencedDocument sdk = ReferencedDocumentConverter.ToSDK(api);

            Assert.AreEqual(sdk.DocumentId, DOCUMENT_ID);
            Assert.IsTrue(compareReferencedFields(apiFields, sdk.Fields));
        }

        [Test()]
        public override void ConvertSDKToAPI()
        {
            List<ReferencedField> sdkFields = createSdkReferencedFieldsForTest();

            ReferencedDocument sdk = new ReferencedDocument();
            sdk.DocumentId = DOCUMENT_ID;
            sdk.Fields = sdkFields;

            OneSpanSign.API.ReferencedDocument api = ReferencedDocumentConverter.ToAPI(sdk);

            Assert.AreEqual(api.DocumentId, DOCUMENT_ID);
            Assert.IsTrue(compareReferencedFields(api.Fields, sdkFields));
        }
    }
}

