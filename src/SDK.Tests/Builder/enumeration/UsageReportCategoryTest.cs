using System;
using NUnit.Framework;
using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Builder;
using OneSpanSign.API;

namespace SDK.Tests
{
    [TestFixture]
    public class UsageReportCategoryTest
    {
        [Test]
        public void whenBuildingUsageReportCategoryWithAPIValueACTIVEThenACTIVEUsageReportCategoryIsReturned()
        {
            string expectedSDKValue = "ACTIVE";


            OneSpanSign.Sdk.UsageReportCategory classUnderTest = OneSpanSign.Sdk.UsageReportCategory.valueOf("ACTIVE");
            String actualSDKValue = classUnderTest.getSdkValue();


            Assert.AreEqual(expectedSDKValue, actualSDKValue);
        }

        [Test]
        public void whenBuildingUsageReportCategoryWithAPIValueDRAFTThenDRAFTUsageReportCategoryIsReturned()
        {
            string expectedSDKValue = "DRAFT";


            OneSpanSign.Sdk.UsageReportCategory classUnderTest = OneSpanSign.Sdk.UsageReportCategory.valueOf("DRAFT");
            String actualSDKValue = classUnderTest.getSdkValue();


            Assert.AreEqual(expectedSDKValue, actualSDKValue);
        }

        [Test]
        public void whenBuildingUsageReportCategoryWithAPIValueSENTThenSENTUsageReportCategoryIsReturned()
        {
            string expectedSDKValue = "SENT";


            OneSpanSign.Sdk.UsageReportCategory classUnderTest = OneSpanSign.Sdk.UsageReportCategory.valueOf("SENT");
            String actualSDKValue = classUnderTest.getSdkValue();


            Assert.AreEqual(expectedSDKValue, actualSDKValue);
        }
        [Test]
        public void whenBuildingUsageReportCategoryWithAPIValueCOMPLETEDThenCOMPLETEDUsageReportCategoryIsReturned()
        {
            string expectedSDKValue = "COMPLETED";


            OneSpanSign.Sdk.UsageReportCategory classUnderTest = OneSpanSign.Sdk.UsageReportCategory.valueOf("COMPLETED");
            String actualSDKValue = classUnderTest.getSdkValue();


            Assert.AreEqual(expectedSDKValue, actualSDKValue);
        }

        [Test]
        public void whenBuildingUsageReportCategoryWithAPIValueARCHIVEDThenARCHIVEDUsageReportCategoryIsReturned()
        {
            string expectedSDKValue = "ARCHIVED";


            OneSpanSign.Sdk.UsageReportCategory classUnderTest = OneSpanSign.Sdk.UsageReportCategory.valueOf("ARCHIVED");
            String actualSDKValue = classUnderTest.getSdkValue();


            Assert.AreEqual(expectedSDKValue, actualSDKValue);
        }

        [Test]
        public void whenBuildingUsageReportCategoryWithAPIValueDECLINEDThenDECLINEDUsageReportCategoryIsReturned()
        {
            string expectedSDKValue = "DECLINED";


            OneSpanSign.Sdk.UsageReportCategory classUnderTest = OneSpanSign.Sdk.UsageReportCategory.valueOf("DECLINED");
            String actualSDKValue = classUnderTest.getSdkValue();


            Assert.AreEqual(expectedSDKValue, actualSDKValue);
        }

        [Test]
        public void whenBuildingUsageReportCategoryWithAPIValueOPTED_OUTThenOPTED_OUTUsageReportCategoryIsReturned()
        {
            string expectedSDKValue = "OPTED_OUT";


            OneSpanSign.Sdk.UsageReportCategory classUnderTest = OneSpanSign.Sdk.UsageReportCategory.valueOf("OPTED_OUT");
            String actualSDKValue = classUnderTest.getSdkValue();


            Assert.AreEqual(expectedSDKValue, actualSDKValue);
        }
        [Test]
        public void whenBuildingUsageReportCategoryWithAPIValueEXPIREDThenEXPIREDUsageReportCategoryIsReturned()
        {
            string expectedSDKValue = "EXPIRED";


            OneSpanSign.Sdk.UsageReportCategory classUnderTest = OneSpanSign.Sdk.UsageReportCategory.valueOf("EXPIRED");
            String actualSDKValue = classUnderTest.getSdkValue();


            Assert.AreEqual(expectedSDKValue, actualSDKValue);
        }
        [Test]
        public void whenBuildingUsageReportCategoryWithAPIValueTRASHEDThenTRASHEDUsageReportCategoryIsReturned()
        {
            string expectedSDKValue = "TRASHED";


            OneSpanSign.Sdk.UsageReportCategory classUnderTest = OneSpanSign.Sdk.UsageReportCategory.valueOf("TRASHED");
            String actualSDKValue = classUnderTest.getSdkValue();


            Assert.AreEqual(expectedSDKValue, actualSDKValue);
        }

        [Test]
        public void whenBuildingUsageReportCategoryWithUnknownAPIValueThenUNRECOGNIZEDUsageReportCategoryIsReturned()
        {
            string expectedSDKValue = "UNRECOGNIZED";


            OneSpanSign.Sdk.UsageReportCategory classUnderTest = OneSpanSign.Sdk.UsageReportCategory.valueOf("ThisUsageReportCategoryDoesNotExistINSDK");
            String actualSDKValue = classUnderTest.getSdkValue();


            Assert.AreEqual(expectedSDKValue, actualSDKValue);
        }
    }
}   