using NUnit.Framework;

namespace SDK.Examples
{

    [TestFixture ()]
    public class AccountUploadSettingsExampleTest
    {
        [Test ()]
        public void VerifyResult ()
        {
            AccountUploadSettingsExample example = new AccountUploadSettingsExample();
            example.Run ();
            
            Assert.IsNotNull(example.defaultAccountUploadSettings.AllowedFileTypes);
            
            Assert.IsTrue(example.patchedAccountUploadSettings.AllowedFileTypes.Contains("TESTFILETYPE1"));
            Assert.IsTrue(example.patchedAccountUploadSettings.AllowedFileTypes.Contains("TESTFILETYPE2"));
            Assert.IsFalse(example.patchedAccountUploadSettings.AllowedFileTypes.Contains("TESTFILETYPE3"));
        }
    }
}