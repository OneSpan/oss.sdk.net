using System;
using NUnit.Framework;
using Silanis.ESL.SDK;

namespace SDK.Tests
{
    [TestFixture]
    public class SignatureStyleConverterTest
    {
        public SignatureStyleConverterTest()
        {
        }
        
        [Test]
        public void ToSDKFromCapture()
        {
            Silanis.ESL.API.FieldSubtype api = Silanis.ESL.API.FieldSubtype.CAPTURE;
            SignatureStyleConverter converter = new SignatureStyleConverter(api);
            SignatureStyle sdk = converter.ToSDKSignatureStyle();

            Assert.IsNotNull(sdk);
            Assert.AreEqual(SignatureStyle.HAND_DRAWN,sdk);            
        }
        
        [Test]
        public void ToSDKFromFullName()
        {
            Silanis.ESL.API.FieldSubtype api = Silanis.ESL.API.FieldSubtype.FULLNAME;
            SignatureStyleConverter converter = new SignatureStyleConverter(api);
            SignatureStyle sdk = converter.ToSDKSignatureStyle();

            Assert.IsNotNull(sdk);
            Assert.AreEqual(SignatureStyle.FULL_NAME,sdk);            
        }
        
        [Test]
        public void ToSDKFromInitials()
        {
            Silanis.ESL.API.FieldSubtype api = Silanis.ESL.API.FieldSubtype.INITIALS;
            SignatureStyleConverter converter = new SignatureStyleConverter(api);
            SignatureStyle sdk = converter.ToSDKSignatureStyle();

            Assert.IsNotNull(sdk);
            Assert.AreEqual(SignatureStyle.INITIALS,sdk);            
        }
        
        [Test]
        [ExpectedException( typeof( EslException ) )]
        public void ToSDKFromCheckbox()
        {
            Silanis.ESL.API.FieldSubtype api = Silanis.ESL.API.FieldSubtype.CHECKBOX;
            SignatureStyleConverter converter = new SignatureStyleConverter(api);
            SignatureStyle sdk = converter.ToSDKSignatureStyle();

            Assert.IsNull(sdk);
        }

        [Test]
        [ExpectedException( typeof( EslException ) )]
        public void ToSDKFromCustomField()
        {
            Silanis.ESL.API.FieldSubtype api = Silanis.ESL.API.FieldSubtype.CUSTOMFIELD;
            SignatureStyleConverter converter = new SignatureStyleConverter(api);
            SignatureStyle sdk = converter.ToSDKSignatureStyle();

            Assert.IsNull(sdk);
        }

        [Test]
        [ExpectedException( typeof( EslException ) )]
        public void ToSDKFromDate()
        {
            Silanis.ESL.API.FieldSubtype api = Silanis.ESL.API.FieldSubtype.DATE;
            SignatureStyleConverter converter = new SignatureStyleConverter(api);
            SignatureStyle sdk = converter.ToSDKSignatureStyle();

            Assert.IsNull(sdk);
        }

        [Test]
        [ExpectedException( typeof( EslException ) )]
        public void ToSDKFromLabel()
        {
            Silanis.ESL.API.FieldSubtype api = Silanis.ESL.API.FieldSubtype.LABEL;
            SignatureStyleConverter converter = new SignatureStyleConverter(api);
            SignatureStyle sdk = converter.ToSDKSignatureStyle();

            Assert.IsNull(sdk);
        }

        [Test]
        [ExpectedException( typeof( EslException ) )]
        public void ToSDKFromList()
        {
            Silanis.ESL.API.FieldSubtype api = Silanis.ESL.API.FieldSubtype.LIST;
            SignatureStyleConverter converter = new SignatureStyleConverter(api);
            SignatureStyle sdk = converter.ToSDKSignatureStyle();

            Assert.IsNull(sdk);
        }

        [Test]
        [ExpectedException( typeof( EslException ) )]
        public void ToSDKFromNotarize()
        {
            Silanis.ESL.API.FieldSubtype api = Silanis.ESL.API.FieldSubtype.NOTARIZE;
            SignatureStyleConverter converter = new SignatureStyleConverter(api);
            SignatureStyle sdk = converter.ToSDKSignatureStyle();

            Assert.IsNull(sdk);
        }

        [Test]
        [ExpectedException( typeof( EslException ) )]
        public void ToSDKFromQRCode()
        {
            Silanis.ESL.API.FieldSubtype api = Silanis.ESL.API.FieldSubtype.QRCODE;
            SignatureStyleConverter converter = new SignatureStyleConverter(api);
            SignatureStyle sdk = converter.ToSDKSignatureStyle();

            Assert.IsNull(sdk);
        }

        [Test]
        [ExpectedException( typeof( EslException ) )]
        public void ToSDKFromRadio()
        {
            Silanis.ESL.API.FieldSubtype api = Silanis.ESL.API.FieldSubtype.RADIO;
            SignatureStyleConverter converter = new SignatureStyleConverter(api);
            SignatureStyle sdk = converter.ToSDKSignatureStyle();

            Assert.IsNull(sdk);
        }

        [Test]
        [ExpectedException( typeof( EslException ) )]
        public void ToSDKFromTextArea()
        {
            Silanis.ESL.API.FieldSubtype api = Silanis.ESL.API.FieldSubtype.TEXTAREA;
            SignatureStyleConverter converter = new SignatureStyleConverter(api);
            SignatureStyle sdk = converter.ToSDKSignatureStyle();

            Assert.IsNull(sdk);
        }

        [Test]
        [ExpectedException( typeof( EslException ) )]
        public void ToSDKFromTextField()
        {
            Silanis.ESL.API.FieldSubtype api = Silanis.ESL.API.FieldSubtype.TEXTFIELD;
            SignatureStyleConverter converter = new SignatureStyleConverter(api);
            SignatureStyle sdk = converter.ToSDKSignatureStyle();

            Assert.IsNull(sdk);
        }
    }
}

