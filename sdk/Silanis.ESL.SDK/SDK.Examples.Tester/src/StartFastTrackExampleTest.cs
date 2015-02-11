using NUnit.Framework;
using System;

namespace SDK.Examples
{
    [TestFixture()]
    public class StartFastTrackExampleTest
    {
        [Test()]
        public void VerifyResult()
        {
            StartFastTrackExample example = new StartFastTrackExample(Props.GetInstance());
            example.Run();

            Assert.IsNotNull(example.signingUrl);
            Assert.IsNotEmpty(example.signingUrl);
        }
    }
}

