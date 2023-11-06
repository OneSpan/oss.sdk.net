using NUnit.Framework;

namespace SDK.Examples
{
    [TestFixture ()]
    public class AccountEmailReminderSettingsExampleTest
    {
        [Test ()]
        public void VerifyResult ()
        {
            AccountEmailReminderSettingsExample example = new AccountEmailReminderSettingsExample();
            example.Run ();
            
            Assert.IsNotNull(example.defaultAccountEmailReminderSettings.StartInDaysDelay);
            Assert.IsNotNull(example.defaultAccountEmailReminderSettings.IntervalInDays);
            Assert.IsNotNull(example.defaultAccountEmailReminderSettings.RepetitionsCount);
            
            Assert.IsTrue(example.patchedAccountEmailReminderSettings.StartInDaysDelay.Equals(20));
            Assert.IsTrue(example.patchedAccountEmailReminderSettings.IntervalInDays.Equals(400));
            Assert.IsTrue(example.patchedAccountEmailReminderSettings.RepetitionsCount.Equals(2));   
        }
    }
}

