using NUnit.Framework;
using System;

namespace SDK.Examples
{
    [TestFixture()]
    public class SignatureImageExampleTest
    {
        [Test()]
        public void VerifyResult()
        {
            SignatureImageExample example = new SignatureImageExample(Props.GetInstance());
            example.Run();
        }
    }
}

