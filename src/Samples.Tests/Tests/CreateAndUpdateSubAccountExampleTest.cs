using System;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using OneSpanSign.Sdk;

namespace SDK.Examples
{
    /*
     * Since a fake AccountID is used in example class, a try catch statement is used to cover the "accountNotFound" exception. once a real account ID is used, the test will skip catch statement and assert response.
     */
    [TestFixture()]
    public class CreateAndUpdateSubAccountExampleTest 
    {
    [Test()]
    public void VerifyResult()
    {
        IList<Account> subAccountList;
        IList<AccessibleAccountResponse> accessibleAccountList; 
        IList<SubAccountApiKey> subAccountApiKeys;
        CreateAndUpdateSubAccountExample example = new CreateAndUpdateSubAccountExample();

        try {
            example.Run();
        } catch (OssServerException e) {
            Assert.IsTrue(e.Message.Contains("error.notFound.accountNotFound") || e.Message.Contains("error.forbidden.noPermission"));
            return;
        } finally {
            subAccountList = example.subAccounts;
            accessibleAccountList = example.accessibleAccounts;
            subAccountApiKeys = example.subAccountApiKeys;
        }
        
        Assert.GreaterOrEqual(subAccountList.Count, 1);
        Assert.GreaterOrEqual(accessibleAccountList.Count, 1);
        Assert.GreaterOrEqual(subAccountApiKeys.Count, 1);
        Assert.IsTrue(subAccountList.Any(it => it.Name == CreateAndUpdateSubAccountExample.NAME));
    }
    }
}

