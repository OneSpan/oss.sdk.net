using System;
using System.Collections.Generic;
using NUnit.Framework;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.src.Internal.Conversion;

namespace SDK.Tests
{
    [TestFixture ()]
    public class ConditionalFieldConverterTest
    {
        private Silanis.ESL.SDK.ConditionalField sdkField1 = null;
        private Silanis.ESL.SDK.ConditionalField sdkField2 = null;
        private Silanis.ESL.API.ConditionalField apiField1 = null;
        private Silanis.ESL.API.ConditionalField apiField2 = null;
        private ConditionalFieldConverter converter = null;

        [Test ()]
        public void ConvertNullSDKToAPI ()
        {
            sdkField1 = null;
            converter = new ConditionalFieldConverter (sdkField1);
            Assert.IsNull (converter.ToAPIConditionalField ());
        }

        [Test ()]
        public void ConvertNullAPIToSDK ()
        {
            apiField1 = null;
            converter = new ConditionalFieldConverter (apiField1);
            Assert.IsNull (converter.ToSDKConditionalField ());
        }

        [Test ()]
        public void ConvertNullSDKToSDK ()
        {
            sdkField1 = null;
            converter = new ConditionalFieldConverter (sdkField1);
            Assert.IsNull (converter.ToSDKConditionalField ());
        }

        [Test ()]
        public void ConvertNullAPIToAPI ()
        {
            apiField1 = null;
            converter = new ConditionalFieldConverter (apiField1);
            Assert.IsNull (converter.ToAPIConditionalField ());
        }

        [Test ()]
        public void ConvertSDKToSDK ()
        {
            sdkField1 = CreateTypicalSDKConditionalField ();
            converter = new ConditionalFieldConverter (sdkField1);
            sdkField2 = converter.ToSDKConditionalField ();
            Assert.IsNotNull (sdkField2);
            Assert.AreEqual (sdkField2, sdkField1);
        }

        [Test ()]
        public void ConvertAPIToAPI ()
        {
            apiField1 = CreateTypicalAPIConditionalField ();
            converter = new ConditionalFieldConverter (apiField1);
            apiField2 = converter.ToAPIConditionalField ();
            Assert.IsNotNull (apiField2);
            Assert.AreEqual (apiField2, apiField1);
        }

        [Test ()]
        public void ConvertAPIToSDK ()
        {
            apiField1 = CreateTypicalAPIConditionalField ();
            sdkField1 = new ConditionalFieldConverter (apiField1).ToSDKConditionalField ();

            Assert.IsNotNull (sdkField1);
            Assert.AreEqual (sdkField1.Validator, new FieldValidatorConverter (apiField1.Validation).ToSDKFieldValidator ());
            Assert.AreEqual (sdkField1.Id, apiField1.Id);
            Assert.AreEqual (sdkField1.Name, apiField1.Name);
            Assert.AreEqual (sdkField1.Page, apiField1.Page);
            Assert.AreEqual (sdkField1.Style, new FieldStyleAndSubTypeConverter (apiField1.Subtype, apiField1.Binding).ToSDKFieldStyle ());
            Assert.AreEqual (sdkField1.TextAnchor, new TextAnchorConverter (apiField1.ExtractAnchor).ToSDKTextAnchor ());
            Assert.AreEqual (sdkField1.Value, apiField1.Value);
            Assert.AreEqual (sdkField1.X, apiField1.Left);
            Assert.AreEqual (sdkField1.Y, apiField1.Top);
            Assert.AreEqual (sdkField1.Width, apiField1.Width);
            Assert.AreEqual (sdkField1.Height, apiField1.Height);
            Assert.AreEqual (sdkField1.Conditions.Count, apiField1.Conditions.Count);
        }

        [Test ()]
        public void ConvertSDKToAPI ()
        {
            sdkField1 = CreateTypicalSDKConditionalField ();
            apiField1 = new ConditionalFieldConverter (sdkField1).ToAPIConditionalField ();

            Assert.IsNotNull (apiField1);
            Assert.AreEqual (sdkField1.Value, apiField1.Value);
            Assert.AreEqual (sdkField1.X, apiField1.Left);
            Assert.AreEqual (sdkField1.Y, apiField1.Top);
            Assert.AreEqual (sdkField1.Width, apiField1.Width);
            Assert.AreEqual (sdkField1.Height, apiField1.Height);
            Assert.AreEqual (sdkField1.Id, apiField1.Id);
            Assert.AreEqual (sdkField1.Name, apiField1.Name);
            Assert.AreEqual (sdkField1.Page, apiField1.Page);
            Assert.AreEqual (sdkField1.Conditions.Count, apiField1.Conditions.Count);
        }

        private Silanis.ESL.SDK.ConditionalField CreateTypicalSDKConditionalField ()
        {

            Silanis.ESL.SDK.ConditionalField sdkField = new Silanis.ESL.SDK.ConditionalField ();
            sdkField.Extract = false;
            sdkField.Height = 100.0;
            sdkField.X = 10.0;
            sdkField.Id = "3";
            sdkField.Name = "Field Name";
            sdkField.Page = 1;
            sdkField.Y = 101.0;
            sdkField.Style = FieldStyle.BOUND_DATE;
            sdkField.Value = "field value";
            sdkField.Width = 102.0;

            FieldCondition condition = new FieldCondition ();
            condition.Id = "ConditionId";
            condition.Condition = "document['DocumentId'].field['fieldId2'].value == 'X'";
            condition.Action = "document['DocumentId'].field['fieldId1'].disabled = true";
            List<FieldCondition> conditions = new List<FieldCondition> ();
            conditions.Add (condition);
            sdkField.Conditions = conditions;

            return sdkField;
        }

        private Silanis.ESL.API.ConditionalField CreateTypicalAPIConditionalField ()
        {
            Silanis.ESL.API.ConditionalField apiField = new Silanis.ESL.API.ConditionalField ();

            apiField.Extract = false;
            apiField.Height = 100.0;
            apiField.Left = 10.0;
            apiField.Id = "3";
            apiField.Name = "Field Name";
            apiField.Page = 1;
            apiField.Subtype = FieldStyle.UNBOUND_TEXT_FIELD.getApiValue ();
            apiField.Top = 101.0;
            apiField.Type = "INPUT";
            apiField.Value = "field value";
            apiField.Width = 102.0;

            Silanis.ESL.API.FieldCondition condition = new Silanis.ESL.API.FieldCondition ();
            condition.Id = "ConditionId";
            condition.Condition = "document['DocumentId'].field['fieldId2'].value == 'X'";
            condition.Action = "document['DocumentId'].field['fieldId1'].disabled = true";
            apiField.AddCondition(condition);

            return apiField;
        }

    }
}
