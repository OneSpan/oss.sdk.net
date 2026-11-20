using NUnit.Framework;
using OneSpanSign.Sdk;
using static OneSpanSign.Sdk.Builder.CallbackConfigurationRequestBuilder;

namespace SDK.Tests
{
    [TestFixture]
    public class CallbackConfigurationRequestBuilderTest
    {
        [Test]
        public void NotificationEventsAreSentAsTheirApiValue()
        {
            CallbackConfigurationRequest request = NewCallbackConfigurationRequest()
                .WithEvents(NotificationEvent.PACKAGE_COMPLETE, NotificationEvent.PACKAGE_READY_FOR_COMPLETION)
                .Build();

            CollectionAssert.AreEqual(new[] { "PACKAGE_COMPLETE", "PACKAGE_READY_FOR_COMPLETE" }, request.Events);
        }
    }
}
