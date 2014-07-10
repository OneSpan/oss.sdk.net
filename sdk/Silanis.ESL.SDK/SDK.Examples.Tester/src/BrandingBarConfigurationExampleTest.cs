using NUnit.Framework;
using System;
using Silanis.ESL.SDK;

namespace SDK.Examples
{
    [TestFixture()]
    public class BrandingBarConfigurationExampleTest
    {
        [Test()]
        public void VerifyResult()
        {
            BrandingBarConfigurationExample example = new BrandingBarConfigurationExample(Props.GetInstance());
            example.Run();

            DocumentPackage documentPackage = example.EslClient.GetPackage(example.PackageId);

            Assert.AreEqual(documentPackage.Settings.EnableOptOut, false);
            Assert.AreEqual(documentPackage.Settings.ShowDownloadButton, false);
            Assert.AreEqual(documentPackage.Settings.CeremonyLayoutSettings.GlobalNavigation, false);
            Assert.AreEqual(documentPackage.Settings.CeremonyLayoutSettings.ShowGlobalDownloadButton, false);
        }
    }
}

