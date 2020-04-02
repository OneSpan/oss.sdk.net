using System;
using NUnit.Framework;
using OneSpanSign.Sdk.Internal;

namespace SDK.Examples
{
    [TestFixture()]
    public class PackageViewRedirectForPackageSenderExampleTest
    {
        [Test()]
        public void VerifyResult()
        {
            PackageViewRedirectForPackageSenderExample example = new PackageViewRedirectForPackageSenderExample();
            example.Run();

            Assert.IsNotEmpty(example.generatedLinkToPackageViewForSender);
        }
    }
}

