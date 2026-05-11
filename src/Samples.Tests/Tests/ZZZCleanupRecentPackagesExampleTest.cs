using NUnit.Framework;
using OneSpanSign.Sdk;

namespace SDK.Examples
{
    /// <summary>
    /// Cleanup test that runs last (alphabetically after all other *ExampleTest
    /// classes) and deletes every package, transaction and template that was updated
    /// since 2024-08-01. This keeps the test account tidy after a full test pass.
    ///
    /// The "ZZZ" prefix guarantees that NUnit's default alphabetical run order
    /// places this class after all other ExampleTest classes.
    /// </summary>
    [TestFixture]
    public class ZZZCleanupRecentPackagesExampleTest
    {
        [Test]
        public void VerifyResult()
        {
            CleanupRecentPackagesExample example = new CleanupRecentPackagesExample();
            example.Run();

            Assert.IsNotNull(example.deletedPackageIds, "Deleted package IDs list should not be null");
            Assert.IsNotNull(example.deletedTemplateIds, "Deleted template IDs list should not be null");
            Assert.IsNotNull(example.deletedSenderIds, "Deleted sender IDs list should not be null");
            Assert.IsNotNull(example.deletedGroupIds, "Deleted group IDs list should not be null");
            Assert.GreaterOrEqual(example.deletedPackagesCount, 0, "Deleted packages count should be non-negative");
            Assert.GreaterOrEqual(example.deletedTemplatesCount, 0, "Deleted templates count should be non-negative");
            Assert.GreaterOrEqual(example.deletedSendersCount, 0, "Deleted senders count should be non-negative");
            Assert.GreaterOrEqual(example.deletedGroupsCount, 0, "Deleted groups count should be non-negative");
            Assert.AreEqual(example.deletedPackagesCount, example.deletedPackageIds.Count, "Deleted packages count should match ID list size");
            Assert.AreEqual(example.deletedTemplatesCount, example.deletedTemplateIds.Count, "Deleted templates count should match ID list size");
            Assert.AreEqual(example.deletedSendersCount, example.deletedSenderIds.Count, "Deleted senders count should match ID list size");
            Assert.AreEqual(example.deletedGroupsCount, example.deletedGroupIds.Count, "Deleted groups count should match ID list size");
        }
    }
}
