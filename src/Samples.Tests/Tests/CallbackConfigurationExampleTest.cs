using NUnit.Framework;
using OneSpanSign.Sdk;

namespace SDK.Examples
{
    [TestFixture]
    public class CallbackConfigurationExampleTest
    {
        [Test]
        public void VerifyResult()
        {
            CallbackConfigurationExample example = new CallbackConfigurationExample();
            example.Run();

            Assert.AreEqual("ACCOUNT", example.saved.Type);
            Assert.AreEqual(CallbackConfigurationExample.URL, example.saved.Url);
            Assert.IsTrue(example.saved.Enabled);

            Assert.IsTrue(example.configurations.Exists(c => c.Id == example.saved.Id),
                "The saved callback is not listed");

            Assert.IsNotNull(example.testResult.Outcome);

            CollectionAssert.AreEquivalent(
                new[] { NotificationEvent.PACKAGE_CREATE, NotificationEvent.PACKAGE_COMPLETE, NotificationEvent.PACKAGE_DECLINE },
                example.replaced.Events);
            Assert.IsTrue(example.replaced.Enabled);

            Assert.IsFalse(example.disabled.Enabled);
        }
    }
}
