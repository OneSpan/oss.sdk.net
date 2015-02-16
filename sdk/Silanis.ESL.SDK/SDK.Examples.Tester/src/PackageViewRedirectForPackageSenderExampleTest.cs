using System;
using NUnit.Framework;
using Silanis.ESL.SDK.Internal;

namespace SDK.Examples
{
    [TestFixture()]
    public class PackageViewRedirectForPackageSenderExampleTest
    {
        [Test()]
        public void VerifyResult()
        {
            PackageViewRedirectForPackageSenderExample example = new PackageViewRedirectForPackageSenderExample(Props.GetInstance());
            example.Run();

            Assert.IsNotNull(example.generatedLinkToPackageViewForSender);

            string stringResponse = HttpRequestUtil.GetUrlContent(example.generatedLinkToPackageViewForSender);
            StringAssert.Contains(example.PACKAGE_NAME, stringResponse);
        }
    }
}

