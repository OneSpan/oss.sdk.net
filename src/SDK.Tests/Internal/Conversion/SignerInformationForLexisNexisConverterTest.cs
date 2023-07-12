using NUnit.Framework;
using System;
using OneSpanSign.Sdk;

namespace SDK.Tests
{
    [TestFixture]
    public class SignerInformationForLexisNexisConverterTest
    {
        private OneSpanSign.Sdk.SignerInformationForLexisNexis sdkSignerInformationForLexisNexis1 = null;
        private OneSpanSign.Sdk.SignerInformationForLexisNexis sdkSignerInformationForLexisNexis2 = null;
        private OneSpanSign.API.SignerInformationForLexisNexis apiSignerInformationForLexisNexis1 = null;
        private OneSpanSign.API.SignerInformationForLexisNexis apiSignerInformationForLexisNexis2 = null;
        private SignerInformationForLexisNexisConverter converter;

        [Test]
        public void ConvertNullSDKToAPI()
        {
            sdkSignerInformationForLexisNexis1 = null;
            converter = new SignerInformationForLexisNexisConverter(sdkSignerInformationForLexisNexis1);
            Assert.IsNull(converter.ToApiSignerInformationForLexisNexis());
        }

        [Test]
        public void ConvertNullAPIToSDK()
        {
            apiSignerInformationForLexisNexis1 = null;
            converter = new SignerInformationForLexisNexisConverter(apiSignerInformationForLexisNexis1);
            Assert.IsNull(converter.ToSDKSignerInformationForLexisNexis());
        }

        [Test]
        public void ConvertNullSDKToSDK()
        {
            sdkSignerInformationForLexisNexis1 = null;
            converter = new SignerInformationForLexisNexisConverter(sdkSignerInformationForLexisNexis1);
            Assert.IsNull(converter.ToSDKSignerInformationForLexisNexis());
        }

        [Test]
        public void ConvertNullAPIToAPI()
        {
            apiSignerInformationForLexisNexis1 = null;
            converter = new SignerInformationForLexisNexisConverter(apiSignerInformationForLexisNexis1);
            Assert.IsNull(converter.ToApiSignerInformationForLexisNexis());
        }

        [Test]
        public void ConvertSDKToSDK()
        {
            sdkSignerInformationForLexisNexis1 = CreateTypicalSDKSignerInformationForLexisNexis();
            sdkSignerInformationForLexisNexis2 = new SignerInformationForLexisNexisConverter(sdkSignerInformationForLexisNexis1).ToSDKSignerInformationForLexisNexis();

            Assert.IsNotNull(sdkSignerInformationForLexisNexis2);
            Assert.AreEqual(sdkSignerInformationForLexisNexis2, sdkSignerInformationForLexisNexis1);
        }

        [Test]
        public void ConvertAPIToAPI()
        {
            apiSignerInformationForLexisNexis1 = CreateTypicalAPISignerInformationForLexisNexis();
            apiSignerInformationForLexisNexis2 = new SignerInformationForLexisNexisConverter(apiSignerInformationForLexisNexis1).ToApiSignerInformationForLexisNexis();

            Assert.IsNotNull(apiSignerInformationForLexisNexis2);
            Assert.AreEqual(apiSignerInformationForLexisNexis2, apiSignerInformationForLexisNexis1);
        }

        [Test]
        public void ConvertAPIToSDK()
        {
            apiSignerInformationForLexisNexis1 = CreateTypicalAPISignerInformationForLexisNexis();
            sdkSignerInformationForLexisNexis1 = new SignerInformationForLexisNexisConverter(apiSignerInformationForLexisNexis1).ToSDKSignerInformationForLexisNexis();

            Assert.AreEqual(sdkSignerInformationForLexisNexis1.FirstName, apiSignerInformationForLexisNexis1.FirstName);
            Assert.AreEqual(sdkSignerInformationForLexisNexis1.LastName, apiSignerInformationForLexisNexis1.LastName);
            Assert.AreEqual(sdkSignerInformationForLexisNexis1.FlatOrApartmentNumber, apiSignerInformationForLexisNexis1.FlatOrApartmentNumber);
            Assert.AreEqual(sdkSignerInformationForLexisNexis1.HouseName, apiSignerInformationForLexisNexis1.HouseName);
            Assert.AreEqual(sdkSignerInformationForLexisNexis1.HouseNumber, apiSignerInformationForLexisNexis1.HouseNumber);
            Assert.AreEqual(sdkSignerInformationForLexisNexis1.City, apiSignerInformationForLexisNexis1.City);
            Assert.AreEqual(sdkSignerInformationForLexisNexis1.State, apiSignerInformationForLexisNexis1.State);
            Assert.AreEqual(sdkSignerInformationForLexisNexis1.Zip, apiSignerInformationForLexisNexis1.Zip);
            Assert.AreEqual(sdkSignerInformationForLexisNexis1.SocialSecurityNumber, apiSignerInformationForLexisNexis1.SocialSecurityNumber);
            Assert.AreEqual(sdkSignerInformationForLexisNexis1.DateOfBirth, apiSignerInformationForLexisNexis1.DateOfBirth);
        }

        [Test]
        public void ConvertSDKToAPI()
        {
            sdkSignerInformationForLexisNexis1 = CreateTypicalSDKSignerInformationForLexisNexis();
            apiSignerInformationForLexisNexis1 = new SignerInformationForLexisNexisConverter(sdkSignerInformationForLexisNexis1).ToApiSignerInformationForLexisNexis();

            Assert.AreEqual(apiSignerInformationForLexisNexis1.FirstName, sdkSignerInformationForLexisNexis1.FirstName);
            Assert.AreEqual(apiSignerInformationForLexisNexis1.LastName, sdkSignerInformationForLexisNexis1.LastName);
            Assert.AreEqual(apiSignerInformationForLexisNexis1.FlatOrApartmentNumber, sdkSignerInformationForLexisNexis1.FlatOrApartmentNumber);
            Assert.AreEqual(apiSignerInformationForLexisNexis1.HouseName, sdkSignerInformationForLexisNexis1.HouseName);
            Assert.AreEqual(apiSignerInformationForLexisNexis1.HouseNumber, sdkSignerInformationForLexisNexis1.HouseNumber);
            Assert.AreEqual(apiSignerInformationForLexisNexis1.City, sdkSignerInformationForLexisNexis1.City);
            Assert.AreEqual(apiSignerInformationForLexisNexis1.State, sdkSignerInformationForLexisNexis1.State);
            Assert.AreEqual(apiSignerInformationForLexisNexis1.Zip, sdkSignerInformationForLexisNexis1.Zip);
            Assert.AreEqual(apiSignerInformationForLexisNexis1.SocialSecurityNumber, sdkSignerInformationForLexisNexis1.SocialSecurityNumber);
            Assert.AreEqual(apiSignerInformationForLexisNexis1.DateOfBirth, sdkSignerInformationForLexisNexis1.DateOfBirth);
        }

        private OneSpanSign.Sdk.SignerInformationForLexisNexis CreateTypicalSDKSignerInformationForLexisNexis()
        {

            OneSpanSign.Sdk.SignerInformationForLexisNexis SignerInformationForLexisNexis = SignerInformationForLexisNexisBuilder.NewSignerInformationForLexisNexis()
                .WithFirstName("Signer First Name")
                .WithLastName("Last Name")
                .WithFlatOrApartmentNumber("1234")
                .WithHouseName("Decarie")
                .WithHouseNumber("5678")
                .WithCity("Montreal")
                .WithState("Quebec")
                .WithZip("H4L3K1")
                .WithSocialSecurityNumber("111-222-333-444")
                .WithDateOfBirth(new DateTime())
                .Build();

            return SignerInformationForLexisNexis;
        }

        private OneSpanSign.API.SignerInformationForLexisNexis CreateTypicalAPISignerInformationForLexisNexis()
        {
            OneSpanSign.API.SignerInformationForLexisNexis SignerInformationForLexisNexis = new OneSpanSign.API.SignerInformationForLexisNexis();
            SignerInformationForLexisNexis.FirstName = "first name";
            SignerInformationForLexisNexis.LastName = "last name";
            SignerInformationForLexisNexis.FlatOrApartmentNumber = "1234";
            SignerInformationForLexisNexis.HouseName = "Decarie";
            SignerInformationForLexisNexis.HouseNumber = "5678";
            SignerInformationForLexisNexis.City = "Montreal";
            SignerInformationForLexisNexis.State = "Quebec";
            SignerInformationForLexisNexis.Zip = "1h27r4";
            SignerInformationForLexisNexis.SocialSecurityNumber = "222-099-888-333";
            SignerInformationForLexisNexis.DateOfBirth = new DateTime();

            return SignerInformationForLexisNexis;
        }
    }
}
