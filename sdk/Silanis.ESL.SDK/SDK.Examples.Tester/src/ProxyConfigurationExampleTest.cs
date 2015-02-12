using System;
using NUnit.Framework;
using Silanis.ESL.SDK;
using NUnit.Framework.SyntaxHelpers;
using System.Collections.Generic;

namespace SDK.Examples
{
    public class ProxyConfigurationExampleTest
    {
        [Test]
        public void verifyResult()
        {
            ProxyConfigurationExample example = new ProxyConfigurationExample(Props.GetInstance());

            example.ExecuteViaHttpProxy();
            DocumentPackage documentPackage1 = example.eslClientWithHttpProxy.GetPackage(example.PackageId);
            DocumentPackage documentPackage2 = example.EslClient.GetPackage(example.PackageId);
            Assert.AreEqual(example.PackageId.Id, documentPackage1.Id.Id);
            Assert.AreEqual(example.PackageId.Id, documentPackage2.Id.Id);

            example.ExecuteViaHttpProxyWithCredentials();
            DocumentPackage documentPackage3 = example.eslClientWithHttpProxyHasCredentials.GetPackage(example.PackageId);
            DocumentPackage documentPackage4 = example.EslClient.GetPackage(example.PackageId);
            Assert.AreEqual(example.PackageId.Id, documentPackage3.Id.Id);
            Assert.AreEqual(example.PackageId.Id, documentPackage4.Id.Id);
        }
    }
}

