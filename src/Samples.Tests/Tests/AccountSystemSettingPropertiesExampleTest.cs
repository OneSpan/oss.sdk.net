using NUnit.Framework;

namespace SDK.Examples
{
    [TestFixture ()]
    public class AccountSystemSettingPropertiesExampleTest
    {
        [Test ()]
        public void VerifyResult ()
        {
            AccountSystemSettingPropertiesExample example = new AccountSystemSettingPropertiesExample();
            example.Run ();
            
            Assert.IsNotNull(example.defaultAccountSystemSettingProperties.SenderLoginMaxFailedAttempts);
            Assert.IsNotNull(example.defaultAccountSystemSettingProperties.LoginSessionTimeout);
            Assert.IsNotNull(example.defaultAccountSystemSettingProperties.SessionTimeoutWarning);
            
            Assert.IsTrue(example.patchedAccountSystemSettingProperties.SenderLoginMaxFailedAttempts.Equals(2));
            Assert.IsTrue(example.patchedAccountSystemSettingProperties.LoginSessionTimeout == 60000);
            Assert.IsTrue(example.patchedAccountSystemSettingProperties.SessionTimeoutWarning == 200000);
        }
    }
}