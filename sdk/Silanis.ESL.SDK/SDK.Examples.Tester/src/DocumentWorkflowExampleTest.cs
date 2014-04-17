using NUnit.Framework;
using System;
using Silanis.ESL.SDK;

namespace SDK.Examples
{
    [TestFixture()]
    public class DocumentWorkflowExampleTest
    {
        [Test()]
        public void VerifyResult()
        {
            DocumentWorkflowExample example = new DocumentWorkflowExample( Props.GetInstance() );
            example.Run();
            
            DocumentPackage package = example.EslClient.GetPackage( example.packageId );
            Assert.IsNotNull(package);
            
        }
    }
}

