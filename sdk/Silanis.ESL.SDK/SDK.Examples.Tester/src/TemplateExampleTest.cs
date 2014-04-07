using System;
using NUnit.Framework;

namespace SDK.Examples
{
    [TestFixture()]
    public class TemplateExampleTest
    {
        [Test()]
        public void VerifyResult()
        {
            TemplateExample example = new TemplateExample( Props.GetInstance() );
            example.Run();
        }
    }
}

