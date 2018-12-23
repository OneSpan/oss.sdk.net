using NUnit.Framework;
using System;
using Silanis.ESL.SDK;

namespace SDK.Examples
{
    [TestFixture()]
    public class GetCompletedPackagesWithinDateRangeExampleTest
    {
        [Test]
        public void verify() {
            GetCompletedPackagesWithinDateRangeExample example = new GetCompletedPackagesWithinDateRangeExample();
            example.Run();

            assertEqualsPackageUpdatedDate(example.draftPackages, example.START_DATE, example.END_DATE);
            assertEqualsPackageUpdatedDate(example.sentPackages, example.START_DATE, example.END_DATE);
            assertEqualsPackageUpdatedDate(example.declinedPackages, example.START_DATE, example.END_DATE);
            assertEqualsPackageUpdatedDate(example.archivedPackages, example.START_DATE, example.END_DATE);
            assertEqualsPackageUpdatedDate(example.completedPackages, example.START_DATE, example.END_DATE);
        }

        private void assertEqualsPackageUpdatedDate(Page<DocumentPackage> packages, DateTime startDate, DateTime endDate) {
            foreach(DocumentPackage draftPackage in packages) {
                int offset = DateTimeOffset.Now.Offset.Hours;
                Assert.GreaterOrEqual(draftPackage.UpdatedDate, startDate.Date);
                Assert.Less(draftPackage.UpdatedDate, endDate.Date.AddHours(-offset).AddDays(1));
            }
        }
    }
}

