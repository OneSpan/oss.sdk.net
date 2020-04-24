using System;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using OneSpanSign.Sdk;

namespace SDK.Examples
{
    [TestFixture()]
    public class CreateAndUpdateSubAccountExampleTest 
    {
    [Test()]
    public void VerifyResult()
    {
        IList<Account> subAccountList;
        IList<AccessibleAccountResponse> accessibleAccountList;
        CreateAndUpdateSubAccountExample example = new CreateAndUpdateSubAccountExample();

        try {
            example.Run();
        } catch (OssServerException e) {
            Assert.IsTrue(e.Message.Contains("error.notFound.accountNotFound"));
            return;
        } finally {
            subAccountList = example.subAccounts;
            accessibleAccountList = example.accessibleAccounts;
        }
        
        Assert.GreaterOrEqual(subAccountList.Count, 1);
        Assert.GreaterOrEqual(accessibleAccountList.Count, 1);
        Assert.IsTrue(subAccountList.Any(it => it.Name == CreateAndUpdateSubAccountExample.NAME));
    }
    }
}

