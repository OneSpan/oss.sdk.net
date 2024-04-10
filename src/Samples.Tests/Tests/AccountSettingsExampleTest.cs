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
            
            Assert.IsNotNull(example.defaultAccountSettings.AccountFeatureSettings.AllowCheckboxConsentApproval);
            Assert.IsNotNull(example.defaultAccountSettings.AccountFeatureSettings.AllowInPersonForAccountSenders);
            Assert.IsNotNull(example.defaultAccountSettings.AccountFeatureSettings.Attachments);
            Assert.IsNotNull(example.defaultAccountSettings.AccountFeatureSettings.ConditionalFields);
            Assert.IsNotNull(example.defaultAccountSettings.AccountFeatureSettings.EnableRecipientHistory);
            Assert.IsNotNull(example.defaultAccountSettings.AccountFeatureSettings.AllowSignersDownloadEvidenceSummary);

            
            Assert.IsFalse(example.patchedAccountSettings.AccountFeatureSettings.AllowCheckboxConsentApproval);
            Assert.IsTrue(example.patchedAccountSettings.AccountFeatureSettings.AllowInPersonForAccountSenders);
            Assert.IsFalse(example.patchedAccountSettings.AccountFeatureSettings.Attachments);
            Assert.IsFalse(example.patchedAccountSettings.AccountFeatureSettings.ConditionalFields);
            Assert.IsTrue(example.patchedAccountSettings.AccountFeatureSettings.OverrideRecipientsPreferredLanguage);
            Assert.IsTrue(example.patchedAccountSettings.AccountFeatureSettings.EnableRecipientHistory);
            Assert.IsTrue(example.patchedAccountSettings.AccountFeatureSettings.AllowSignersDownloadEvidenceSummary);

            
            Assert.IsNotNull(example.defaultAccountSettings.AccountPackageSettings.Ada);
            Assert.IsNotNull(example.defaultAccountSettings.AccountPackageSettings.DeclineButton);
            Assert.IsNotNull(example.defaultAccountSettings.AccountPackageSettings.DefaultTimeBasedExpiry);
            Assert.IsNotNull(example.defaultAccountSettings.AccountPackageSettings.DisableDeclineOther);
            Assert.IsNotNull(example.defaultAccountSettings.AccountPackageSettings.Title);
            Assert.IsNotNull(example.defaultAccountSettings.AccountPackageSettings.ProgressBar);
            Assert.IsNotNull(example.defaultAccountSettings.AccountPackageSettings.Navigator);
            Assert.IsNotNull(example.defaultAccountSettings.AccountPackageSettings.MaxAttachmentFiles);
            Assert.IsNotNull(example.defaultAccountSettings.AccountPackageSettings.FontSize);

            Assert.IsTrue(example.patchedAccountSettings.AccountPackageSettings.Ada);
            Assert.IsTrue(example.patchedAccountSettings.AccountPackageSettings.DeclineButton);
            Assert.IsTrue(example.patchedAccountSettings.AccountPackageSettings.DefaultTimeBasedExpiry);
            Assert.IsTrue(example.patchedAccountSettings.AccountPackageSettings.DisableDeclineOther);
            Assert.IsTrue(example.patchedAccountSettings.AccountPackageSettings.Title);
            Assert.IsTrue(example.patchedAccountSettings.AccountPackageSettings.ProgressBar);
            Assert.IsTrue(example.patchedAccountSettings.AccountPackageSettings.Navigator);
            Assert.That(example.patchedAccountSettings.AccountPackageSettings.MaxAttachmentFiles,Is.EqualTo(0));
            Assert.That(example.patchedAccountSettings.AccountPackageSettings.FontSize,Is.EqualTo(12));

            
            Assert.IsNotNull(example.defaultAccountFeatureSettings.AllowCheckboxConsentApproval);
            Assert.IsNotNull(example.defaultAccountFeatureSettings.AllowInPersonForAccountSenders);
            Assert.IsNotNull(example.defaultAccountFeatureSettings.Attachments);
            Assert.IsNotNull(example.defaultAccountFeatureSettings.ConditionalFields);
            Assert.IsNotNull(example.defaultAccountFeatureSettings.EnableRecipientHistory);
            Assert.IsNotNull(example.defaultAccountFeatureSettings.AllowSignersDownloadEvidenceSummary);
            
            Assert.IsFalse(example.patchedAccountFeatureSettings.AllowCheckboxConsentApproval);
            Assert.IsTrue(example.patchedAccountFeatureSettings.AllowInPersonForAccountSenders);
            Assert.IsFalse(example.patchedAccountFeatureSettings.Attachments);
            Assert.IsFalse(example.patchedAccountFeatureSettings.ConditionalFields);
            Assert.IsTrue(example.patchedAccountFeatureSettings.EnableRecipientHistory);
            Assert.IsTrue(example.patchedAccountFeatureSettings.AllowSignersDownloadEvidenceSummary);


            Assert.IsNotNull(example.defaultAccountPackageSettings.Ada);
            Assert.IsNotNull(example.defaultAccountPackageSettings.DeclineButton);
            Assert.IsNotNull(example.defaultAccountPackageSettings.DefaultTimeBasedExpiry);
            Assert.IsNotNull(example.defaultAccountPackageSettings.DisableDeclineOther);
            Assert.IsNotNull(example.defaultAccountPackageSettings.Title);
            Assert.IsNotNull(example.defaultAccountPackageSettings.ProgressBar);
            Assert.IsNotNull(example.defaultAccountPackageSettings.Navigator);
            Assert.IsNotNull(example.defaultAccountPackageSettings.MaxAttachmentFiles);
            Assert.IsNotNull(example.defaultAccountPackageSettings.FontSize);

            Assert.IsTrue(example.patchedAccountPackageSettings.Ada);
            Assert.IsTrue(example.patchedAccountPackageSettings.DeclineButton);
            Assert.IsTrue(example.patchedAccountPackageSettings.DefaultTimeBasedExpiry);
            Assert.IsTrue(example.patchedAccountPackageSettings.DisableDeclineOther);
            Assert.IsTrue(example.patchedAccountPackageSettings.Title);
            Assert.IsTrue(example.patchedAccountPackageSettings.ProgressBar);
            Assert.IsTrue(example.patchedAccountPackageSettings.Navigator);
            Assert.That(example.patchedAccountPackageSettings.MaxAttachmentFiles,Is.EqualTo(100));
            Assert.That(example.patchedAccountPackageSettings.FontSize,Is.EqualTo(16));

        }
    }
}