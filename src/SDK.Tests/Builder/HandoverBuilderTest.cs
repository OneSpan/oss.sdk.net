using NUnit.Framework;
using OneSpanSign.Sdk.Builder;

namespace OneSpanSign.Sdk.Test.Builder
{
    public class HandoverBuilderTest
    {
        [Test]
        public void BuildTest()
        {
            string lang = "fr";
            string href = "href";
            string text = "text";
            string title = "title";
            Handover handover = HandoverBuilder.NewHandover(lang)
                .WithHref(href)
                .WithText(text)
                .WithTitle(title)
                .Build();

            Assert.AreEqual(lang, handover.Language);
            Assert.AreEqual(href, handover.Href);
            Assert.AreEqual(text, handover.Text);
            Assert.AreEqual(title, handover.Title);
        }
    }
}