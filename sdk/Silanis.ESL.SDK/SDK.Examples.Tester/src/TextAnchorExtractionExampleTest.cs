using NUnit.Framework;
using System;
using Silanis.ESL.SDK;

namespace SDK.Examples
{
    [TestFixture()]
    public class TextAnchorExtractionExampleTest 
    {
        [Test()]
        public void VerifyResult()
        {
            TextAnchorExtractionExample example = new TextAnchorExtractionExample(Props.GetInstance());
            example.Run();
        }
    }
}

