using NUnit.Framework;
using System;
using Silanis.ESL.SDK.Internal;

namespace SDK.Examples
{
    [TestFixture()]
    public class GetSigningUrlExampleTest
    {
        [Test()]
        public void VerifyResult()
        {
            GetSigningUrlExample example = new GetSigningUrlExample(Props.GetInstance());
            example.Run();

            Assert.IsNotNull(example.signingUrlForSigner1);
            Assert.IsNotEmpty(example.signingUrlForSigner1);
            Assert.IsNotNull(example.signingUrlForSigner2);
            Assert.IsNotEmpty(example.signingUrlForSigner2);

            string stringResponse1 = HttpRequestUtil.GetUrlContent(example.signingUrlForSigner1);
            StringAssert.Contains("Electronic Disclosures and Signatures Consent", stringResponse1);

            string stringResponse2 = HttpRequestUtil.GetUrlContent(example.signingUrlForSigner2);
            StringAssert.Contains("Electronic Disclosures and Signatures Consent", stringResponse2);
        }
    }
}

