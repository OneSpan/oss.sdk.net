using System;
using Silanis.ESL.SDK;
using System.Collections.Generic;

namespace SDK.Examples
{
    public class SenderManipulationExample : SDKSample
    {
        public string email1;
        public string email2;
        public string email3;

        public AccountMember accountMember1;
        public AccountMember accountMember2;
        public AccountMember accountMember3;
        public SenderInfo updatedSenderInfo;

        public IDictionary<string, Sender> accountMembers;
        public IDictionary<string, Sender> accountMembersWithDeletedSender;
        public IDictionary<string, Sender> accountMembersWithUpdatedSender;

        public SenderManipulationExample(Props props) : this(props.Get("api.url"), props.Get("api.key"), props.Get("1.email"), props.Get("2.email"), props.Get("3.email"))
        {
        }

        public SenderManipulationExample(string apiKey, string apiUrl, string email1, string email2, string email3) : base( apiKey, apiUrl )
        {
            this.email1 = email1;
            this.email2 = email2;
            this.email3 = email3;
        }

        override public void Execute()
        {
            accountMember1 = AccountMemberBuilder.NewAccountMember(email1)
                .WithFirstName( "firstName1" )
                .WithLastName( "lastName1" )
                .WithCompany( "company1" )
                .WithTitle( "title1" )
                .WithLanguage( "language1" )
                .WithPhoneNumber( "phoneNumber1" )
                .WithStatus(SenderStatus.ACTIVE)
                .Build();

            accountMember2 = AccountMemberBuilder.NewAccountMember(email2)
                .WithFirstName( "firstName2" )
                .WithLastName( "lastName2" )
                .WithCompany( "company2" )
                .WithTitle( "title2" )
                .WithLanguage( "language2" )
                .WithPhoneNumber( "phoneNumber2" )
                .WithStatus(SenderStatus.ACTIVE)
                .Build();

            accountMember3 = AccountMemberBuilder.NewAccountMember(email3)
                .WithFirstName( "firstName3" )
                .WithLastName( "lastName3" )
                .WithCompany( "company3" )
                .WithTitle( "title3" )
                .WithLanguage( "language3" )
                .WithPhoneNumber( "phoneNumber3" )
                .WithStatus(SenderStatus.ACTIVE)
                .Build();

            Sender createdSender1 = eslClient.AccountService.InviteUser(accountMember1);
            Sender retrievedSender1 = eslClient.AccountService.GetSender(createdSender1.Id);
            Sender createdSender2 = eslClient.AccountService.InviteUser(accountMember2);
            Sender createdSender3 = eslClient.AccountService.InviteUser(accountMember3);

            Console.Out.WriteLine(email2);

            eslClient.AccountService.SendInvite(createdSender1.Id);

            accountMembers = eslClient.AccountService.GetSenders(Direction.ASCENDING, new PageRequest(1, 1000));

            eslClient.AccountService.DeleteSender(createdSender2.Id);

            accountMembersWithDeletedSender = eslClient.AccountService.GetSenders(Direction.ASCENDING, new PageRequest(1, 1000));

            updatedSenderInfo = SenderInfoBuilder.NewSenderInfo(email3)
                .WithName("updatedFirstName", "updatedLastName")
                    .WithCompany("updatedCompany")
                    .WithTitle("updatedTitle")
                    .Build();

            eslClient.AccountService.UpdateSender(updatedSenderInfo, accountMembersWithDeletedSender[email3].Id);
            accountMembersWithUpdatedSender = eslClient.AccountService.GetSenders(Direction.ASCENDING, new PageRequest(1, 1000));
        }
    }
}

