using NUnit.Framework;
using System;

namespace SDK.Examples
{
    [TestFixture()]
    public class SendSmsToSignerExampleTest
    {
        [Test()]
        public void VerifyResult()
        {
            SendSmsToSignerExample example = new SendSmsToSignerExample(Props.GetInstance());
            example.Run();
        }
    }
}

