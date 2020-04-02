using NUnit.Framework;
using System;
using OneSpanSign.Sdk;

namespace SDK.Examples
{
    [TestFixture()]
    public class DocumentWorkflowExampleTest
    {
        [Test()]
        public void VerifyResult()
        {
            DocumentWorkflowExample example = new DocumentWorkflowExample(  );
            example.Run();
            
            DocumentPackage package = example.OssClient.GetPackage( example.PackageId );
            Assert.IsNotNull(package);
            
        }
    }
}

