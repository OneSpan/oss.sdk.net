using NUnit.Framework;
using OneSpanSign.Sdk;

namespace SDK.Tests
{
    public class SigningUiOptionsBuilderTest
    {
        [Test]
        public void BuildsFieldWithDefaultValues()
        {
            SigningUiOptions signingUiOptions  = SigningUiOptionsBuilder.NewSigningUiOptions()
                .WithCompleteSummaryOptions(CompleteSummaryOptionsBuilder.NewCompleteSummaryOptions()
                    .WithTitle()
                    .WithMessage()
                    .WithDownload()
                    .WithReview()
                    .WithContinue()
                    .WithDocumentSection()
                    .WithUploadSection()
                    .Build())
                .WithInpersonHostThankYouOptions(InpersonHostThankYouOptionsBuilder.NewInpersonHostThankYouOptions()
                    .WithTitle()
                    .WithBody()
                    .WithRecipientName()
                    .WithRecipientEmail()
                    .WithRecipientRole()
                    .WithRecipientStatus()
                    .WithDownloadButton()
                    .WithReviewDocumentsButton()
                    .Build())
                .WithInpersonWelcomeOptions(InpersonWelcomeOptionsBuilder.NewInpersonWelcomeOptions()
                    .WithTitle()
                    .WithBody()
                    .WithRecipientName()
                    .WithRecipientEmail()
                    .WithRecipientActionRequired()
                    .WithRecipientRole()
                    .WithRecipientStatus()
                    .Build())
                .WithNotaryHostThankYouOptions(NotaryHostThankYouOptionsBuilder.NewNotaryHostThankYouOptions()
                    .WithTitle()
                    .WithBody()
                    .WithRecipientName()
                    .WithRecipientEmail()
                    .WithRecipientRole()
                    .WithNotaryTag()
                    .WithRecipientStatus()
                    .WithDownloadButton()
                    .WithReviewDocumentsButton()
                    .Build())
                .WithNotaryWelcomeOptions(NotaryWelcomeOptionsBuilder.NewNotaryWelcomeOptions()
                    .WithTitle()
                    .WithBody()
                    .WithRecipientName()
                    .WithRecipientEmail()
                    .WithRecipientActionRequired()
                    .WithNotaryTag()
                    .WithRecipientRole()
                    .WithRecipientStatus()
                    .Build())
                .WithOverviewOptions(OverviewOptionsBuilder.NewOverviewOptions()
                    .WithTitle()
                    .WithBody()
                    .WithDocumentSection()
                    .WithUploadSection()
                    .Build())
                .Build();

            Assert.IsTrue(signingUiOptions.CompleteSummaryOptions.Title);
            Assert.IsTrue(signingUiOptions.CompleteSummaryOptions.Message);
            Assert.IsTrue(signingUiOptions.CompleteSummaryOptions.Download);
            Assert.IsTrue(signingUiOptions.CompleteSummaryOptions.Review);
            Assert.IsTrue(signingUiOptions.CompleteSummaryOptions.Continue);
            Assert.IsTrue(signingUiOptions.CompleteSummaryOptions.DocumentSection);
            Assert.IsTrue(signingUiOptions.CompleteSummaryOptions.UploadSection);
            Assert.IsTrue(signingUiOptions.InpersonHostThankYouOptions.Title);
            Assert.IsTrue(signingUiOptions.InpersonHostThankYouOptions.Body);
            Assert.IsTrue(signingUiOptions.InpersonHostThankYouOptions.RecipientName);
            Assert.IsTrue(signingUiOptions.InpersonHostThankYouOptions.RecipientEmail);
            Assert.IsTrue(signingUiOptions.InpersonHostThankYouOptions.RecipientRole);
            Assert.IsTrue(signingUiOptions.InpersonHostThankYouOptions.RecipientStatus);
            Assert.IsTrue(signingUiOptions.InpersonHostThankYouOptions.DownloadButton);
            Assert.IsTrue(signingUiOptions.InpersonHostThankYouOptions.ReviewDocumentsButton);
            Assert.IsTrue(signingUiOptions.InpersonWelcomeOptions.Title);
            Assert.IsTrue(signingUiOptions.InpersonWelcomeOptions.Body);
            Assert.IsTrue(signingUiOptions.InpersonWelcomeOptions.RecipientName);
            Assert.IsTrue(signingUiOptions.InpersonWelcomeOptions.RecipientEmail);
            Assert.IsTrue(signingUiOptions.InpersonWelcomeOptions.RecipientActionRequired);
            Assert.IsTrue(signingUiOptions.InpersonWelcomeOptions.RecipientRole);
            Assert.IsTrue(signingUiOptions.InpersonWelcomeOptions.RecipientStatus);
            Assert.IsTrue(signingUiOptions.NotaryHostThankYouOptions.Title);
            Assert.IsTrue(signingUiOptions.NotaryHostThankYouOptions.Body);
            Assert.IsTrue(signingUiOptions.NotaryHostThankYouOptions.RecipientName);
            Assert.IsTrue(signingUiOptions.NotaryHostThankYouOptions.RecipientEmail);
            Assert.IsTrue(signingUiOptions.NotaryHostThankYouOptions.RecipientRole);
            Assert.IsTrue(signingUiOptions.NotaryHostThankYouOptions.NotaryTag);
            Assert.IsTrue(signingUiOptions.NotaryHostThankYouOptions.RecipientStatus);
            Assert.IsTrue(signingUiOptions.NotaryHostThankYouOptions.DownloadButton);
            Assert.IsTrue(signingUiOptions.NotaryHostThankYouOptions.ReviewDocumentsButton);
            Assert.IsTrue(signingUiOptions.NotaryWelcomeOptions.Title);
            Assert.IsTrue(signingUiOptions.NotaryWelcomeOptions.Body);
            Assert.IsTrue(signingUiOptions.NotaryWelcomeOptions.RecipientName);
            Assert.IsTrue(signingUiOptions.NotaryWelcomeOptions.RecipientEmail);
            Assert.IsTrue(signingUiOptions.NotaryWelcomeOptions.RecipientActionRequired);
            Assert.IsTrue(signingUiOptions.NotaryWelcomeOptions.NotaryTag);
            Assert.IsTrue(signingUiOptions.NotaryWelcomeOptions.RecipientRole);
            Assert.IsTrue(signingUiOptions.NotaryWelcomeOptions.RecipientStatus);
            Assert.IsTrue(signingUiOptions.OverviewOptions.Title);
            Assert.IsTrue(signingUiOptions.OverviewOptions.Body);
            Assert.IsTrue(signingUiOptions.OverviewOptions.DocumentSection);
            Assert.IsTrue(signingUiOptions.OverviewOptions.UploadSection);
        }

        [Test]
        public void BuildsFieldWithSpecifiedValues()
        {
            SigningUiOptions signingUiOptions  = SigningUiOptionsBuilder.NewSigningUiOptions()
                .WithCompleteSummaryOptions(CompleteSummaryOptionsBuilder.NewCompleteSummaryOptions()
                    .WithoutTitle()
                    .WithoutMessage()
                    .WithoutDownload()
                    .WithoutReview()
                    .WithoutContinue()
                    .WithoutDocumentSection()
                    .WithoutUploadSection()
                    .Build())
                .WithInpersonHostThankYouOptions(InpersonHostThankYouOptionsBuilder.NewInpersonHostThankYouOptions()
                    .WithoutTitle()
                    .WithoutBody()
                    .WithoutRecipientName()
                    .WithoutRecipientEmail()
                    .WithoutRecipientRole()
                    .WithoutRecipientStatus()
                    .WithoutDownloadButton()
                    .WithoutReviewDocumentsButton()
                    .Build())
                .WithInpersonWelcomeOptions(InpersonWelcomeOptionsBuilder.NewInpersonWelcomeOptions()
                    .WithoutTitle()
                    .WithoutBody()
                    .WithoutRecipientName()
                    .WithoutRecipientEmail()
                    .WithoutRecipientActionRequired()
                    .WithoutRecipientRole()
                    .WithoutRecipientStatus()
                    .Build())
                .WithNotaryHostThankYouOptions(NotaryHostThankYouOptionsBuilder.NewNotaryHostThankYouOptions()
                    .WithoutTitle()
                    .WithoutBody()
                    .WithoutRecipientName()
                    .WithoutRecipientEmail()
                    .WithoutRecipientRole()
                    .WithoutNotaryTag()
                    .WithoutRecipientStatus()
                    .WithoutReviewDocumentsButton()
                    .WithoutDownloadButton()
                    .Build())
                .WithNotaryWelcomeOptions(NotaryWelcomeOptionsBuilder.NewNotaryWelcomeOptions()
                    .WithoutTitle()
                    .WithoutBody()
                    .WithoutRecipientName()
                    .WithoutRecipientEmail()
                    .WithoutRecipientActionRequired()
                    .WithoutNotaryTag()
                    .WithoutRecipientRole()
                    .WithoutRecipientStatus()
                    .Build())
                .WithOverviewOptions(OverviewOptionsBuilder.NewOverviewOptions()
                    .WithoutTitle()
                    .WithoutBody()
                    .WithoutDocumentSection()
                    .WithoutUploadSection()
                    .Build())
                .Build();

            Assert.IsFalse(signingUiOptions.CompleteSummaryOptions.Title);
            Assert.IsFalse(signingUiOptions.CompleteSummaryOptions.Message);
            Assert.IsFalse(signingUiOptions.CompleteSummaryOptions.Download);
            Assert.IsFalse(signingUiOptions.CompleteSummaryOptions.Review);
            Assert.IsFalse(signingUiOptions.CompleteSummaryOptions.Continue);
            Assert.IsFalse(signingUiOptions.CompleteSummaryOptions.DocumentSection);
            Assert.IsFalse(signingUiOptions.CompleteSummaryOptions.UploadSection);
            Assert.IsFalse(signingUiOptions.InpersonHostThankYouOptions.Title);
            Assert.IsFalse(signingUiOptions.InpersonHostThankYouOptions.Body);
            Assert.IsFalse(signingUiOptions.InpersonHostThankYouOptions.RecipientName);
            Assert.IsFalse(signingUiOptions.InpersonHostThankYouOptions.RecipientEmail);
            Assert.IsFalse(signingUiOptions.InpersonHostThankYouOptions.RecipientRole);
            Assert.IsFalse(signingUiOptions.InpersonHostThankYouOptions.RecipientStatus);
            Assert.IsFalse(signingUiOptions.InpersonHostThankYouOptions.DownloadButton);
            Assert.IsFalse(signingUiOptions.InpersonHostThankYouOptions.ReviewDocumentsButton);
            Assert.IsFalse(signingUiOptions.InpersonWelcomeOptions.Title);
            Assert.IsFalse(signingUiOptions.InpersonWelcomeOptions.Body);
            Assert.IsFalse(signingUiOptions.InpersonWelcomeOptions.RecipientName);
            Assert.IsFalse(signingUiOptions.InpersonWelcomeOptions.RecipientEmail);
            Assert.IsFalse(signingUiOptions.InpersonWelcomeOptions.RecipientActionRequired);
            Assert.IsFalse(signingUiOptions.InpersonWelcomeOptions.RecipientRole);
            Assert.IsFalse(signingUiOptions.InpersonWelcomeOptions.RecipientStatus);
            Assert.IsFalse(signingUiOptions.NotaryHostThankYouOptions.Title);
            Assert.IsFalse(signingUiOptions.NotaryHostThankYouOptions.Body);
            Assert.IsFalse(signingUiOptions.NotaryHostThankYouOptions.RecipientName);
            Assert.IsFalse(signingUiOptions.NotaryHostThankYouOptions.RecipientEmail);
            Assert.IsFalse(signingUiOptions.NotaryHostThankYouOptions.RecipientRole);
            Assert.IsFalse(signingUiOptions.NotaryHostThankYouOptions.NotaryTag);
            Assert.IsFalse(signingUiOptions.NotaryHostThankYouOptions.RecipientStatus);
            Assert.IsFalse(signingUiOptions.NotaryHostThankYouOptions.DownloadButton);
            Assert.IsFalse(signingUiOptions.NotaryHostThankYouOptions.ReviewDocumentsButton);
            Assert.IsFalse(signingUiOptions.NotaryWelcomeOptions.Title);
            Assert.IsFalse(signingUiOptions.NotaryWelcomeOptions.Body);
            Assert.IsFalse(signingUiOptions.NotaryWelcomeOptions.RecipientName);
            Assert.IsFalse(signingUiOptions.NotaryWelcomeOptions.RecipientEmail);
            Assert.IsFalse(signingUiOptions.NotaryWelcomeOptions.RecipientActionRequired);
            Assert.IsFalse(signingUiOptions.NotaryWelcomeOptions.NotaryTag);
            Assert.IsFalse(signingUiOptions.NotaryWelcomeOptions.RecipientRole);
            Assert.IsFalse(signingUiOptions.NotaryWelcomeOptions.RecipientStatus);
            Assert.IsFalse(signingUiOptions.OverviewOptions.Title);
            Assert.IsFalse(signingUiOptions.OverviewOptions.Body);
            Assert.IsFalse(signingUiOptions.OverviewOptions.DocumentSection);
            Assert.IsFalse(signingUiOptions.OverviewOptions.UploadSection);
        }
        
    }
}
