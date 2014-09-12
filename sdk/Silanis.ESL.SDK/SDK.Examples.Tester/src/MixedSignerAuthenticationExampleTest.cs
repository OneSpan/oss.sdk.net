using NUnit.Framework;
using System;
using Silanis.ESL.SDK;
using System.Collections.Generic;
using Silanis.ESL.API;
using Silanis.ESL.SDK.Internal;
using System.IO;
using Silanis.ESL.SDK.Builder;
using Silanis.ESL.SDK.Builder.Internal;
using System.Text;


namespace SDK.Examples
{
    [TestFixture()]
    public class MixedSignerAuthenticationExampleTest
    {
        [Test()]
        public void VerifyResult()
        {
            MixedSignerAuthenticationExample example = new MixedSignerAuthenticationExample(Props.GetInstance());
            example.Run();

            DocumentPackage documentPackage = example.EslClient.GetPackage(example.PackageId);

            SignerInformationForEquifaxCanada signerInformationForEquifaxCanada = documentPackage.Signers[example.SIGNER_WITH_AUTHENTICATION_EQUIFAX_CANADA.Email].KnowledgeBasedAuthentication.SignerInformationForEquifaxCanada;

            Assert.AreEqual(signerInformationForEquifaxCanada.FirstName, example.SIGNER_WITH_AUTHENTICATION_EQUIFAX_CANADA.KnowledgeBasedAuthentication.SignerInformationForEquifaxCanada.FirstName);
            Assert.AreEqual(signerInformationForEquifaxCanada.LastName, example.SIGNER_WITH_AUTHENTICATION_EQUIFAX_CANADA.KnowledgeBasedAuthentication.SignerInformationForEquifaxCanada.LastName);
            Assert.AreEqual(signerInformationForEquifaxCanada.StreetAddress, example.SIGNER_WITH_AUTHENTICATION_EQUIFAX_CANADA.KnowledgeBasedAuthentication.SignerInformationForEquifaxCanada.StreetAddress);
            Assert.AreEqual(signerInformationForEquifaxCanada.City, example.SIGNER_WITH_AUTHENTICATION_EQUIFAX_CANADA.KnowledgeBasedAuthentication.SignerInformationForEquifaxCanada.City);
            Assert.AreEqual(signerInformationForEquifaxCanada.Province, example.SIGNER_WITH_AUTHENTICATION_EQUIFAX_CANADA.KnowledgeBasedAuthentication.SignerInformationForEquifaxCanada.Province);
            Assert.AreEqual(signerInformationForEquifaxCanada.PostalCode, example.SIGNER_WITH_AUTHENTICATION_EQUIFAX_CANADA.KnowledgeBasedAuthentication.SignerInformationForEquifaxCanada.PostalCode);
            Assert.AreEqual(signerInformationForEquifaxCanada.TimeAtAddress, example.SIGNER_WITH_AUTHENTICATION_EQUIFAX_CANADA.KnowledgeBasedAuthentication.SignerInformationForEquifaxCanada.TimeAtAddress);
            Assert.AreEqual(signerInformationForEquifaxCanada.DriversLicenseIndicator, example.SIGNER_WITH_AUTHENTICATION_EQUIFAX_CANADA.KnowledgeBasedAuthentication.SignerInformationForEquifaxCanada.DriversLicenseIndicator);
            Assert.AreEqual(signerInformationForEquifaxCanada.SocialInsuranceNumber, example.SIGNER_WITH_AUTHENTICATION_EQUIFAX_CANADA.KnowledgeBasedAuthentication.SignerInformationForEquifaxCanada.SocialInsuranceNumber);
            Assert.AreEqual(signerInformationForEquifaxCanada.HomePhoneNumber, example.SIGNER_WITH_AUTHENTICATION_EQUIFAX_CANADA.KnowledgeBasedAuthentication.SignerInformationForEquifaxCanada.HomePhoneNumber);
            Assert.AreEqual(signerInformationForEquifaxCanada.DateOfBirth, example.SIGNER_WITH_AUTHENTICATION_EQUIFAX_CANADA.KnowledgeBasedAuthentication.SignerInformationForEquifaxCanada.DateOfBirth);

            SignerInformationForEquifaxUSA signerInformationForEquifaxUSA = documentPackage.Signers[example.SIGNER_WITH_AUTHENTICATION_EQUIFAX_USA.Email].KnowledgeBasedAuthentication.SignerInformationForEquifaxUSA;
            Assert.AreEqual(signerInformationForEquifaxUSA.FirstName, example.SIGNER_WITH_AUTHENTICATION_EQUIFAX_USA.KnowledgeBasedAuthentication.SignerInformationForEquifaxUSA.FirstName);
            Assert.AreEqual(signerInformationForEquifaxUSA.LastName, example.SIGNER_WITH_AUTHENTICATION_EQUIFAX_USA.KnowledgeBasedAuthentication.SignerInformationForEquifaxUSA.LastName);
            Assert.AreEqual(signerInformationForEquifaxUSA.StreetAddress, example.SIGNER_WITH_AUTHENTICATION_EQUIFAX_USA.KnowledgeBasedAuthentication.SignerInformationForEquifaxUSA.StreetAddress);
            Assert.AreEqual(signerInformationForEquifaxUSA.City, example.SIGNER_WITH_AUTHENTICATION_EQUIFAX_USA.KnowledgeBasedAuthentication.SignerInformationForEquifaxUSA.City);
            Assert.AreEqual(signerInformationForEquifaxUSA.State, example.SIGNER_WITH_AUTHENTICATION_EQUIFAX_USA.KnowledgeBasedAuthentication.SignerInformationForEquifaxUSA.State);
            Assert.AreEqual(signerInformationForEquifaxUSA.Zip, example.SIGNER_WITH_AUTHENTICATION_EQUIFAX_USA.KnowledgeBasedAuthentication.SignerInformationForEquifaxUSA.Zip);
            Assert.AreEqual(signerInformationForEquifaxUSA.SocialSecurityNumber, example.SIGNER_WITH_AUTHENTICATION_EQUIFAX_USA.KnowledgeBasedAuthentication.SignerInformationForEquifaxUSA.SocialSecurityNumber);
            Assert.AreEqual(signerInformationForEquifaxUSA.HomePhoneNumber, example.SIGNER_WITH_AUTHENTICATION_EQUIFAX_USA.KnowledgeBasedAuthentication.SignerInformationForEquifaxUSA.HomePhoneNumber);
            Assert.AreEqual(signerInformationForEquifaxUSA.DateOfBirth, example.SIGNER_WITH_AUTHENTICATION_EQUIFAX_USA.KnowledgeBasedAuthentication.SignerInformationForEquifaxUSA.DateOfBirth);

            Signer signerQnA = documentPackage.Signers[example.SIGNER_WITH_AUTHENTICATION_Q_AND_A.Email];
            // Note that for security reasons, the backend doesn't return challenge answers, so we don't verify the answers here.
            foreach (Challenge challenge in signerQnA.ChallengeQuestion)
            {
                Assert.IsTrue(String.Equals(challenge.Question, example.SIGNER_WITH_AUTHENTICATION_Q_AND_A.ChallengeQuestion[0].Question) || String.Equals(challenge.Question, example.SIGNER_WITH_AUTHENTICATION_Q_AND_A.ChallengeQuestion[1].Question));
            }
        }
    }
}

