using System;
using NUnit.Framework;
using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Builder;
using OneSpanSign.API;

namespace SDK.Tests
{
    [TestFixture]
    public class TextAnchorPositionTest
    {
        [Test]
        public void whenBuildingTextAnchorPositionWithAPIValueTOPLEFTThenTOPLEFTTextAnchorPositionIsReturned()
        {
            string expectedSDKValue = "TOPLEFT";


            OneSpanSign.Sdk.TextAnchorPosition classUnderTest = OneSpanSign.Sdk.TextAnchorPosition.valueOf("TOPLEFT");
            String actualSDKValue = classUnderTest.getSdkValue();


            Assert.AreEqual(expectedSDKValue, actualSDKValue);
        }

        [Test]
        public void whenBuildingTextAnchorPositionWithAPIValueTOPRIGHTThenTOPRIGHTTextAnchorPositionIsReturned()
        {
            string expectedSDKValue = "TOPRIGHT";


            OneSpanSign.Sdk.TextAnchorPosition classUnderTest = OneSpanSign.Sdk.TextAnchorPosition.valueOf("TOPRIGHT");
            String actualSDKValue = classUnderTest.getSdkValue();


            Assert.AreEqual(expectedSDKValue, actualSDKValue);
        }

        [Test]
        public void whenBuildingTextAnchorPositionWithAPIValueBOTTOMLEFTThenBOTTOMLEFTTextAnchorPositionIsReturned()
        {
            string expectedSDKValue = "BOTTOMLEFT";


            OneSpanSign.Sdk.TextAnchorPosition classUnderTest = OneSpanSign.Sdk.TextAnchorPosition.valueOf("BOTTOMLEFT");
            String actualSDKValue = classUnderTest.getSdkValue();


            Assert.AreEqual(expectedSDKValue, actualSDKValue);
        }
        [Test]
        public void whenBuildingTextAnchorPositionWithAPIValueBOTTOMRIGHTThenBOTTOMRIGHTTextAnchorPositionIsReturned()
        {
            string expectedSDKValue = "BOTTOMRIGHT";


            OneSpanSign.Sdk.TextAnchorPosition classUnderTest = OneSpanSign.Sdk.TextAnchorPosition.valueOf("BOTTOMRIGHT");
            String actualSDKValue = classUnderTest.getSdkValue();


            Assert.AreEqual(expectedSDKValue, actualSDKValue);
        }
        [Test]
        public void whenBuildingTextAnchorPositionWithUnknownAPIValueThenUNRECOGNIZEDTextAnchorPositionIsReturned()
        {
            string expectedSDKValue = "UNRECOGNIZED";


            OneSpanSign.Sdk.TextAnchorPosition classUnderTest = OneSpanSign.Sdk.TextAnchorPosition.valueOf("ThisTextAnchorPositionDoesNotExistINSDK");
            String actualSDKValue = classUnderTest.getSdkValue();


            Assert.AreEqual(expectedSDKValue, actualSDKValue);
        }
    }
}   