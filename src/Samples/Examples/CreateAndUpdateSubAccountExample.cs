using System;
using System.Collections.Generic;
using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Builder;

namespace SDK.Examples
{
    public class CreateAndUpdateSubAccountExample : SDKSample
    {
        public static void Main(string[] args)
        {
            new CreateAndUpdateSubAccountExample().Run();
        }

        public IList<Account> subAccounts;
        public IList<AccessibleAccountResponse> accessibleAccounts;
        public IList<SubAccountApiKey> subAccountApiKeys;

        private static readonly string PARENT_ACCOUNT_ID = "dummyAccountId";
        public static readonly string NAME = "SubAccount_" + DateTime.Now.ToString("HH:mm:ss");
        private static readonly string TIMEZONE_ID = "GMT";
        private static readonly string LANGUAGE = "en";
        private static readonly string UPDATE_TIMEZONE_ID = "Europe/Prague";
        private static readonly string UPDATE_LANGUAGE = "it";
        override public void Execute()
        {
            SubAccount subAccount = SubAccountBuilder.NewSubAccount()
                .WithName(NAME)
                .WithParentAccountId(PARENT_ACCOUNT_ID)
                .WithLanguage(LANGUAGE)
                .WithTimezoneId(TIMEZONE_ID)
                .Build();

            //Creates subAccount
            Account account = ossClient.AccountService.createSubAccount(subAccount);

            Console.WriteLine(account);
            
            SubAccount updateSubAccount = SubAccountBuilder.NewSubAccount()
                .WithLanguage(UPDATE_LANGUAGE)
                .WithTimezoneId(UPDATE_TIMEZONE_ID)
                .Build();
            
            //Updates subAccount
            ossClient.AccountService.updateSubAccount(updateSubAccount, account.Id);
            
            //Lists accessibleAccounts
            accessibleAccounts = ossClient.AccountService.getAccessibleAccounts();
            
            //Lists subAccounts Api Key
            subAccountApiKeys = ossClient.AccountService.getSubAccountApiKey();
            
            //Lists subAccounts
            subAccounts = ossClient.AccountService.getSubAccounts();
        }
    }
}

