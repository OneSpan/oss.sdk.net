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
    public class MixedSignerAuthenticationExampleTest
    {
        [Test()]
        public void VerifyResult()
        {
            MixedSignerAuthenticationExample example = new MixedSignerAuthenticationExample();
            example.Run();

            DocumentPackage documentPackage = example.RetrievedPackage;

            Signer canadianSigner = documentPackage.GetSigner(example.SIGNER_WITH_AUTHENTICATION_EQUIFAX_CANADA.Email);
            SignerInformationForEquifaxCanada canadianSignerInformationForEquifaxCanada = canadianSigner.KnowledgeBasedAuthentication.SignerInformationForEquifaxCanada;

            Assert.AreEqual(canadianSignerInformationForEquifaxCanada.FirstName, example.SIGNER_WITH_AUTHENTICATION_EQUIFAX_CANADA.KnowledgeBasedAuthentication.SignerInformationForEquifaxCanada.FirstName);
            Assert.AreEqual(canadianSignerInformationForEquifaxCanada.LastName, example.SIGNER_WITH_AUTHENTICATION_EQUIFAX_CANADA.KnowledgeBasedAuthentication.SignerInformationForEquifaxCanada.LastName);
            Assert.AreEqual(canadianSignerInformationForEquifaxCanada.StreetAddress, example.SIGNER_WITH_AUTHENTICATION_EQUIFAX_CANADA.KnowledgeBasedAuthentication.SignerInformationForEquifaxCanada.StreetAddress);
            Assert.AreEqual(canadianSignerInformationForEquifaxCanada.City, example.SIGNER_WITH_AUTHENTICATION_EQUIFAX_CANADA.KnowledgeBasedAuthentication.SignerInformationForEquifaxCanada.City);
            Assert.AreEqual(canadianSignerInformationForEquifaxCanada.Province, example.SIGNER_WITH_AUTHENTICATION_EQUIFAX_CANADA.KnowledgeBasedAuthentication.SignerInformationForEquifaxCanada.Province);
            Assert.AreEqual(canadianSignerInformationForEquifaxCanada.PostalCode, example.SIGNER_WITH_AUTHENTICATION_EQUIFAX_CANADA.KnowledgeBasedAuthentication.SignerInformationForEquifaxCanada.PostalCode);
            Assert.AreEqual(canadianSignerInformationForEquifaxCanada.TimeAtAddress, example.SIGNER_WITH_AUTHENTICATION_EQUIFAX_CANADA.KnowledgeBasedAuthentication.SignerInformationForEquifaxCanada.TimeAtAddress);
            Assert.AreEqual(canadianSignerInformationForEquifaxCanada.DriversLicenseNumber, example.SIGNER_WITH_AUTHENTICATION_EQUIFAX_CANADA.KnowledgeBasedAuthentication.SignerInformationForEquifaxCanada.DriversLicenseNumber);
            Assert.AreEqual(canadianSignerInformationForEquifaxCanada.SocialInsuranceNumber, example.SIGNER_WITH_AUTHENTICATION_EQUIFAX_CANADA.KnowledgeBasedAuthentication.SignerInformationForEquifaxCanada.SocialInsuranceNumber);
            Assert.AreEqual(canadianSignerInformationForEquifaxCanada.HomePhoneNumber, example.SIGNER_WITH_AUTHENTICATION_EQUIFAX_CANADA.KnowledgeBasedAuthentication.SignerInformationForEquifaxCanada.HomePhoneNumber);
            Assert.AreEqual(canadianSignerInformationForEquifaxCanada.DateOfBirth, example.SIGNER_WITH_AUTHENTICATION_EQUIFAX_CANADA.KnowledgeBasedAuthentication.SignerInformationForEquifaxCanada.DateOfBirth);

            // Note that for security reasons, the backend doesn't return challenge answers, so we don't verify the answers here.
            foreach (Challenge challenge in canadianSigner.ChallengeQuestion)
            {
                Assert.IsTrue(String.Equals(challenge.Question, example.SIGNER_WITH_AUTHENTICATION_EQUIFAX_CANADA.ChallengeQuestion[0].Question) || String.Equals(challenge.Question, example.SIGNER_WITH_AUTHENTICATION_EQUIFAX_CANADA.ChallengeQuestion[1].Question));
            }

            Signer usaSigner = documentPackage.GetSigner(example.SIGNER_WITH_AUTHENTICATION_EQUIFAX_USA.Email);
            SignerInformationForEquifaxUSA usaSignerInformationForEquifaxUSA = usaSigner.KnowledgeBasedAuthentication.SignerInformationForEquifaxUSA;

            Assert.AreEqual(usaSignerInformationForEquifaxUSA.FirstName, example.SIGNER_WITH_AUTHENTICATION_EQUIFAX_USA.KnowledgeBasedAuthentication.SignerInformationForEquifaxUSA.FirstName);
            Assert.AreEqual(usaSignerInformationForEquifaxUSA.LastName, example.SIGNER_WITH_AUTHENTICATION_EQUIFAX_USA.KnowledgeBasedAuthentication.SignerInformationForEquifaxUSA.LastName);
            Assert.AreEqual(usaSignerInformationForEquifaxUSA.StreetAddress, example.SIGNER_WITH_AUTHENTICATION_EQUIFAX_USA.KnowledgeBasedAuthentication.SignerInformationForEquifaxUSA.StreetAddress);
            Assert.AreEqual(usaSignerInformationForEquifaxUSA.City, example.SIGNER_WITH_AUTHENTICATION_EQUIFAX_USA.KnowledgeBasedAuthentication.SignerInformationForEquifaxUSA.City);
            Assert.AreEqual(usaSignerInformationForEquifaxUSA.State, example.SIGNER_WITH_AUTHENTICATION_EQUIFAX_USA.KnowledgeBasedAuthentication.SignerInformationForEquifaxUSA.State);
            Assert.AreEqual(usaSignerInformationForEquifaxUSA.Zip, example.SIGNER_WITH_AUTHENTICATION_EQUIFAX_USA.KnowledgeBasedAuthentication.SignerInformationForEquifaxUSA.Zip);
            Assert.AreEqual(usaSignerInformationForEquifaxUSA.SocialSecurityNumber, example.SIGNER_WITH_AUTHENTICATION_EQUIFAX_USA.KnowledgeBasedAuthentication.SignerInformationForEquifaxUSA.SocialSecurityNumber);
            Assert.AreEqual(usaSignerInformationForEquifaxUSA.HomePhoneNumber, example.SIGNER_WITH_AUTHENTICATION_EQUIFAX_USA.KnowledgeBasedAuthentication.SignerInformationForEquifaxUSA.HomePhoneNumber);
            Assert.AreEqual(usaSignerInformationForEquifaxUSA.DateOfBirth, example.SIGNER_WITH_AUTHENTICATION_EQUIFAX_USA.KnowledgeBasedAuthentication.SignerInformationForEquifaxUSA.DateOfBirth);
            Assert.AreEqual(usaSignerInformationForEquifaxUSA.TimeAtAddress, example.SIGNER_WITH_AUTHENTICATION_EQUIFAX_USA.KnowledgeBasedAuthentication.SignerInformationForEquifaxUSA.TimeAtAddress);
            Assert.AreEqual(usaSignerInformationForEquifaxUSA.DriversLicenseNumber, example.SIGNER_WITH_AUTHENTICATION_EQUIFAX_USA.KnowledgeBasedAuthentication.SignerInformationForEquifaxUSA.DriversLicenseNumber);

            foreach (Challenge challenge in usaSigner.ChallengeQuestion)
            {
                Assert.IsTrue(String.Equals(challenge.Question, example.SIGNER_WITH_AUTHENTICATION_EQUIFAX_USA.ChallengeQuestion[0].Question) || String.Equals(challenge.Question, example.SIGNER_WITH_AUTHENTICATION_EQUIFAX_USA.ChallengeQuestion[1].Question));
            }

        }
    }
}

