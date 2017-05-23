using NUnit.Framework;
using System;
using System.Collections.Generic;
using Silanis.ESL.API;

namespace SDK.Examples
{
    [TestFixture()]
    public class SignerVerificationTypeExampleTest
    {
        [Test()]
        public void VerifyResult()
        {
            SignerVerificationTypesExample example = new SignerVerificationTypesExample();
            example.Run();

            IList<VerificationType> verificationTypes = example.verificationTypes;
            IList<string> verificationTypeIds = new List<string>();

            foreach (VerificationType verificationType in verificationTypes)
            {
                verificationTypeIds.Add(verificationType.Id);
            }
            Assert.GreaterOrEqual(verificationTypes.Count, 1, "There is no Verification Type for this account.");
            Assert.IsTrue(verificationTypeIds.Contains("DIGIPASS"));
        }
    }
}
