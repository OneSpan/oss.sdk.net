using NUnit.Framework;

namespace SDK.Examples
{
    [TestFixture ()]
    public class AccountSettingsExampleTest
    {
        [Test ()]
        public void VerifyResult ()
        {
            AccountSettingsExample example = new AccountSettingsExample();
            example.Run ();
            
            Assert.IsTrue(example.defaultAccountSettings.AccountFeatureSettings.AllowCheckboxConsentApproval);
            Assert.IsFalse(example.defaultAccountSettings.AccountFeatureSettings.AllowInPersonForAccountSenders);
            Assert.IsTrue(example.defaultAccountSettings.AccountFeatureSettings.Attachments);
            Assert.IsTrue(example.defaultAccountSettings.AccountFeatureSettings.ConditionalFields);

            Assert.IsFalse(example.patchedAccountSettings.AccountFeatureSettings.AllowCheckboxConsentApproval);
            Assert.IsTrue(example.patchedAccountSettings.AccountFeatureSettings.AllowInPersonForAccountSenders);
            Assert.IsFalse(example.patchedAccountSettings.AccountFeatureSettings.Attachments);
            Assert.IsFalse(example.patchedAccountSettings.AccountFeatureSettings.ConditionalFields);

            Assert.IsTrue(example.deletedAccountSettings.AccountFeatureSettings.AllowCheckboxConsentApproval);
            Assert.IsFalse(example.deletedAccountSettings.AccountFeatureSettings.AllowInPersonForAccountSenders);
            Assert.IsTrue(example.deletedAccountSettings.AccountFeatureSettings.Attachments);
            Assert.IsTrue(example.deletedAccountSettings.AccountFeatureSettings.ConditionalFields);
            
            Assert.IsFalse(example.defaultAccountSettings.AccountPackageSettings.Ada);
            Assert.IsFalse(example.defaultAccountSettings.AccountPackageSettings.DeclineButton);
            Assert.IsFalse(example.defaultAccountSettings.AccountPackageSettings.DefaultTimeBasedExpiry);
            Assert.IsFalse(example.defaultAccountSettings.AccountPackageSettings.DisableDeclineOther);

            Assert.IsTrue(example.patchedAccountSettings.AccountPackageSettings.Ada);
            Assert.IsTrue(example.patchedAccountSettings.AccountPackageSettings.DeclineButton);
            Assert.IsTrue(example.patchedAccountSettings.AccountPackageSettings.DefaultTimeBasedExpiry);
            Assert.IsTrue(example.patchedAccountSettings.AccountPackageSettings.DisableDeclineOther);

            Assert.IsFalse(example.deletedAccountSettings.AccountPackageSettings.Ada);
            Assert.IsFalse(example.deletedAccountSettings.AccountPackageSettings.DeclineButton);
            Assert.IsFalse(example.deletedAccountSettings.AccountPackageSettings.DefaultTimeBasedExpiry);
            Assert.IsFalse(example.deletedAccountSettings.AccountPackageSettings.DisableDeclineOther);
            
            Assert.IsTrue(example.defaultAccountFeatureSettings.AllowCheckboxConsentApproval);
            Assert.IsFalse(example.defaultAccountFeatureSettings.AllowInPersonForAccountSenders);
            Assert.IsTrue(example.defaultAccountFeatureSettings.Attachments);
            Assert.IsTrue(example.defaultAccountFeatureSettings.ConditionalFields);

            Assert.IsFalse(example.patchedAccountFeatureSettings.AllowCheckboxConsentApproval);
            Assert.IsTrue(example.patchedAccountFeatureSettings.AllowInPersonForAccountSenders);
            Assert.IsFalse(example.patchedAccountFeatureSettings.Attachments);
            Assert.IsFalse(example.patchedAccountFeatureSettings.ConditionalFields);

            Assert.IsTrue(example.deletedAccountFeatureSettings.AllowCheckboxConsentApproval);
            Assert.IsFalse(example.deletedAccountFeatureSettings.AllowInPersonForAccountSenders);
            Assert.IsTrue(example.deletedAccountFeatureSettings.Attachments);
            Assert.IsTrue(example.deletedAccountFeatureSettings.ConditionalFields);

            Assert.IsFalse(example.defaultAccountPackageSettings.Ada);
            Assert.IsFalse(example.defaultAccountPackageSettings.DeclineButton);
            Assert.IsFalse(example.defaultAccountPackageSettings.DefaultTimeBasedExpiry);
            Assert.IsFalse(example.defaultAccountPackageSettings.DisableDeclineOther);

            Assert.IsTrue(example.patchedAccountPackageSettings.Ada);
            Assert.IsTrue(example.patchedAccountPackageSettings.DeclineButton);
            Assert.IsTrue(example.patchedAccountPackageSettings.DefaultTimeBasedExpiry);
            Assert.IsTrue(example.patchedAccountPackageSettings.DisableDeclineOther);

            Assert.IsFalse(example.deletedAccountPackageSettings.Ada);
            Assert.IsFalse(example.deletedAccountPackageSettings.DeclineButton);
            Assert.IsFalse(example.deletedAccountPackageSettings.DefaultTimeBasedExpiry);
            Assert.IsFalse(example.deletedAccountPackageSettings.DisableDeclineOther);
        }
    }
}