using NUnit.Framework;
using System;
using OneSpanSign.Sdk;

namespace SDK.Examples
{
    [TestFixture()]
    public class ListTemplatesExampleTest
    {
        [Test()]
        public void VerifyResult()
        {
            ListTemplatesExample example = new ListTemplatesExample();
            example.Run();

            Page<DocumentPackage> templateList = example.Templates;

            Assert.GreaterOrEqual(templateList.Size, 0);
        }
    }
}

