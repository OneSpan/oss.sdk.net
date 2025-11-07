using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace SDK.Examples
{
    [TestFixture()]
    public class SupportingDocumentCloneExampleTest
    {
        [Test()]
        public void VerifyResult()
        {
            SupportingDocumentCloneExample example = new SupportingDocumentCloneExample();
            example.Run();

            HashSet<string> beforeCloning = new HashSet<string>(example.SupportingDocumentBeforeCloning
                .Select(doc => doc.DocumentName));

            HashSet<string> afterCloning = new HashSet<string>(example.SupportingDocumentAfterCloning
                .Select(doc => doc.DocumentName));

            Assert.AreEqual(beforeCloning, afterCloning);
        }
    }
}