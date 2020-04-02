using System;
using NUnit.Framework;
using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Builder;
using System.Collections.Generic;

namespace SDK.Tests
{
    [TestFixture()]
    public class ReferencedFieldConverterTest : ReferencedConditionsConverterTest
    {
        [Test()]
        public override void ConvertNullSDKToAPI()
        {
            OneSpanSign.API.ReferencedField api = ReferencedFieldConverter.ToAPI(null);
            Assert.IsNull(api);
        }

        [Test()]
        public override void ConvertNullAPIToSDK()
        {
            ReferencedField sdk = ReferencedFieldConverter.ToSDK(null);
            Assert.IsNull(sdk);
        }

        [Test()]
        public override void ConvertAPIToSDK()
        {
            OneSpanSign.API.ReferencedFieldConditions apiConditions = createApiReferencedFieldConditionsForTest();

            OneSpanSign.API.ReferencedField api = new OneSpanSign.API.ReferencedField();
            api.FieldId = FIELD_1_ID;
            api.Conditions = apiConditions;

            ReferencedField sdk = ReferencedFieldConverter.ToSDK(api);

            Assert.AreEqual(sdk.FieldId, FIELD_1_ID);
            Assert.IsTrue(compareReferencedFieldConditions(apiConditions, sdk.Conditions));
        }

        [Test()]
        public override void ConvertSDKToAPI()
        {
            ReferencedFieldConditions sdkConditions = createSdkReferencedFieldConditionsForTest();

            ReferencedField sdk = new ReferencedField();
            sdk.FieldId = FIELD_1_ID;
            sdk.Conditions = sdkConditions;

            OneSpanSign.API.ReferencedField api = ReferencedFieldConverter.ToAPI(sdk);

            Assert.AreEqual(api.FieldId, FIELD_1_ID);
            Assert.IsTrue(compareReferencedFieldConditions(api.Conditions, sdkConditions));
        }
    }
}

