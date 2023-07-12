using NUnit.Framework;
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
    public class SignerInformationForLexisNexisExampleTest
    {
        [Test()]
        public void VerifyResult()
        {
            SignerInformationForLexisNexisExample example = new SignerInformationForLexisNexisExample();
            example.Run();

            DocumentPackage documentPackage = example.RetrievedPackage;

            SignerInformationForLexisNexis signerInformationForLexisNexis = documentPackage.GetSigner(example.email1).KnowledgeBasedAuthentication.SignerInformationForLexisNexis;

            Assert.AreEqual(signerInformationForLexisNexis.FirstName, example.FIRST_NAME);
            Assert.AreEqual(signerInformationForLexisNexis.LastName, example.LAST_NAME);
            Assert.AreEqual(signerInformationForLexisNexis.FlatOrApartmentNumber, example.FLAT_OR_APARTMENT_NUMBER);
            Assert.AreEqual(signerInformationForLexisNexis.HouseName, example.HOUSE_NAME);
            Assert.AreEqual(signerInformationForLexisNexis.HouseNumber, example.HOUSE_NUMBER);
            Assert.AreEqual(signerInformationForLexisNexis.City, example.CITY);
            Assert.AreEqual(signerInformationForLexisNexis.State, example.STATE);
            Assert.AreEqual(signerInformationForLexisNexis.Zip, example.ZIP);
            Assert.AreEqual(signerInformationForLexisNexis.SocialSecurityNumber, example.SOCIAL_SECURITY_NUMBER);
            Assert.AreEqual(signerInformationForLexisNexis.DateOfBirth, example.DATE_OF_BIRTH);
        }
    }
}


