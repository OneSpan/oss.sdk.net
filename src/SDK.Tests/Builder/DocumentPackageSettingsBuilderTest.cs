using System;
using NUnit.Framework;
using OneSpanSign.Sdk;

namespace SDK.Tests
{
    public class DocumentPackageSettingsBuilderTest
    {
        [Test]
        public void LanguageDropDown() 
        {
            DocumentPackageSettingsBuilder builder = DocumentPackageSettingsBuilder.NewDocumentPackageSettings();
            DocumentPackageSettings unset = builder.Build();
            Assert.IsFalse(unset.ShowLanguageDropDown.HasValue);
            DocumentPackageSettings with = builder.WithLanguageDropDown().Build();
            Assert.IsTrue(with.ShowLanguageDropDown.HasValue);
            Assert.IsTrue(with.ShowLanguageDropDown.Value);
            DocumentPackageSettings without = builder.WithoutLanguageDropDown().Build();
            Assert.IsTrue(without.ShowLanguageDropDown.HasValue);
            Assert.IsFalse(without.ShowLanguageDropDown.Value);
        }
        
        [Test]
        public void FirstAffidavit()
        {
            DocumentPackageSettingsBuilder builder = DocumentPackageSettingsBuilder.NewDocumentPackageSettings();
            DocumentPackageSettings unset = builder.Build();
            Assert.IsFalse(unset.EnableFirstAffidavit.HasValue);
            DocumentPackageSettings with = builder.EnableFirstAffidavit().Build();
            Assert.IsTrue(with.EnableFirstAffidavit.HasValue);
            Assert.IsTrue(with.EnableFirstAffidavit.Value);
            DocumentPackageSettings without = builder.DisableFirstAffidavit().Build();
            Assert.IsTrue(without.EnableFirstAffidavit.HasValue);
            Assert.IsFalse(without.EnableFirstAffidavit.Value);
        }

        [Test]
        public void SecondAffidavit()
        {
            DocumentPackageSettingsBuilder builder = DocumentPackageSettingsBuilder.NewDocumentPackageSettings();
            DocumentPackageSettings unset = builder.Build();
            Assert.IsFalse(unset.EnableSecondAffidavit.HasValue);
            DocumentPackageSettings with = builder.EnableSecondAffidavit().Build();
            Assert.IsTrue(with.EnableSecondAffidavit.HasValue);
            Assert.IsTrue(with.EnableSecondAffidavit.Value);
            DocumentPackageSettings without = builder.DisableSecondAffidavit().Build();
            Assert.IsTrue(without.EnableSecondAffidavit.HasValue);
            Assert.IsFalse(without.EnableSecondAffidavit.Value);
        }

        [Test]
        public void ShowOwnerInPersonDropDown()
        {
            DocumentPackageSettingsBuilder builder = DocumentPackageSettingsBuilder.NewDocumentPackageSettings();
            DocumentPackageSettings unset = builder.Build();
            Assert.IsFalse(unset.ShowOwnerInPersonDropDown.HasValue);
            DocumentPackageSettings with = builder.ShowOwnerInPersonDropDown().Build();
            Assert.IsTrue(with.ShowOwnerInPersonDropDown.HasValue);
            Assert.IsTrue(with.ShowOwnerInPersonDropDown.Value);
            DocumentPackageSettings without = builder.HideOwnerInPersonDropDown().Build();
            Assert.IsTrue(without.ShowOwnerInPersonDropDown.HasValue);
            Assert.IsFalse(without.ShowOwnerInPersonDropDown.Value);
        }

        [Test]
        public void EnforceCaptureSignature ()
        {
            DocumentPackageSettingsBuilder builder = DocumentPackageSettingsBuilder.NewDocumentPackageSettings ();
            DocumentPackageSettings unset = builder.Build ();
            Assert.IsFalse (unset.EnforceCaptureSignature.HasValue);
            DocumentPackageSettings with = builder.WithEnforceCaptureSignature ().Build ();
            Assert.IsTrue (with.EnforceCaptureSignature.HasValue);
            Assert.IsTrue (with.EnforceCaptureSignature.Value);
            DocumentPackageSettings without = builder.WithoutEnforceCaptureSignature ().Build ();
            Assert.IsTrue (without.EnforceCaptureSignature.HasValue);
            Assert.IsFalse (without.EnforceCaptureSignature.Value);
        }

        [Test]
        public void TimeBasedExpiry ()
        {
            DocumentPackageSettingsBuilder builder = DocumentPackageSettingsBuilder.NewDocumentPackageSettings ();
            DocumentPackageSettings unset = builder.Build ();
            Assert.IsFalse (unset.DefaultTimeBasedExpiry.HasValue);
            DocumentPackageSettings with = builder.WithDefaultTimeBasedExpiry ().WithRemainingDays (14).Build ();
            Assert.IsTrue (with.DefaultTimeBasedExpiry.HasValue);
            Assert.IsTrue (with.DefaultTimeBasedExpiry.Value);
            Assert.IsTrue (with.RemainingDays.Value == 14);
            DocumentPackageSettings without = builder.WithoutDefaultTimeBasedExpiry ().Build ();
            Assert.IsTrue (without.DefaultTimeBasedExpiry.HasValue);
            Assert.IsFalse (without.DefaultTimeBasedExpiry.Value);
        }
        
        [Test]
        public void LeftMenuExpand ()
        {
            DocumentPackageSettingsBuilder builder = DocumentPackageSettingsBuilder.NewDocumentPackageSettings ();
            DocumentPackageSettings unset = builder.Build ();
            Assert.IsFalse (unset.ExpandLeftMenu.HasValue);
            DocumentPackageSettings with = builder.WithLeftMenuExpand().Build ();
            Assert.IsTrue (with.ExpandLeftMenu.HasValue);
            Assert.IsTrue (with.ExpandLeftMenu.Value);
            DocumentPackageSettings without = builder.WithoutLeftMenuExpand().Build ();
            Assert.IsTrue (without.ExpandLeftMenu.HasValue);
            Assert.IsFalse (without.ExpandLeftMenu.Value);
        }

    }
}

