﻿using NUnit.Framework;
using System;
using OneSpanSign.Sdk;
using System.Collections.Generic;
using OneSpanSign.API;
using OneSpanSign.Sdk.Internal;
using System.IO;
using OneSpanSign.Sdk.Builder;
using OneSpanSign.Sdk.Builder.Internal;
using System.Text;

namespace SDK.Examples
{
    [TestFixture()]
    public class SignerInformationForEquifaxUSAExampleTest
    {
        [Test()]
        public void VerifyResult()
        {
            SignerInformationForEquifaxUSAExample example = new SignerInformationForEquifaxUSAExample();
            example.Run();

            DocumentPackage documentPackage = example.RetrievedPackage;

            SignerInformationForEquifaxUSA signerInformationForEquifaxUSA = documentPackage.GetSigner(example.email1).KnowledgeBasedAuthentication.SignerInformationForEquifaxUSA;

            Assert.AreEqual(signerInformationForEquifaxUSA.FirstName, example.FIRST_NAME);
            Assert.AreEqual(signerInformationForEquifaxUSA.LastName, example.LAST_NAME);
            Assert.AreEqual(signerInformationForEquifaxUSA.StreetAddress, example.STREET_ADDRESS);
            Assert.AreEqual(signerInformationForEquifaxUSA.City, example.CITY);
            Assert.AreEqual(signerInformationForEquifaxUSA.State, example.STATE);
            Assert.AreEqual(signerInformationForEquifaxUSA.Zip, example.ZIP);
            Assert.AreEqual(signerInformationForEquifaxUSA.TimeAtAddress, example.TIME_AT_ADDRESS);
            Assert.AreEqual(signerInformationForEquifaxUSA.SocialSecurityNumber, example.SOCIAL_SECURITY_NUMBER);
            Assert.AreEqual(signerInformationForEquifaxUSA.HomePhoneNumber, example.HOME_PHONE_NUMBER);
            Assert.AreEqual(signerInformationForEquifaxUSA.DateOfBirth, example.DATE_OF_BIRTH);
            Assert.AreEqual(signerInformationForEquifaxUSA.DriversLicenseNumber, example.DRIVERS_LICENSE_NUMBER);
        }
    }
}


