using NUnit.Framework;
using System;

namespace SDK.Examples
{
    [TestFixture()]
    public class ApplicationVersionExampleTest
    {
        [Test()]
        public void VerifyResult()
        {
            ApplicationVersionExample example = new ApplicationVersionExample(Props.GetInstance());
            example.Run();

            Assert.IsNotNull(example.applicationVersion);
            Assert.IsNotEmpty(example.applicationVersion);
        }
    }
}

