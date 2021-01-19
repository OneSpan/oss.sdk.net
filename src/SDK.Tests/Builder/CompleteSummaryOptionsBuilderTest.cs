using NUnit.Framework;
using OneSpanSign.Sdk;

namespace SDK.Tests
{
    public class CompleteSummaryOptionsBuilderTest
    {
        [Test]
        public void BuildsFieldWithDefaultValues()
        {
            CompleteSummaryOptions completeSummaryOptions  = CompleteSummaryOptionsBuilder.NewCompleteSummaryOptions()
                .WithFrom()
                .WithTitle()
                .WithMessage()
                .WithDownload()
                .WithReview()
                .WithContinue()
                .Build();

            Assert.IsTrue(completeSummaryOptions.From);
            Assert.IsTrue(completeSummaryOptions.Title);
            Assert.IsTrue(completeSummaryOptions.Message);
            Assert.IsTrue(completeSummaryOptions.Download);
            Assert.IsTrue(completeSummaryOptions.Review);
            Assert.IsTrue(completeSummaryOptions.Continue);
        }

        [Test]
        public void BuildsFieldWithSpecifiedValues()
        {
            CompleteSummaryOptions completeSummaryOptions  = CompleteSummaryOptionsBuilder.NewCompleteSummaryOptions()
                .WithoutFrom()
                .WithoutTitle()
                .WithoutMessage()
                .WithoutDownload()
                .WithoutReview()
                .WithoutContinue()
                .Build();

            Assert.IsFalse(completeSummaryOptions.From);
            Assert.IsFalse(completeSummaryOptions.Title);
            Assert.IsFalse(completeSummaryOptions.Message);
            Assert.IsFalse(completeSummaryOptions.Download);
            Assert.IsFalse(completeSummaryOptions.Review);
            Assert.IsFalse(completeSummaryOptions.Continue);
        }
        
    }
}
