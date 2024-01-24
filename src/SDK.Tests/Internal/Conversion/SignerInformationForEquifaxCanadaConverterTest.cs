﻿using NUnit.Framework;
using System;
using OneSpanSign.Sdk;

namespace SDK.Tests
{
    [TestFixture]
    public class SignerInformationForEquifaxCanadaConverterTest
    {
        private OneSpanSign.Sdk.SignerInformationForEquifaxCanada sdkSignerInformationForEquifaxCanada1 = null;
        private OneSpanSign.Sdk.SignerInformationForEquifaxCanada sdkSignerInformationForEquifaxCanada2 = null;
        private OneSpanSign.API.SignerInformationForEquifaxCanada apiSignerInformationForEquifaxCanada1 = null;
        private OneSpanSign.API.SignerInformationForEquifaxCanada apiSignerInformationForEquifaxCanada2 = null;
        private SignerInformationForEquifaxCanadaConverter converter;

        [Test]
        public void ConvertNullSDKToAPI()
        {
            sdkSignerInformationForEquifaxCanada1 = null;
            converter = new SignerInformationForEquifaxCanadaConverter(sdkSignerInformationForEquifaxCanada1);
            Assert.IsNull(converter.ToAPISignerInformationForEquifaxCanada());
        }

        [Test]
        public void ConvertNullAPIToSDK()
        {
            apiSignerInformationForEquifaxCanada1 = null;
            converter = new SignerInformationForEquifaxCanadaConverter(apiSignerInformationForEquifaxCanada1);
            Assert.IsNull(converter.ToSDKSignerInformationForEquifaxCanada());
        }

        [Test]
        public void ConvertNullSDKToSDK()
        {
            sdkSignerInformationForEquifaxCanada1 = null;
            converter = new SignerInformationForEquifaxCanadaConverter(sdkSignerInformationForEquifaxCanada1);
            Assert.IsNull(converter.ToSDKSignerInformationForEquifaxCanada());
        }

        [Test]
        public void ConvertNullAPIToAPI()
        {
            apiSignerInformationForEquifaxCanada1 = null;
            converter = new SignerInformationForEquifaxCanadaConverter(apiSignerInformationForEquifaxCanada1);
            Assert.IsNull(converter.ToAPISignerInformationForEquifaxCanada());
        }

        [Test]
        public void ConvertSDKToSDK()
        {
            sdkSignerInformationForEquifaxCanada1 = CreateTypicalSDKSignerInformationForEquifaxCanada();
            sdkSignerInformationForEquifaxCanada2 = new SignerInformationForEquifaxCanadaConverter(sdkSignerInformationForEquifaxCanada1).ToSDKSignerInformationForEquifaxCanada();

            Assert.IsNotNull(sdkSignerInformationForEquifaxCanada2);
            Assert.AreEqual(sdkSignerInformationForEquifaxCanada2, sdkSignerInformationForEquifaxCanada1);
        }

        [Test]
        public void ConvertAPIToAPI()
        {
            apiSignerInformationForEquifaxCanada1 = CreateTypicalAPISignerInformationForEquifaxCanada();
            apiSignerInformationForEquifaxCanada2 = new SignerInformationForEquifaxCanadaConverter(apiSignerInformationForEquifaxCanada1).ToAPISignerInformationForEquifaxCanada();

            Assert.IsNotNull(apiSignerInformationForEquifaxCanada2);
            Assert.AreEqual(apiSignerInformationForEquifaxCanada2, apiSignerInformationForEquifaxCanada1);
        }

        [Test]
        public void ConvertAPIToSDK()
        {
            apiSignerInformationForEquifaxCanada1 = CreateTypicalAPISignerInformationForEquifaxCanada();
            sdkSignerInformationForEquifaxCanada1 = new SignerInformationForEquifaxCanadaConverter(apiSignerInformationForEquifaxCanada1).ToSDKSignerInformationForEquifaxCanada();

            Assert.AreEqual(sdkSignerInformationForEquifaxCanada1.FirstName, apiSignerInformationForEquifaxCanada1.FirstName);
            Assert.AreEqual(sdkSignerInformationForEquifaxCanada1.LastName, apiSignerInformationForEquifaxCanada1.LastName);
            Assert.AreEqual(sdkSignerInformationForEquifaxCanada1.StreetAddress, apiSignerInformationForEquifaxCanada1.StreetAddress);
            Assert.AreEqual(sdkSignerInformationForEquifaxCanada1.City, apiSignerInformationForEquifaxCanada1.City);
            Assert.AreEqual(sdkSignerInformationForEquifaxCanada1.Province, apiSignerInformationForEquifaxCanada1.Province);
            Assert.AreEqual(sdkSignerInformationForEquifaxCanada1.PostalCode, apiSignerInformationForEquifaxCanada1.PostalCode);
            Assert.AreEqual(sdkSignerInformationForEquifaxCanada1.TimeAtAddress, apiSignerInformationForEquifaxCanada1.TimeAtAddress);
            Assert.AreEqual(sdkSignerInformationForEquifaxCanada1.DriversLicenseNumber, apiSignerInformationForEquifaxCanada1.DriversLicenseNumber);
            Assert.AreEqual(sdkSignerInformationForEquifaxCanada1.SocialInsuranceNumber, apiSignerInformationForEquifaxCanada1.SocialInsuranceNumber);
            Assert.AreEqual(sdkSignerInformationForEquifaxCanada1.HomePhoneNumber, apiSignerInformationForEquifaxCanada1.HomePhoneNumber);
            Assert.AreEqual(sdkSignerInformationForEquifaxCanada1.DateOfBirth, apiSignerInformationForEquifaxCanada1.DateOfBirth);
        }

        [Test]
        public void ConvertSDKToAPI()
        {
            sdkSignerInformationForEquifaxCanada1 = CreateTypicalSDKSignerInformationForEquifaxCanada();
            apiSignerInformationForEquifaxCanada1 = new SignerInformationForEquifaxCanadaConverter(sdkSignerInformationForEquifaxCanada1).ToAPISignerInformationForEquifaxCanada();

            Assert.AreEqual(apiSignerInformationForEquifaxCanada1.FirstName, sdkSignerInformationForEquifaxCanada1.FirstName);
            Assert.AreEqual(apiSignerInformationForEquifaxCanada1.LastName, sdkSignerInformationForEquifaxCanada1.LastName);
            Assert.AreEqual(apiSignerInformationForEquifaxCanada1.StreetAddress, sdkSignerInformationForEquifaxCanada1.StreetAddress);
            Assert.AreEqual(apiSignerInformationForEquifaxCanada1.City, sdkSignerInformationForEquifaxCanada1.City);
            Assert.AreEqual(apiSignerInformationForEquifaxCanada1.Province, sdkSignerInformationForEquifaxCanada1.Province);
            Assert.AreEqual(apiSignerInformationForEquifaxCanada1.PostalCode, sdkSignerInformationForEquifaxCanada1.PostalCode);
            Assert.AreEqual(apiSignerInformationForEquifaxCanada1.TimeAtAddress, sdkSignerInformationForEquifaxCanada1.TimeAtAddress);
            Assert.AreEqual(apiSignerInformationForEquifaxCanada1.DriversLicenseNumber, sdkSignerInformationForEquifaxCanada1.DriversLicenseNumber);
            Assert.AreEqual(apiSignerInformationForEquifaxCanada1.SocialInsuranceNumber, sdkSignerInformationForEquifaxCanada1.SocialInsuranceNumber);
            Assert.AreEqual(apiSignerInformationForEquifaxCanada1.HomePhoneNumber, sdkSignerInformationForEquifaxCanada1.HomePhoneNumber);
            Assert.AreEqual(apiSignerInformationForEquifaxCanada1.DateOfBirth, sdkSignerInformationForEquifaxCanada1.DateOfBirth);
        }

        private OneSpanSign.Sdk.SignerInformationForEquifaxCanada CreateTypicalSDKSignerInformationForEquifaxCanada()
        {

            OneSpanSign.Sdk.SignerInformationForEquifaxCanada SignerInformationForEquifaxCanada = SignerInformationForEquifaxCanadaBuilder.NewSignerInformationForEquifaxCanada()
                .WithFirstName("Signer First Name")
                .WithLastName("Last Name")
                .WithStreetAddress("main street")
                .WithCity("Montreal")
                .WithProvince("Quebec")
                .WithPostalCode("H4L3K1")
                .WithTimeAtAddress(1)
                .WithDriversLicenseNumber("Driver's licence")
                .WithSocialInsuranceNumber("111-222-333-444")
                .WithHomePhoneNumber("1-800-123-8763")
                .WithDateOfBirth(new DateTime())
                .Build();

            return SignerInformationForEquifaxCanada;
        }

        private OneSpanSign.API.SignerInformationForEquifaxCanada CreateTypicalAPISignerInformationForEquifaxCanada()
        {
            OneSpanSign.API.SignerInformationForEquifaxCanada SignerInformationForEquifaxCanada = new OneSpanSign.API.SignerInformationForEquifaxCanada();
            SignerInformationForEquifaxCanada.FirstName = "first name";
            SignerInformationForEquifaxCanada.LastName = "last name";
            SignerInformationForEquifaxCanada.StreetAddress = "1234 main street";
            SignerInformationForEquifaxCanada.City = "Montreal";
            SignerInformationForEquifaxCanada.Province = "Quebec";
            SignerInformationForEquifaxCanada.PostalCode = "1h27r4";
            SignerInformationForEquifaxCanada.TimeAtAddress = 9;
            SignerInformationForEquifaxCanada.DriversLicenseNumber = "licence 222";
            SignerInformationForEquifaxCanada.SocialInsuranceNumber = "222-099-888-333";
            SignerInformationForEquifaxCanada.HomePhoneNumber = "877-098-0974";
            SignerInformationForEquifaxCanada.DateOfBirth = new DateTime();

            return SignerInformationForEquifaxCanada;
        }
    }
}




