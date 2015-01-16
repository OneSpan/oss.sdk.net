using NUnit.Framework;
using System;

namespace SDK.Examples
{
    [TestFixture()]
    public class GetCompletedPackagesWithinDateRangeExampleTest
    {
        [Test]
        public void verify() {
            GetCompletedPackagesWithinDateRangeExample example = new GetCompletedPackagesWithinDateRangeExample(Props.GetInstance());
            example.Run();
        }
    }
}

