using NUnit.Framework;
using System;
using OneSpanSign.Sdk;

namespace SDK.Examples
{
    [TestFixture()]
    public class FieldInjectionExampleTest
    {
        [Test()]
        public void VerifyResult()
        {
            FieldInjectionExample example = new FieldInjectionExample(  );
            example.Run();
            
            // InjectedField list is not returned by the oss-backend.
            Assert.IsNotNull(example.PackageId);
        }
    }
}

