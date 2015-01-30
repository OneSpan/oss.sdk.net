using NUnit.Framework;
using System;

namespace SDK.Examples
{
    [TestFixture()]
    public class NotaryJournalExampleTest
    {
        [Test()]
        public void VerifyResult()
        {
            NotaryJournalExample example = new NotaryJournalExample(Props.GetInstance());
            example.Run();

            Assert.IsNotNull(example.sdkJournalEntries);
            Assert.IsNotNull(example.csvJournalEntries);
        }
    }
}

