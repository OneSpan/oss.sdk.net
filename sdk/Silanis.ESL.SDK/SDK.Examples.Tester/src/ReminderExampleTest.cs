using NUnit.Framework;
using System;
using Silanis.ESL.SDK;

namespace SDK.Examples
{
    [TestFixture()]
    public class ReminderExampleTest
    {
        [Test()]
        public void VerifyResult()
        {
            ReminderExample example = new ReminderExample(Props.GetInstance());
            example.Run();

            ReminderSchedule reminderSchedule = example.ReminderSchedule;

            Assert.IsNotNull(reminderSchedule);
            Assert.AreEqual(reminderSchedule.PackageId.Id, example.PackageId.Id);
            Assert.AreEqual(reminderSchedule.DaysUntilFirstReminder, 2);
            Assert.AreEqual(reminderSchedule.DaysBetweenReminders, 1);
            Assert.AreEqual(reminderSchedule.NumberOfRepetitions, 5);
        }
    }
}

