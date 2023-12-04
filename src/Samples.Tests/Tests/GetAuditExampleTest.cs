using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using OneSpanSign.Sdk;

namespace SDK.Examples
{
    [TestFixture()]
    public class GetAuditExampleTest
    {
        [Test()]
        public void VerifyResult()
        {
            GetAuditExample example = new GetAuditExample();
            example.Run();

            List<Audit> audits = example.audits;

            Assert.AreEqual(DateTime.Now.Date, Convert.ToDateTime(audits[0].dateTime).Date);
            Assert.AreEqual(example.senderEmail, audits[0].email);
        }
    }
}