using System;
using NUnit.Framework;
using Silanis.ESL.SDK;
using Silanis.ESL.API;

namespace SDK.Tests
{

    [TestFixture()]
    public class FieldStyleAndSubTypeConverterTest
    {
        private Silanis.ESL.API.FieldSubtype apiFieldSubtype1;
        private Silanis.ESL.API.FieldSubtype apiFieldSubtype2;
        private Silanis.ESL.SDK.FieldStyle sdkFieldStyle1;
        private Silanis.ESL.SDK.FieldStyle sdkFieldStyle2;

        [Test()]
        public void ConvertSDKToSDK()
        {
            sdkFieldStyle1 = FieldStyle.UNBOUND_CUSTOM_FIELD;
            sdkFieldStyle2 = new FieldStyleAndSubTypeConverter(sdkFieldStyle1).ToSDKFieldStyle();
            Assert.IsNotNull(sdkFieldStyle2);
            Assert.AreEqual(sdkFieldStyle2, sdkFieldStyle1);
        }

        [Test()]
        public void ConvertAPIToAPI()
        {
            string binding = null;
            apiFieldSubtype1 = FieldSubtype.CUSTOMFIELD;
            apiFieldSubtype2 = new FieldStyleAndSubTypeConverter(apiFieldSubtype1, binding).ToAPIFieldSubtype();

            Assert.IsNotNull(apiFieldSubtype2);
            Assert.AreEqual(apiFieldSubtype2, apiFieldSubtype1);
        }

        [Test()]
        public void ConvertAPIToSDK()
        {
            string BINDING_DATE = "{approval.signed}";
            string BINDING_TITLE = "{signer.title}";
            string BINDING_NAME = "{signer.name}";
            string BINDING_COMPANY = "{signer.company}";

            // Where the conversion is based on subtype.
            string binding;
            FieldSubtype fieldSubtype;
            FieldStyle fieldStyle;

            fieldSubtype = FieldSubtype.CUSTOMFIELD;
            binding = null;
            fieldStyle = new FieldStyleAndSubTypeConverter(fieldSubtype, binding).ToSDKFieldStyle();
            Assert.AreEqual(fieldStyle, FieldStyle.UNBOUND_CUSTOM_FIELD);

            fieldSubtype = FieldSubtype.TEXTFIELD;
            binding = null;
            fieldStyle = new FieldStyleAndSubTypeConverter(fieldSubtype, binding).ToSDKFieldStyle();
            Assert.AreEqual(fieldStyle, FieldStyle.UNBOUND_TEXT_FIELD);

            fieldSubtype = FieldSubtype.CHECKBOX;
            binding = null;
            fieldStyle = new FieldStyleAndSubTypeConverter(fieldSubtype, binding).ToSDKFieldStyle();
            Assert.AreEqual(fieldStyle, FieldStyle.UNBOUND_CHECK_BOX);

            fieldSubtype = FieldSubtype.RADIO;
            binding = null;
            fieldStyle = new FieldStyleAndSubTypeConverter(fieldSubtype, binding).ToSDKFieldStyle();
            Assert.AreEqual(fieldStyle, FieldStyle.UNBOUND_RADIO_BUTTON);

            fieldSubtype = FieldSubtype.TEXTAREA;
            binding = null;
            fieldStyle = new FieldStyleAndSubTypeConverter(fieldSubtype, binding).ToSDKFieldStyle();
            Assert.AreEqual(fieldStyle, FieldStyle.TEXT_AREA);

            fieldSubtype = FieldSubtype.LIST;
            binding = null;
            fieldStyle = new FieldStyleAndSubTypeConverter(fieldSubtype, binding).ToSDKFieldStyle();
            Assert.AreEqual(fieldStyle, FieldStyle.DROP_LIST);

            fieldSubtype = FieldSubtype.QRCODE;
            binding = null;
            fieldStyle = new FieldStyleAndSubTypeConverter(fieldSubtype, binding).ToSDKFieldStyle();
            Assert.AreEqual(fieldStyle, FieldStyle.BOUND_QRCODE);

            // Where the conversion is based on binding.
            fieldSubtype = new FieldSubtype();
            binding = BINDING_DATE;
            fieldStyle = new FieldStyleAndSubTypeConverter(fieldSubtype, binding).ToSDKFieldStyle();
            Assert.AreEqual(fieldStyle, FieldStyle.BOUND_DATE);

            fieldSubtype = new FieldSubtype();
            binding = BINDING_TITLE;
            fieldStyle = new FieldStyleAndSubTypeConverter(fieldSubtype, binding).ToSDKFieldStyle();
            Assert.AreEqual(fieldStyle, FieldStyle.BOUND_TITLE);

            fieldSubtype = new FieldSubtype();
            binding = BINDING_NAME;
            fieldStyle = new FieldStyleAndSubTypeConverter(fieldSubtype, binding).ToSDKFieldStyle();
            Assert.AreEqual(fieldStyle, FieldStyle.BOUND_NAME);

            fieldSubtype = new FieldSubtype();
            binding = BINDING_COMPANY;
            fieldStyle = new FieldStyleAndSubTypeConverter(fieldSubtype, binding).ToSDKFieldStyle();
            Assert.AreEqual(fieldStyle, FieldStyle.BOUND_COMPANY);
        }

        [Test()]
        public void ConvertSDKToAPI()
        {
            FieldStyle fieldStyle = FieldStyle.UNBOUND_CUSTOM_FIELD;
            FieldSubtype fieldSubtype = new FieldStyleAndSubTypeConverter(fieldStyle).ToAPIFieldSubtype();
            Assert.AreEqual(fieldSubtype, FieldSubtype.CUSTOMFIELD);

            fieldStyle = FieldStyle.UNBOUND_TEXT_FIELD;
            fieldSubtype = new FieldStyleAndSubTypeConverter(fieldStyle).ToAPIFieldSubtype();
            Assert.AreEqual(fieldSubtype, FieldSubtype.TEXTFIELD);

            fieldStyle = FieldStyle.LABEL;
            fieldSubtype = new FieldStyleAndSubTypeConverter(fieldStyle).ToAPIFieldSubtype();
            Assert.AreEqual(fieldSubtype, FieldSubtype.LABEL);

            fieldStyle = FieldStyle.UNBOUND_CHECK_BOX;
            fieldSubtype = new FieldStyleAndSubTypeConverter(fieldStyle).ToAPIFieldSubtype();
            Assert.AreEqual(fieldSubtype, FieldSubtype.CHECKBOX);

            fieldStyle = FieldStyle.UNBOUND_RADIO_BUTTON;
            fieldSubtype = new FieldStyleAndSubTypeConverter(fieldStyle).ToAPIFieldSubtype();
            Assert.AreEqual(fieldSubtype, FieldSubtype.RADIO);

            fieldStyle = FieldStyle.DROP_LIST;
            fieldSubtype = new FieldStyleAndSubTypeConverter(fieldStyle).ToAPIFieldSubtype();
            Assert.AreEqual(fieldSubtype, FieldSubtype.LIST);

            fieldStyle = FieldStyle.TEXT_AREA;
            fieldSubtype = new FieldStyleAndSubTypeConverter(fieldStyle).ToAPIFieldSubtype();
            Assert.AreEqual(fieldSubtype, FieldSubtype.TEXTAREA);

            fieldStyle = FieldStyle.BOUND_QRCODE;
            fieldSubtype = new FieldStyleAndSubTypeConverter(fieldStyle).ToAPIFieldSubtype();
            Assert.AreEqual(fieldSubtype, FieldSubtype.QRCODE);

            fieldStyle = FieldStyle.BOUND_DATE;
            fieldSubtype = new FieldStyleAndSubTypeConverter(fieldStyle).ToAPIFieldSubtype();
            Assert.AreEqual(fieldSubtype, FieldSubtype.LABEL);

            fieldStyle = FieldStyle.BOUND_NAME;
            fieldSubtype = new FieldStyleAndSubTypeConverter(fieldStyle).ToAPIFieldSubtype();
            Assert.AreEqual(fieldSubtype, FieldSubtype.LABEL);

            fieldStyle = FieldStyle.BOUND_TITLE;
            fieldSubtype = new FieldStyleAndSubTypeConverter(fieldStyle).ToAPIFieldSubtype();
            Assert.AreEqual(fieldSubtype, FieldSubtype.LABEL);

            fieldStyle = FieldStyle.BOUND_COMPANY;
            fieldSubtype = new FieldStyleAndSubTypeConverter(fieldStyle).ToAPIFieldSubtype();
            Assert.AreEqual(fieldSubtype, FieldSubtype.LABEL);
        }

    }
}

