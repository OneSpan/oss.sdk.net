using NUnit.Framework;
using OneSpanSign.Sdk;

namespace SDK.Tests
{
    public class InpersonWelcomeOptionsBuilderTest
    {
        [Test]
        public void BuildsFieldWithDefaultValues()
        {
            InpersonWelcomeOptions inpersonWelcomeOptions  = InpersonWelcomeOptionsBuilder.NewInpersonWelcomeOptions()
                .WithTitle()
                .WithBody()
                .WithRecipientName()
                .WithRecipientEmail()
                .WithRecipientActionRequired()
                .WithRecipientRole()
                .WithRecipientStatus()
                .Build();

            Assert.IsTrue(inpersonWelcomeOptions.Title);
            Assert.IsTrue(inpersonWelcomeOptions.Body);
            Assert.IsTrue(inpersonWelcomeOptions.RecipientName);
            Assert.IsTrue(inpersonWelcomeOptions.RecipientEmail);
            Assert.IsTrue(inpersonWelcomeOptions.RecipientActionRequired);
            Assert.IsTrue(inpersonWelcomeOptions.RecipientRole);
            Assert.IsTrue(inpersonWelcomeOptions.RecipientStatus);
        }

        [Test]
        public void BuildsFieldWithSpecifiedValues()
        {
            InpersonWelcomeOptions inpersonWelcomeOptions = InpersonWelcomeOptionsBuilder.NewInpersonWelcomeOptions()
                .WithoutTitle()
                .WithoutBody()
                .WithoutRecipientName()
                .WithoutRecipientEmail()
                .WithoutRecipientActionRequired()
                .WithoutRecipientRole()
                .WithoutRecipientStatus()
                .Build();

            Assert.IsFalse(inpersonWelcomeOptions.Title);
            Assert.IsFalse(inpersonWelcomeOptions.Body);
            Assert.IsFalse(inpersonWelcomeOptions.RecipientName);
            Assert.IsFalse(inpersonWelcomeOptions.RecipientEmail);
            Assert.IsFalse(inpersonWelcomeOptions.RecipientActionRequired);
            Assert.IsFalse(inpersonWelcomeOptions.RecipientRole);
            Assert.IsFalse(inpersonWelcomeOptions.RecipientStatus);
        }
        
    }
}
