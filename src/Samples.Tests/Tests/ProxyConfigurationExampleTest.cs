using System;
using NUnit.Framework;
using OneSpanSign.Sdk;
using System.Collections.Generic;

namespace SDK.Examples
{
    [TestFixture()]
    public class ProxyConfigurationExampleTest
    {
        [Test]
        [Category("Proxy")]
        [Ignore("Ignore a ProxyConfigurationExampleTest because could not find current proxy server")]
        public void verifyResult()
        {
            ProxyConfigurationExample example = new ProxyConfigurationExample(Props.GetInstance());
            example.Run();

            DocumentPackage documentPackage1 = example.ossClientWithHttpProxy.GetPackage(example.packageId1);
            DocumentPackage documentPackage2 = example.OssClient.GetPackage(example.packageId1);
            Assert.AreEqual(example.packageId1.Id, documentPackage1.Id.Id);
            Assert.AreEqual(example.packageId1.Id, documentPackage2.Id.Id);
            Assert.AreEqual(DocumentPackageStatus.COMPLETED, documentPackage1.Status);
            Assert.AreEqual(DocumentPackageStatus.COMPLETED, documentPackage2.Status);

            DocumentPackage documentPackage3 = example.ossClientWithHttpProxyHasCredentials.GetPackage(example.packageId2);
            DocumentPackage documentPackage4 = example.OssClient.GetPackage(example.packageId2);
            Assert.AreEqual(example.packageId2.Id, documentPackage3.Id.Id);
            Assert.AreEqual(example.packageId2.Id, documentPackage4.Id.Id);
            Assert.AreEqual (DocumentPackageStatus.COMPLETED, documentPackage3.Status);
            Assert.AreEqual (DocumentPackageStatus.COMPLETED, documentPackage4.Status);
        }
    }
}

