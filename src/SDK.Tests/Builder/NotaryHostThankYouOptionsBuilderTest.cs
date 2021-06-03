using NUnit.Framework;
using OneSpanSign.Sdk;

namespace SDK.Tests
{
    public class NotaryHostThankYouOptionsBuilderTest
    {
        [Test]
        public void BuildsFieldWithDefaultValues()
        {
            NotaryHostThankYouOptions notaryHostThankYouOptions  = NotaryHostThankYouOptionsBuilder.NewNotaryHostThankYouOptions()
                .WithTitle()
                .WithBody()
                .WithRecipientName()
                .WithRecipientEmail()
                .WithRecipientRole()
                .WithNotaryTag()
                .WithRecipientStatus()
                .WithDownloadButton()
                .WithReviewDocumentsButton()
                .Build();

            Assert.IsTrue(notaryHostThankYouOptions.Title);
            Assert.IsTrue(notaryHostThankYouOptions.Body);
            Assert.IsTrue(notaryHostThankYouOptions.RecipientName);
            Assert.IsTrue(notaryHostThankYouOptions.RecipientEmail);
            Assert.IsTrue(notaryHostThankYouOptions.RecipientRole);
            Assert.IsTrue(notaryHostThankYouOptions.NotaryTag);
            Assert.IsTrue(notaryHostThankYouOptions.RecipientStatus);
            Assert.IsTrue(notaryHostThankYouOptions.DownloadButton);
            Assert.IsTrue(notaryHostThankYouOptions.ReviewDocumentsButton);
        }

        [Test]
        public void BuildsFieldWithSpecifiedValues()
        {
            NotaryHostThankYouOptions notaryHostThankYouOptions = NotaryHostThankYouOptionsBuilder.NewNotaryHostThankYouOptions()
                .WithoutTitle()
                .WithoutBody()
                .WithoutRecipientName()
                .WithoutRecipientEmail()
                .WithoutRecipientRole()
                .WithoutNotaryTag()
                .WithoutRecipientStatus()
                .WithoutDownloadButton()
                .WithoutReviewDocumentsButton()
                .Build();

            Assert.IsFalse(notaryHostThankYouOptions.Title);
            Assert.IsFalse(notaryHostThankYouOptions.Body);
            Assert.IsFalse(notaryHostThankYouOptions.RecipientName);
            Assert.IsFalse(notaryHostThankYouOptions.RecipientEmail);
            Assert.IsFalse(notaryHostThankYouOptions.RecipientRole);
            Assert.IsFalse(notaryHostThankYouOptions.NotaryTag);
            Assert.IsFalse(notaryHostThankYouOptions.RecipientStatus);
            Assert.IsFalse(notaryHostThankYouOptions.DownloadButton);
            Assert.IsFalse(notaryHostThankYouOptions.ReviewDocumentsButton);
        }
        
    }
}
