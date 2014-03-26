using NUnit.Framework;
using System;

namespace SDK.Examples
{
    [TestFixture()]
    public class BasicPackageCreationExampleTest
    {
        [Test()]
        public void VerifyResult()
        {
            BasicPackageCreationExample example = new BasicPackageCreationExample( Props.GetInstance() );
            example.Run();
        }
    }
}

