using NUnit.Framework;
using OneSpanSign.Sdk;

namespace SDK.Tests
{
    [TestFixture]
    public class AccountEmailReminderSettingsBuilderTest
    {
        public AccountEmailReminderSettingsBuilderTest()
        {
        }

        [Test]
        public void buildWithSpecifiedValues()
        {
            AccountEmailReminderSettings accountEmailReminderSettings = AccountEmailReminderSettingsBuilder
                .NewAccountEmailReminderSettings()
                .WithRepetitionsCount(2)
                .WithIntervalInDays(400)
                .WithStartInDaysDelay(20)
                .Build();

            Assert.AreEqual(accountEmailReminderSettings.StartInDaysDelay, 20);
            Assert.AreEqual(accountEmailReminderSettings.IntervalInDays, 400);
            Assert.AreEqual(accountEmailReminderSettings.RepetitionsCount, 2);   
        }

    }
}