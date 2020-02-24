using System;
using NUnit.Framework;
using Silanis.ESL.SDK;
namespace SDK.Tests
{
    [TestFixture]
    public class TransactionRetentionBuilderTest
    {

        [Test]
        public void BuildTest ()
        {
            TransactionRetentionBuilder builder = TransactionRetentionBuilder.NewTransactionRetention ()
                    .WithArchived (10)
                    .WithCompleted (20)
                    .WithDeclined (30)
                    .WithDraft (40)
                    .WithExpired (50)
                    .WithOptedOut (60)
                    .WithSent (70);

            TransactionRetention result = builder.Build ();

            Assert.IsNotNull (result);
            Assert.AreEqual (result.Archived, 10);
            Assert.AreEqual (result.Completed, 20);
            Assert.AreEqual (result.Declined, 30);
            Assert.AreEqual (result.Draft, 40);
            Assert.AreEqual (result.Expired, 50);
            Assert.AreEqual (result.OptedOut, 60);
            Assert.AreEqual (result.Sent, 70);
        }
    }
}
