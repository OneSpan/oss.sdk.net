using NUnit.Framework;
using OneSpanSign.Sdk;

namespace SDK.Examples
{
    [TestFixture ()]
    public class ChooseSignatureSettingsExampleTest
    {
        [Test ()]
        public void VerifyResult ()
        {
            ChooseSignatureSettingsExample example = new ChooseSignatureSettingsExample();
            example.Run ();

            Assert.IsNotNull (example.defaultChooseSignatureSettings);
            Assert.IsTrue(example.defaultChooseSignatureSettings.Signature.AllowStyling);
            Assert.IsTrue(example.defaultChooseSignatureSettings.Signature.AllowDrawing);
            Assert.IsTrue(example.defaultChooseSignatureSettings.Signature.AllowUploading);
            Assert.IsTrue(example.defaultChooseSignatureSettings.Signature.AllowMobileSigning);
            Assert.AreEqual(example.defaultChooseSignatureSettings.Signature.DefaultSignatureType, ChooseSignatureStyleType.STYLE.ToString());
            
            Assert.IsNotNull (example.chooseSignatureSettingsAfterPatch);
            Assert.IsTrue(example.chooseSignatureSettingsAfterPatch.Signature.AllowStyling);
            Assert.IsFalse(example.chooseSignatureSettingsAfterPatch.Signature.AllowDrawing);
            Assert.IsFalse(example.chooseSignatureSettingsAfterPatch.Signature.AllowUploading);
            Assert.IsFalse(example.chooseSignatureSettingsAfterPatch.Signature.AllowMobileSigning);
            Assert.AreEqual(example.chooseSignatureSettingsAfterPatch.Signature.DefaultSignatureType, ChooseSignatureStyleType.DRAW.ToString());
            
            Assert.IsNotNull (example.chooseSignatureSettingsAfterDelete);
            Assert.IsTrue(example.chooseSignatureSettingsAfterDelete.Signature.AllowStyling);
            Assert.IsTrue(example.chooseSignatureSettingsAfterDelete.Signature.AllowDrawing);
            Assert.IsTrue(example.chooseSignatureSettingsAfterDelete.Signature.AllowUploading);
            Assert.IsTrue(example.chooseSignatureSettingsAfterDelete.Signature.AllowMobileSigning);
            Assert.AreEqual(example.chooseSignatureSettingsAfterDelete.Signature.DefaultSignatureType, ChooseSignatureStyleType.STYLE.ToString());

        }
    }
}