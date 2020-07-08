using NUnit.Framework;
using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Builder;

namespace SDK.Examples
{
    [TestFixture()]
    public class ConfigureHandoverUrlExampleTest
    {
        [Test()]
        public void VerifyResult()
        {
            ConfigureHandoverUrlExample example = new ConfigureHandoverUrlExample();
            example.Run();

            AssertHandover(example.handoverBeforeCreating, HandoverBuilder.NewHandover(example.ITALIAN)
                .WithHref("")
                .WithTitle("")
                .WithText("")
                .Build());
            AssertHandover(example.handoverAfterCreating, HandoverBuilder.NewHandover(example.ITALIAN)
                .WithHref(example.HREF)
                .WithTitle(example.TITLE)
                .WithText(example.TEXT)
                .Build());
            AssertHandover(example.handoverAfterUpdating, HandoverBuilder.NewHandover(example.ITALIAN)
                .WithHref(example.HREF)
                .WithTitle(example.UPDATED_TITLE)
                .WithText(example.TEXT)
                .Build());
            AssertHandover(example.handoverAfterDeleting, HandoverBuilder.NewHandover(example.ITALIAN)
                .WithHref("")
                .WithTitle("")
                .WithText("")
                .Build());
        }

        private void AssertHandover(Handover actual, Handover expected)
        {
            Assert.AreEqual(expected.Href, actual.Href);
            Assert.AreEqual(expected.Text, actual.Text);
            Assert.AreEqual(expected.Title, actual.Title);
        }
    }
}