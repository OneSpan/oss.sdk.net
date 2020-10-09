using System;
using NUnit.Framework;
using OneSpanSign.Sdk;

namespace SDK.Examples
{
    [TestFixture ()]
    public class DocumentPackageSettingsExampleTest
    {
        [Test ()]
        public void VerifyResult ()
        {
            DocumentPackageSettingsExample example = new DocumentPackageSettingsExample ();
            example.Run ();

            DocumentPackage result = example.OssClient.GetPackage (example.PackageId);

            Assert.IsTrue (result.Settings.EnableFirstAffidavit.HasValue);
            Assert.IsFalse (result.Settings.EnableFirstAffidavit.Value);

            Assert.IsTrue (result.Settings.EnableSecondAffidavit.HasValue);
            Assert.IsFalse (result.Settings.EnableSecondAffidavit.Value);

            Assert.IsTrue (result.Settings.ShowLanguageDropDown.HasValue);
            Assert.IsFalse (result.Settings.ShowLanguageDropDown.Value);

            Assert.IsTrue (result.Settings.ShowOwnerInPersonDropDown.HasValue);
            Assert.IsFalse (result.Settings.ShowOwnerInPersonDropDown.Value);

            Assert.IsTrue (result.Settings.EnforceCaptureSignature.HasValue);
            Assert.IsTrue (result.Settings.EnforceCaptureSignature.Value);
            
            Assert.IsTrue (result.Settings.ExpandLeftMenu.HasValue);
            Assert.IsTrue (result.Settings.ExpandLeftMenu.Value);

            Assert.AreEqual (example.FONT_SIZE, result.Settings.FontSize.Value);

            Assert.AreEqual (3, result.Settings.DeclineReasons.Count);
            Assert.AreEqual (example.DECLINE_REASON_1, result.Settings.DeclineReasons [0]);
            Assert.AreEqual (example.DECLINE_REASON_2, result.Settings.DeclineReasons [1]);
            Assert.AreEqual (example.DECLINE_REASON_3, result.Settings.DeclineReasons [2]);
            Assert.IsTrue (result.Settings.DisableDeclineOther.Value);

            Assert.AreEqual (3, result.Settings.DeclineReasons.Count);
            Assert.AreEqual (example.OPT_OUT_REASON_1, result.Settings.OptOutReasons [0]);
            Assert.AreEqual (example.OPT_OUT_REASON_2, result.Settings.OptOutReasons [1]);
            Assert.AreEqual (example.OPT_OUT_REASON_3, result.Settings.OptOutReasons [2]);
            Assert.IsTrue (result.Settings.DisableOptOutOther.Value);

            Assert.IsTrue (result.Settings.DefaultTimeBasedExpiry.Value);
            Assert.AreEqual (example.EXPIRE_IN_DAYS, result.Settings.RemainingDays.Value);

            Assert.AreEqual (example.MAX_ATTACHMENT_FILES, result.Settings.MaxAttachmentFiles.Value);
        }
    }
}

