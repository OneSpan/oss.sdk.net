using NUnit.Framework;
using System;
using System.Collections;

namespace SDK.Examples
{
    [TestFixture]
    public class DeclineReasonsExampleTest
    {
        [Test]
        public void VerifyResult()
        {
            DeclineReasonsExample example = new DeclineReasonsExample();
            example.Run();
            
            Assert.IsNotNull(example.CreatedDeclineReasons);
            Assert.Contains("Decline Reason 1", (ICollection) example.CreatedDeclineReasons);
            
            Assert.IsNotNull(example.RetrievedDeclineReasons);
            
            Assert.IsNotNull(example.UpdatedDeclineReasons);
            Assert.Contains("Decline Reason 3", (ICollection) example.UpdatedDeclineReasons);
            
            CollectionAssert.IsEmpty(example.DeclineReasonsAfterDelete);
        }
    }
}