using System;
using NUnit.Framework;
using System.Collections.Generic;
using OneSpanSign.Sdk;

namespace SDK.Examples
{
    [TestFixture()]
    public class UserAccountRoleExampleTest
    {
        [Test()]
        public void VerifyResult()
        {

            Assert.Throws<OssServerException>(() =>
            {
                UserAccountRolesExample example = new UserAccountRolesExample();
                example.Run();

            });
            
        }
    }
}