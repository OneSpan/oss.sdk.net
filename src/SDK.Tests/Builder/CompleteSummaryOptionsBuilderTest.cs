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
                .WithTitle()
                .WithMessage()
                .WithDownload()
                .WithReview()
                .WithContinue()
                .WithDocumentSection()
                .WithUploadSection()
                .Build();

            Assert.IsTrue(completeSummaryOptions.Title);
            Assert.IsTrue(completeSummaryOptions.Message);
            Assert.IsTrue(completeSummaryOptions.Download);
            Assert.IsTrue(completeSummaryOptions.Review);
            Assert.IsTrue(completeSummaryOptions.Continue);
            Assert.IsTrue(completeSummaryOptions.DocumentSection);
            Assert.IsTrue(completeSummaryOptions.UploadSection);
        }

        [Test]
        public void BuildsFieldWithSpecifiedValues()
        {
            CompleteSummaryOptions completeSummaryOptions  = CompleteSummaryOptionsBuilder.NewCompleteSummaryOptions()
                .WithoutTitle()
                .WithoutMessage()
                .WithoutDownload()
                .WithoutReview()
                .WithoutContinue()
                .WithoutDocumentSection()
                .WithoutUploadSection()
                .Build();

            Assert.IsFalse(completeSummaryOptions.Title);
            Assert.IsFalse(completeSummaryOptions.Message);
            Assert.IsFalse(completeSummaryOptions.Download);
            Assert.IsFalse(completeSummaryOptions.Review);
            Assert.IsFalse(completeSummaryOptions.Continue);
            Assert.IsFalse(completeSummaryOptions.DocumentSection);
            Assert.IsFalse(completeSummaryOptions.UploadSection);
        }
        
    }
}
