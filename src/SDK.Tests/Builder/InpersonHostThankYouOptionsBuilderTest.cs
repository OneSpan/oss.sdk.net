using NUnit.Framework;
using OneSpanSign.Sdk;

namespace SDK.Tests
{
    public class InpersonHostThankYouOptionsBuilderTest
    {
        [Test]
        public void BuildsFieldWithDefaultValues()
        {
            InpersonHostThankYouOptions inpersonHostThankYouOptions  = InpersonHostThankYouOptionsBuilder.NewInpersonHostThankYouOptions()
                .WithTitle()
                .WithBody()
                .WithRecipientName()
                .WithRecipientEmail()
                .WithRecipientRole()
                .WithRecipientStatus()
                .WithDownloadButton()
                .WithReviewDocumentsButton()
                .Build();

            Assert.IsTrue(inpersonHostThankYouOptions.Title);
            Assert.IsTrue(inpersonHostThankYouOptions.Body);
            Assert.IsTrue(inpersonHostThankYouOptions.RecipientName);
            Assert.IsTrue(inpersonHostThankYouOptions.RecipientEmail);
            Assert.IsTrue(inpersonHostThankYouOptions.RecipientRole);
            Assert.IsTrue(inpersonHostThankYouOptions.RecipientStatus);
            Assert.IsTrue(inpersonHostThankYouOptions.DownloadButton);
            Assert.IsTrue(inpersonHostThankYouOptions.ReviewDocumentsButton);
        }

        [Test]
        public void BuildsFieldWithSpecifiedValues()
        {
            InpersonHostThankYouOptions inpersonHostThankYouOptions = InpersonHostThankYouOptionsBuilder.NewInpersonHostThankYouOptions()
                .WithoutTitle()
                .WithoutBody()
                .WithoutRecipientName()
                .WithoutRecipientEmail()
                .WithoutRecipientRole()
                .WithoutRecipientStatus()
                .WithoutDownloadButton()
                .WithoutReviewDocumentsButton()
                .Build();

            Assert.IsFalse(inpersonHostThankYouOptions.Title);
            Assert.IsFalse(inpersonHostThankYouOptions.Body);
            Assert.IsFalse(inpersonHostThankYouOptions.RecipientName);
            Assert.IsFalse(inpersonHostThankYouOptions.RecipientEmail);
            Assert.IsFalse(inpersonHostThankYouOptions.RecipientRole);
            Assert.IsFalse(inpersonHostThankYouOptions.RecipientStatus);
            Assert.IsFalse(inpersonHostThankYouOptions.DownloadButton);
            Assert.IsFalse(inpersonHostThankYouOptions.ReviewDocumentsButton);
        }
        
    }
}
