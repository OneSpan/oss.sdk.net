using System;
using NUnit.Framework;

namespace SDK.Examples
{
    [TestFixture()]
    public class CreateTemplateFromPackageExampleTest
    {
        [Test()]
        public void VerifyResult()
        {
            CreateTemplateFromPackageExample example = new CreateTemplateFromPackageExample( Props.GetInstance() );
            example.Run();
        }
    }
}

