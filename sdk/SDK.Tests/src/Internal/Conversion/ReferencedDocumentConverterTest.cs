using System;
using NUnit.Framework;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;
using System.Collections.Generic;

namespace SDK.Tests
{
    [TestFixture()]
    public class ReferencedDocumentConverterTest : ReferencedConditionsConverterTest
    {
        [Test()]
        public override void ConvertNullSDKToAPI()
        {
            Silanis.ESL.API.ReferencedDocument api = ReferencedDocumentConverter.ToAPI(null);
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
            List<Silanis.ESL.API.ReferencedField> apiFields = createApiReferencedFieldsForTest();

            Silanis.ESL.API.ReferencedDocument api = new Silanis.ESL.API.ReferencedDocument();
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

            Silanis.ESL.API.ReferencedDocument api = ReferencedDocumentConverter.ToAPI(sdk);

            Assert.AreEqual(api.DocumentId, DOCUMENT_ID);
            Assert.IsTrue(compareReferencedFields(api.Fields, sdkFields));
        }
    }
}

