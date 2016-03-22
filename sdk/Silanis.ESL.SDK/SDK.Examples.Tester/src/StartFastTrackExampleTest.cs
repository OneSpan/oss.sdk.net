using NUnit.Framework;
using System;
using Silanis.ESL.SDK.Internal;

namespace SDK.Examples
{
    [TestFixture()]
    public class StartFastTrackExampleTest
    {
        [Test()]
        public void VerifyResult()
        {
            StartFastTrackExample example = new StartFastTrackExample();
            example.Run();

            Assert.IsNotNull(example.signingUrl);
            Assert.IsNotEmpty(example.signingUrl);
            
            string stringResponse1 = HttpRequestUtil.GetUrlContent(example.signingUrl);
            StringAssert.Contains("Electronic Disclosures and Signatures Consent", stringResponse1);
        }
    }
}

