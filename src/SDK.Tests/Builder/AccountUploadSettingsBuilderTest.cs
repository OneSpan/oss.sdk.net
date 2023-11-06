using NUnit.Framework;
using OneSpanSign.Sdk;
using System.Collections.Generic;

namespace SDK.Tests
{
    [TestFixture]
    public class AccountUploadSettingsBuilderTest
    {
        public AccountUploadSettingsBuilderTest()
        {

        }

        [Test]
        public void buildWithSpecifiedValues()
        {

            AccountUploadSettings accountUploadSettings = AccountUploadSettingsBuilder
                .NewAccountUploadSettings()
                .WithAllowedFileTypes(new List<string> { "FileType1", "FileType2" })
                .Build();

            Assert.IsTrue(accountUploadSettings.AllowedFileTypes.Contains("FileType1"));
            Assert.IsTrue(accountUploadSettings.AllowedFileTypes.Contains("FileType2"));

        }
    }
}