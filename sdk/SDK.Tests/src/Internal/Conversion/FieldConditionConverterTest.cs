using System;
using NUnit.Framework;
using Silanis.ESL.SDK;

namespace SDK.Tests
{
    [TestFixture ()]
    public class FieldConditionConverterTest
    {
        private Silanis.ESL.SDK.FieldCondition sdkFieldCondition1 = null;
        private Silanis.ESL.SDK.FieldCondition sdkFieldCondition2 = null;
        private Silanis.ESL.API.FieldCondition apiFieldCondition1 = null;
        private Silanis.ESL.API.FieldCondition apiFieldCondition2 = null;
        private FieldConditionConverter converter = null;

        [Test ()]
        public void ConvertNullSDKToAPI ()
        {
            sdkFieldCondition1 = null;
            converter = new FieldConditionConverter (sdkFieldCondition1);
            Assert.IsNull (converter.ToAPIFieldCondition ());
        }

        [Test ()]
        public void ConvertNullAPIToSDK ()
        {
            apiFieldCondition1 = null;
            converter = new FieldConditionConverter (apiFieldCondition1);
            Assert.IsNull (converter.ToSDKFieldCondition ());
        }

        [Test ()]
        public void ConvertNullSDKToSDK ()
        {
            sdkFieldCondition1 = null;
            converter = new FieldConditionConverter (sdkFieldCondition1);
            Assert.IsNull (converter.ToSDKFieldCondition ());
        }

        [Test ()]
        public void ConvertNullAPIToAPI ()
        {
            apiFieldCondition1 = null;
            converter = new FieldConditionConverter (apiFieldCondition1);
            Assert.IsNull (converter.ToAPIFieldCondition ());
        }

        [Test ()]
        public void ConvertSDKToSDK ()
        {
            sdkFieldCondition1 = CreateTypicalSDKFieldCondition ();
            converter = new FieldConditionConverter (sdkFieldCondition1);
            sdkFieldCondition2 = converter.ToSDKFieldCondition ();
            Assert.IsNotNull (sdkFieldCondition2);
            Assert.AreEqual (sdkFieldCondition2, sdkFieldCondition1);
        }

        [Test ()]
        public void ConvertAPIToAPI ()
        {
            apiFieldCondition1 = CreateTypicalAPIFieldCondition ();
            converter = new FieldConditionConverter (apiFieldCondition1);
            apiFieldCondition2 = converter.ToAPIFieldCondition ();
            Assert.IsNotNull (apiFieldCondition2);
            Assert.AreEqual (apiFieldCondition2, apiFieldCondition1);
        }

        [Test ()]
        public void ConvertAPIToSDK ()
        {
            apiFieldCondition1 = CreateTypicalAPIFieldCondition ();
            sdkFieldCondition1 = new FieldConditionConverter (apiFieldCondition1).ToSDKFieldCondition ();

            Assert.IsNotNull (sdkFieldCondition1);
            Assert.AreEqual (sdkFieldCondition1.Id, apiFieldCondition1.Id);
            Assert.AreEqual (sdkFieldCondition1.Condition, apiFieldCondition1.Condition);
            Assert.AreEqual (sdkFieldCondition1.Action, apiFieldCondition1.Action);
        }

        [Test ()]
        public void ConvertSDKToAPI ()
        {
            sdkFieldCondition1 = CreateTypicalSDKFieldCondition ();
            apiFieldCondition1 = new FieldConditionConverter (sdkFieldCondition1).ToAPIFieldCondition ();

            Assert.IsNotNull (apiFieldCondition1);
            Assert.AreEqual (sdkFieldCondition1.Id, apiFieldCondition1.Id);
            Assert.AreEqual (sdkFieldCondition1.Condition, apiFieldCondition1.Condition);
            Assert.AreEqual (sdkFieldCondition1.Action, apiFieldCondition1.Action);
       }

        private Silanis.ESL.SDK.FieldCondition CreateTypicalSDKFieldCondition ()
        {

            FieldCondition sdkFieldCondition = new FieldCondition ();
            sdkFieldCondition.Id = "ConditionId";
            sdkFieldCondition.Condition = "document['DocumentId'].field['fieldId2'].value == 'X'";
            sdkFieldCondition.Action = "document['DocumentId'].field['fieldId1'].disabled = true";


            return sdkFieldCondition;
        }

        private Silanis.ESL.API.FieldCondition CreateTypicalAPIFieldCondition ()
        {
            Silanis.ESL.API.FieldCondition apiFieldCondition = new Silanis.ESL.API.FieldCondition ();

            apiFieldCondition.Id = "ConditionId";
            apiFieldCondition.Condition = "document['DocumentId'].field['fieldId2'].value == 'X'";
            apiFieldCondition.Action = "document['DocumentId'].field['fieldId1'].disabled = true";

            return apiFieldCondition;
        }

    }
}
