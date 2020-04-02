using System;
using NUnit.Framework;
using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Builder;
using OneSpanSign.API;

namespace SDK.Tests
{
    [TestFixture]
    public class SignatureStyleTest
    {
        [Test]
        public void whenBuildingSignatureStyleWithAPIValueINITIALSThenINITIALSSignatureStyleIsReturned()
        {
            string expectedSDKValue = "INITIALS";


            OneSpanSign.Sdk.SignatureStyle classUnderTest = OneSpanSign.Sdk.SignatureStyle.valueOf("INITIALS");
            String actualSDKValue = classUnderTest.getSdkValue();


            Assert.AreEqual(expectedSDKValue, actualSDKValue);
        }

        [Test]
        public void whenBuildingSignatureStyleWithAPIValueCAPTUREThenHAND_DRAWNSignatureStyleIsReturned()
        {
            string expectedSDKValue = "HAND_DRAWN";


            OneSpanSign.Sdk.SignatureStyle classUnderTest = OneSpanSign.Sdk.SignatureStyle.valueOf("CAPTURE");
            String actualSDKValue = classUnderTest.getSdkValue();


            Assert.AreEqual(expectedSDKValue, actualSDKValue);
        }
        
        [Test]
        public void whenBuildingSignatureStyleWithAPIValueFULLNAMEThenREGULARSignatureStyleIsReturned()
        {
            string expectedSDKValue = "FULL_NAME";


            OneSpanSign.Sdk.SignatureStyle classUnderTest = OneSpanSign.Sdk.SignatureStyle.valueOf("FULLNAME");
            String actualSDKValue = classUnderTest.getSdkValue();


            Assert.AreEqual(expectedSDKValue, actualSDKValue);
        }
        [Test]
        public void whenBuildingSignatureStyleWithUnknownAPIValueThenUNRECOGNIZEDSignatureStyleIsReturned()
        {
            string expectedSDKValue = "UNRECOGNIZED";


            OneSpanSign.Sdk.SignatureStyle classUnderTest = OneSpanSign.Sdk.SignatureStyle.valueOf("ThisSignatureStyleDoesNotExistINSDK");
            String actualSDKValue = classUnderTest.getSdkValue();


            Assert.AreEqual(expectedSDKValue, actualSDKValue);
        }
    }
}   