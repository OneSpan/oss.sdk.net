using NUnit.Framework;
using OneSpanSign.Sdk;

namespace SDK.Tests
{
    public class OverviewOptionsBuilderTest
    {
        [Test]
        public void BuildsFieldWithDefaultValues()
        {
            OverviewOptions overviewOptions  = OverviewOptionsBuilder.NewOverviewOptions()
                .WithTitle()
                .WithBody()
                .WithDocumentSection()
                .WithUploadSection()
                .Build();

            Assert.IsTrue(overviewOptions.Title);
            Assert.IsTrue(overviewOptions.Body);
            Assert.IsTrue(overviewOptions.DocumentSection);
            Assert.IsTrue(overviewOptions.UploadSection);
        }

        [Test]
        public void BuildsFieldWithSpecifiedValues()
        {
            OverviewOptions overviewOptions  = OverviewOptionsBuilder.NewOverviewOptions()
                .WithoutTitle()
                .WithoutBody()
                .WithoutDocumentSection()
                .WithoutUploadSection()
                .Build();
            
            Assert.IsFalse(overviewOptions.Title);
            Assert.IsFalse(overviewOptions.Body);
            Assert.IsFalse(overviewOptions.DocumentSection);
            Assert.IsFalse(overviewOptions.UploadSection);
        }
        
    }
}
