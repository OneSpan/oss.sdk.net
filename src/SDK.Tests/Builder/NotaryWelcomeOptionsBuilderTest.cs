using NUnit.Framework;
using OneSpanSign.Sdk;

namespace SDK.Tests
{
    public class NotaryWelcomeOptionsBuilderTest
    {
        [Test]
        public void BuildsFieldWithDefaultValues()
        {
            NotaryWelcomeOptions notaryWelcomeOptions  = NotaryWelcomeOptionsBuilder.NewNotaryWelcomeOptions()
                .WithTitle()
                .WithBody()
                .WithRecipientName()
                .WithRecipientEmail()
                .WithRecipientActionRequired()
                .WithNotaryTag()
                .WithRecipientRole()
                .WithRecipientStatus()
                .Build();

            Assert.IsTrue(notaryWelcomeOptions.Title);
            Assert.IsTrue(notaryWelcomeOptions.Body);
            Assert.IsTrue(notaryWelcomeOptions.RecipientName);
            Assert.IsTrue(notaryWelcomeOptions.RecipientEmail);
            Assert.IsTrue(notaryWelcomeOptions.RecipientActionRequired);
            Assert.IsTrue(notaryWelcomeOptions.NotaryTag);
            Assert.IsTrue(notaryWelcomeOptions.RecipientRole);
            Assert.IsTrue(notaryWelcomeOptions.RecipientStatus);
        }

        [Test]
        public void BuildsFieldWithSpecifiedValues()
        {
            NotaryWelcomeOptions notaryWelcomeOptions = NotaryWelcomeOptionsBuilder.NewNotaryWelcomeOptions()
                .WithoutTitle()
                .WithoutBody()
                .WithoutRecipientName()
                .WithoutRecipientEmail()
                .WithoutRecipientActionRequired()
                .WithoutNotaryTag()
                .WithoutRecipientRole()
                .WithoutRecipientStatus()
                .Build();

            Assert.IsFalse(notaryWelcomeOptions.Title);
            Assert.IsFalse(notaryWelcomeOptions.Body);
            Assert.IsFalse(notaryWelcomeOptions.RecipientName);
            Assert.IsFalse(notaryWelcomeOptions.RecipientEmail);
            Assert.IsFalse(notaryWelcomeOptions.RecipientActionRequired);
            Assert.IsFalse(notaryWelcomeOptions.NotaryTag);
            Assert.IsFalse(notaryWelcomeOptions.RecipientRole);
            Assert.IsFalse(notaryWelcomeOptions.RecipientStatus);
        }
        
    }
}
