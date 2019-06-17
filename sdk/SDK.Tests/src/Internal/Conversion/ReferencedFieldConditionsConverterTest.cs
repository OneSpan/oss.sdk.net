using System;
using NUnit.Framework;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;
using System.Collections.Generic;

namespace SDK.Tests
{
    [TestFixture()]
    public class ReferencedFieldConditionsConverterTest : ReferencedConditionsConverterTest
    {
        [Test()]
        public override void ConvertNullSDKToAPI()
        {
            Silanis.ESL.API.ReferencedFieldConditions api = ReferencedFieldConditionsConverter.ToAPI(null);
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
            List<Silanis.ESL.API.FieldCondition> apiFieldConditionsReferencedInCondition = createApiFieldConditionsForTest(CONDITION_1_ID, FIELD_1_ID, FIELD_2_ID);
            List<Silanis.ESL.API.FieldCondition> apiFieldConditionsReferencedInAction = new List<Silanis.ESL.API.FieldCondition>();

            Silanis.ESL.API.ReferencedFieldConditions api = new Silanis.ESL.API.ReferencedFieldConditions();
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

            Silanis.ESL.API.ReferencedFieldConditions api = ReferencedFieldConditionsConverter.ToAPI(sdk);

            Assert.IsTrue(compareFieldConditions(api.ReferencedInCondition, sdkFieldConditionsReferencedInCondition));
            Assert.IsTrue(compareFieldConditions(api.ReferencedInAction, sdkFieldConditionsReferencedInAction));
        }
    }
}

