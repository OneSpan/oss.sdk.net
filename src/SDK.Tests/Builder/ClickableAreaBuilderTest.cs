using System;
using NUnit.Framework;
using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Builder;

namespace SDK.Tests
{
    public class ClickableAreaBuilderTest
    {
        [Test]
        public void BuildWithSizeAndAlignment()
        {
            double width = 20;
            double height = 10;
            ClickableAreaAlignment alignment = ClickableAreaAlignment.TOP_LEFT;

            ClickableArea clickableArea = ClickableAreaBuilder.NewClickableArea()
                .WithSize(width, height)
                .WithAlignment(alignment)
                .Build();

            Assert.AreEqual(width, clickableArea.Width);
            Assert.AreEqual(height, clickableArea.Height);
            Assert.AreEqual(alignment, clickableArea.Alignment);
        }

        [Test]
        public void BuildWithNoValuesSetLeavesEverythingNull()
        {
            ClickableArea clickableArea = ClickableAreaBuilder.NewClickableArea().Build();

            Assert.IsNull(clickableArea.Width);
            Assert.IsNull(clickableArea.Height);
            Assert.IsNull(clickableArea.Alignment);
        }

        [Test]
        public void WithSizeRejectsNegativeWidth()
        {
            Assert.Throws<BuilderException>(() => ClickableAreaBuilder.NewClickableArea().WithSize(-1, 10));
        }

        [Test]
        public void WithSizeRejectsNegativeHeight()
        {
            Assert.Throws<BuilderException>(() => ClickableAreaBuilder.NewClickableArea().WithSize(10, -1));
        }
    }
}
