using NUnit.Framework;
using System;

namespace SDK.Examples
{
    [TestFixture ()]
    public class SigningStyleExampleTest
    {
        [Test ()]
        public void VerifyResult ()
        {
            object actualTheme;
            
            SigningStyleExample example = new SigningStyleExample();
            example.Run ();

            Assert.IsNotNull (example.createdSigningThemes);
            Assert.IsNotNull (example.createdSigningThemes.ContainsKey ("default"));
            example.createdSigningThemes.TryGetValue ("default", out actualTheme);
            StringAssert.Contains ("\"primary\": \"#5940C3\"", actualTheme.ToString());

            Assert.IsNotNull (example.retrievedSigningThemes);
            Assert.IsNotNull (example.retrievedSigningThemes.ContainsKey ("default"));
            example.retrievedSigningThemes.TryGetValue ("default", out actualTheme);
            StringAssert.Contains ("\"primary\": \"#5940C3\"", actualTheme.ToString ());

            Assert.IsNotNull (example.updatedSigningThemes);
            Assert.IsNotNull (example.updatedSigningThemes.ContainsKey ("default"));
            example.updatedSigningThemes.TryGetValue ("default", out actualTheme);
            StringAssert.Contains ("\"secondary\": \"#F31C8B\"", actualTheme.ToString ());

            CollectionAssert.IsEmpty (example.removedSigningThemes);

            Assert.IsNotNull (example.createdSigningLogos [0]);
            Assert.AreEqual (example.createdSigningLogos [0].Language, "en");

            Assert.IsNotNull (example.updatedSigningLogos [1]);
            Assert.AreEqual (example.updatedSigningLogos [1].Language, "fr");

          //  Assert.IsEmpty (example.removedSigningLogos);
            
            Assert.IsNotNull (example.defaultSigningUiOptions);
            Assert.IsTrue(example.defaultSigningUiOptions.OverviewOptions.Body);
            Assert.IsTrue(example.defaultSigningUiOptions.OverviewOptions.Title);
            Assert.IsTrue(example.defaultSigningUiOptions.OverviewOptions.DocumentSection);
            Assert.IsTrue(example.defaultSigningUiOptions.OverviewOptions.UploadSection);
            
            Assert.IsNotNull (example.patchedSigningUiOptions);
            Assert.IsFalse(example.patchedSigningUiOptions.OverviewOptions.Body);
            Assert.IsFalse(example.patchedSigningUiOptions.OverviewOptions.Title);
            Assert.IsFalse(example.patchedSigningUiOptions.OverviewOptions.DocumentSection);
            Assert.IsFalse(example.patchedSigningUiOptions.OverviewOptions.UploadSection);

            Assert.IsNotNull (example.deletedSigningUiOptions);
            Assert.IsTrue(example.deletedSigningUiOptions.OverviewOptions.Body);
            Assert.IsTrue(example.deletedSigningUiOptions.OverviewOptions.Title);
            Assert.IsTrue(example.deletedSigningUiOptions.OverviewOptions.DocumentSection);
            Assert.IsTrue(example.deletedSigningUiOptions.OverviewOptions.UploadSection);
        }
    }
}