using NUnit.Framework;
using System;
using OneSpanSign.Sdk;

namespace SDK.Examples
{
    [TestFixture()]
    public class UpdateInjectedFieldsFromTemplateExampleTest
    {
        [Test()]
        public void VerifyResult()
        {
            UpdateInjectedFieldsFromTemplateExample example = new UpdateInjectedFieldsFromTemplateExample();
            example.Run();
        }
    }
}

