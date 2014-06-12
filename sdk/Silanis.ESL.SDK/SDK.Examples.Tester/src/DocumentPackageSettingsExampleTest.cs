using System;
using NUnit.Framework;
using Silanis.ESL.SDK;

namespace SDK.Examples
{
    [TestFixture()]
    public class DocumentPackageSettingsExampleTest
    {
        [Test()]
        public void VerifyResult()
        {
            DocumentPackageSettingsExample example = new DocumentPackageSettingsExample( Props.GetInstance() );
            example.Run();
            
            DocumentPackage result = example.EslClient.GetPackage( example.PackageId );
            
            Assert.IsTrue( result.Settings.EnableFirstAffidavit.HasValue);
            Assert.IsFalse( result.Settings.EnableFirstAffidavit.Value );
            
            Assert.IsTrue( result.Settings.EnableSecondAffidavit.HasValue);
            Assert.IsFalse( result.Settings.EnableSecondAffidavit.Value );
            
            Assert.IsTrue( result.Settings.ShowLanguageDropDown.HasValue);
            Assert.IsFalse( result.Settings.ShowLanguageDropDown.Value );
            
            Assert.IsTrue( result.Settings.ShowOwnerInPersonDropDown.HasValue);
            Assert.IsFalse( result.Settings.ShowOwnerInPersonDropDown.Value );
        }
    }
}

