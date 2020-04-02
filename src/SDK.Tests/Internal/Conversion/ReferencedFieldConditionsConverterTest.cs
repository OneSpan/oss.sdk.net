using System;
using NUnit.Framework;
using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Builder;
using System.Collections.Generic;

namespace SDK.Tests
{
    [TestFixture()]
    public class ReferencedFieldConditionsConverterTest : ReferencedConditionsConverterTest
    {
        [Test()]
        public override void ConvertNullSDKToAPI()
        {
            OneSpanSign.API.ReferencedFieldConditions api = ReferencedFieldConditionsConverter.ToAPI(null);
            Assert.IsNull(api);
        }

        [Test()]
        public override void ConvertNullAPIToSDK()
        {
            ReferencedFieldConditions sdk = ReferencedFieldConditionsConverter.ToSDK(null);
            Assert.IsNull(sdk);
        }

        [Test()]
        public override void ConvertAPIToSDK()
        {
            List<OneSpanSign.API.FieldCondition> apiFieldConditionsReferencedInCondition = createApiFieldConditionsForTest(CONDITION_1_ID, FIELD_1_ID, FIELD_2_ID);
            List<OneSpanSign.API.FieldCondition> apiFieldConditionsReferencedInAction = new List<OneSpanSign.API.FieldCondition>();

            OneSpanSign.API.ReferencedFieldConditions api = new OneSpanSign.API.ReferencedFieldConditions();
            api.ReferencedInCondition = apiFieldConditionsReferencedInCondition;
            api.ReferencedInAction = apiFieldConditionsReferencedInAction;

            ReferencedFieldConditions sdk = ReferencedFieldConditionsConverter.ToSDK(api);

            Assert.IsTrue(compareFieldConditions(apiFieldConditionsReferencedInCondition, sdk.ReferencedInCondition));
            Assert.IsTrue(compareFieldConditions(apiFieldConditionsReferencedInAction, sdk.ReferencedInAction));
        }

        [Test()]
        public override void ConvertSDKToAPI()
        {
            List<FieldCondition> sdkFieldConditionsReferencedInCondition = createSdkFieldConditionsForTest(CONDITION_1_ID, FIELD_1_ID, FIELD_2_ID);
            List<FieldCondition> sdkFieldConditionsReferencedInAction = new List<FieldCondition>();

            ReferencedFieldConditions sdk = new ReferencedFieldConditions();
            sdk.ReferencedInCondition = sdkFieldConditionsReferencedInCondition;
            sdk.ReferencedInAction = sdkFieldConditionsReferencedInAction;

            OneSpanSign.API.ReferencedFieldConditions api = ReferencedFieldConditionsConverter.ToAPI(sdk);

            Assert.IsTrue(compareFieldConditions(api.ReferencedInCondition, sdkFieldConditionsReferencedInCondition));
            Assert.IsTrue(compareFieldConditions(api.ReferencedInAction, sdkFieldConditionsReferencedInAction));
        }
    }
}

