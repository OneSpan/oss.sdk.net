using System;
using OneSpanSign.Sdk;
using System.Collections.Generic;
using OneSpanSign.Sdk.Builder;

namespace SDK.Examples
{
    public class DelegationExample : SDKSample
    {

        public static void Main(string[] args)
        {
            new DelegationExample().Run();
        }

        public string email7, email8, email9, email10, email11;

        public Sender retrievedOwner, retrievedSender1, retrievedSender2, retrievedSender3,
        retrievedSender4, retrievedSender5, retrievedSender6, retrievedSender7, retrievedSender8, retrievedSender9,
        retrievedSender10, retrievedSender11;
        public DelegationUser delegationUser1, delegationUser2, delegationUser3,
        delegationUser4, delegationUser5, delegationUser6, delegationUser7, delegationUser8, delegationUser9,
        delegationUser10, delegationUser11;
        public IList<DelegationUser> delegationUserListAfterAdding, delegationUserListAfterRemoving, delegationUserListAfterUpdating
            ,delegationUserListAfterClearing, delegationUserListAfterUpdatingWithObjects;

        public DelegationExample()
        {
            this.email1 = Guid.NewGuid().ToString().Replace("-", "") + "@simulator.amazonses.com";
            this.email2 = Guid.NewGuid().ToString().Replace("-", "") + "@simulator.amazonses.com";
            this.email3 = Guid.NewGuid().ToString().Replace("-", "") + "@simulator.amazonses.com";
            this.email4 = Guid.NewGuid().ToString().Replace("-", "") + "@simulator.amazonses.com";
            this.email5 = Guid.NewGuid().ToString().Replace("-", "") + "@simulator.amazonses.com";
            this.email6 = Guid.NewGuid().ToString().Replace("-", "") + "@simulator.amazonses.com";
            this.email7 = Guid.NewGuid().ToString().Replace("-", "") + "@simulator.amazonses.com";
            this.email8 = Guid.NewGuid().ToString().Replace("-", "") + "@simulator.amazonses.com";
            this.email9 = Guid.NewGuid().ToString().Replace("-", "") + "@simulator.amazonses.com";
            this.email10 = Guid.NewGuid().ToString().Replace("-", "") + "@simulator.amazonses.com";
            this.email11 = Guid.NewGuid().ToString().Replace("-", "") + "@simulator.amazonses.com";
        }

        override public void Execute()
        {
            AccountMember ownerMember = GetAccountMember(senderEmail, "firstName", "lastName", "company", "title", "language", "phoneNumber");
            AccountMember accountMember1 = GetAccountMember(email1, "firstName1", "lastName", "company1", "title1", "language1", "phoneNumber1");
            AccountMember accountMember2 = GetAccountMember(email2, "firstName2", "lastName2", "company2", "title2", "language2", "phoneNumber2");
            AccountMember accountMember3 = GetAccountMember(email3, "firstName3", "lastName3", "company3", "title3", "language3", "phoneNumber3");
            AccountMember accountMember4 = GetAccountMember(email4, "firstName4", "lastName4", "company4", "title4", "language4", "phoneNumber4");
            AccountMember accountMember5 = GetAccountMember(email5, "firstName5", "lastName5", "company5", "title5", "language5", "phoneNumber5");
            AccountMember accountMember6 = GetAccountMember(email6, "firstName6", "lastName6", "company6", "title6", "language6", "phoneNumber6");
            AccountMember accountMember7 = GetAccountMember(email7, "firstName7", "lastName7", "company7", "title7", "language7", "phoneNumber7");
            AccountMember accountMember8 = GetAccountMember(email8, "firstName8", "lastName8", "company8", "title8", "language8", "phoneNumber8");
            AccountMember accountMember9 = GetAccountMember(email9, "firstName9", "lastName9", "company9", "title9", "language9", "phoneNumber9");
            AccountMember accountMember10 = GetAccountMember(email10, "firstName10", "lastName10", "company10", "title10", "language10", "phoneNumber10");
            AccountMember accountMember11 = GetAccountMember(email11, "firstName11", "lastName11", "company11", "title11", "language11", "phoneNumber11");

            Sender createdOwnerMember = ossClient.AccountService.InviteUser(ownerMember);
            Sender createdSender1 = ossClient.AccountService.InviteUser(accountMember1);
            Sender createdSender2 = ossClient.AccountService.InviteUser(accountMember2);
            Sender createdSender3 = ossClient.AccountService.InviteUser(accountMember3);
            Sender createdSender4 = ossClient.AccountService.InviteUser(accountMember4);
            Sender createdSender5 = ossClient.AccountService.InviteUser(accountMember5);
            Sender createdSender6 = ossClient.AccountService.InviteUser(accountMember6);
            Sender createdSender7 = ossClient.AccountService.InviteUser(accountMember7);
            Sender createdSender8 = ossClient.AccountService.InviteUser(accountMember8);
            Sender createdSender9 = ossClient.AccountService.InviteUser(accountMember9);
            Sender createdSender10 = ossClient.AccountService.InviteUser(accountMember10);
            Sender createdSender11 = ossClient.AccountService.InviteUser(accountMember11);

            retrievedOwner =   ossClient.AccountService.GetSender(createdOwnerMember.Id);
            retrievedSender1 = ossClient.AccountService.GetSender(createdSender1.Id);
            retrievedSender2 = ossClient.AccountService.GetSender(createdSender2.Id);
            retrievedSender3 = ossClient.AccountService.GetSender(createdSender3.Id);
            retrievedSender4 = ossClient.AccountService.GetSender(createdSender4.Id);
            retrievedSender5 = ossClient.AccountService.GetSender(createdSender5.Id);
            retrievedSender6 = ossClient.AccountService.GetSender(createdSender6.Id);
            retrievedSender7 = ossClient.AccountService.GetSender(createdSender7.Id);
            retrievedSender8 = ossClient.AccountService.GetSender(createdSender8.Id);
            retrievedSender9 = ossClient.AccountService.GetSender(createdSender9.Id);
            retrievedSender10 = ossClient.AccountService.GetSender(createdSender10.Id);
            retrievedSender11 = ossClient.AccountService.GetSender(createdSender11.Id);

            delegationUser1 = DelegationUserBuilder.NewDelegationUser(retrievedSender1).Build();
            delegationUser2 = DelegationUserBuilder.NewDelegationUser(retrievedSender2).Build();
            delegationUser3 = DelegationUserBuilder.NewDelegationUser(retrievedSender3).Build();
            delegationUser4 = DelegationUserBuilder.NewDelegationUser(retrievedSender4).Build();
            delegationUser5 = DelegationUserBuilder.NewDelegationUser(retrievedSender5).Build();
            delegationUser6 = DelegationUserBuilder.NewDelegationUser(retrievedSender6).Build();
            delegationUser7 = DelegationUserBuilder.NewDelegationUser(retrievedSender7).Build();
            delegationUser8 = DelegationUserBuilder.NewDelegationUser(retrievedSender8).Build();
            delegationUser9 = DelegationUserBuilder.NewDelegationUser(retrievedSender9).Build();

            ossClient.AccountService.ClearDelegates(createdOwnerMember.Id);

            ossClient.AccountService.AddDelegate(createdOwnerMember.Id, delegationUser1);
            ossClient.AccountService.AddDelegate(createdOwnerMember.Id, delegationUser2);
            ossClient.AccountService.AddDelegate(createdOwnerMember.Id, delegationUser3);
            delegationUserListAfterAdding = ossClient.AccountService.GetDelegates(createdOwnerMember.Id);

            ossClient.AccountService.RemoveDelegate(createdOwnerMember.Id, delegationUser2.Id);
            delegationUserListAfterRemoving = ossClient.AccountService.GetDelegates(createdOwnerMember.Id);

            List<string> delegateIds = new List<string>();
            delegateIds.Add(delegationUser4.Id);
            delegateIds.Add(delegationUser5.Id);
            delegateIds.Add(delegationUser6.Id);
            delegateIds.Add(delegationUser7.Id);
            delegateIds.Add(delegationUser8.Id);
            delegateIds.Add(delegationUser9.Id);

            ossClient.AccountService.UpdateDelegates(createdOwnerMember.Id, delegateIds);
            delegationUserListAfterUpdating = ossClient.AccountService.GetDelegates(createdOwnerMember.Id);

            ossClient.AccountService.ClearDelegates(createdOwnerMember.Id);
            delegationUserListAfterClearing = ossClient.AccountService.GetDelegates(createdOwnerMember.Id);

            delegationUser10 = DelegationUserBuilder.NewDelegationUser(retrievedSender10).WithExpiryDate(DateTime.Now.AddHours(1)).Build();
            delegationUser11 = DelegationUserBuilder.NewDelegationUser(retrievedSender11).WithExpiryDate(DateTime.Now.AddHours(1)).Build();

            List<DelegationUser> delegates = new List<DelegationUser>();
            delegates.Add(delegationUser10);
            delegates.Add(delegationUser11);

            ossClient.AccountService.updateDelegationWithDelegationUsers(createdOwnerMember.Id, delegates);
            delegationUserListAfterUpdatingWithObjects = ossClient.AccountService.GetDelegates(createdOwnerMember.Id);
        }

        private AccountMember GetAccountMember(string email, string firstName, string lastName, string company, string title, string language, string phoneNumber) 
        {
            return AccountMemberBuilder.NewAccountMember(email)
                .WithFirstName(firstName)
                .WithLastName(lastName)
                .WithCompany(company)
                .WithTitle(title)
                .WithLanguage(language)
                .WithPhoneNumber(phoneNumber)
                .Build();
        }
    }
}

