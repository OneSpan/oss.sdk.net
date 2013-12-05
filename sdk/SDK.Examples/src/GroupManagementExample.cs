using System;
using System.IO;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;
using System.Collections.Generic;

namespace SDK.Examples
{
    public class GroupManagementExample : SDKSample
    {
        public static void Main (string[] args)
        {
            new GroupManagementExample(Props.GetInstance()).Run();
        }

        private string email1;
        private string email2;
        private string email3;
        private string email4;
        private Stream fileStream1;

        public GroupManagementExample( Props props ) : this(props.Get("api.url"), 
                                                            props.Get("api.key"), 
                                                            props.Get("1.email"), 
                                                            props.Get("2.email"), 
                                                            props.Get("3.email"), 
                                                            props.Get("4.email")) {
        }

        public GroupManagementExample( string apiKey, string apiUrl, string email1, string email2, string email3, string email4 ) : base( apiKey, apiUrl ) {
            this.email1 = email1;
            this.email2 = email2;
            this.email3 = email3;
            this.email4 = email4;
            this.fileStream1 = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/src/document.pdf").FullName);
        }

		private void displayAccountGroupsAndMembers() {
			{
				List<Group> allGroups = eslClient.GroupService.GetMyGroups();
				foreach ( Group group in allGroups ) {
					Console.Out.WriteLine( group.Name + " with email " + group.Email + " and id " + group.Id );
					List<GroupMember> allMembers = eslClient.GroupService.GetGroupMembers( group.Id );
					foreach ( GroupMember member in allMembers ) {
						Console.Out.WriteLine( member.GroupMemberType.ToString() + " " + member.FirstName + " " + member.LastName + " with email " + member.Email);
					}
				}
			}
		}

		private void inviteUsersToMyAccount() {
			// The group members need to be account members, if they aren't already you may need to invite them to your account.
//			eslClient.AccountService.InviteUser(AccountMemberBuilder.NewAccountMember(email1)
//				.WithFirstName("first1")
//				.WithLastName("last1")
//				.WithCompany("company1")
//				.WithTitle("title1")
//				.WithLanguage("language1")
//				.WithPhoneNumber("phoneNumber1")
//				.Build());
//			eslClient.AccountService.InviteUser(AccountMemberBuilder.NewAccountMember(email2)
//				.WithFirstName("first2")
//				.WithLastName("last2")
//				.WithCompany("company2")
//				.WithTitle("title2")
//				.WithLanguage("language2")
//				.WithPhoneNumber("phoneNumber2")
//				.Build());
//			eslClient.AccountService.InviteUser(AccountMemberBuilder.NewAccountMember(email3)
//				.WithFirstName("first3")
//				.WithLastName("last3")
//				.WithCompany("company3")
//				.WithTitle("title3")
//				.WithLanguage("language3")
//				.WithPhoneNumber("phoneNumber3")
//				.Build());
//			eslClient.AccountService.InviteUser(AccountMemberBuilder.NewAccountMember(email4)
//				.WithFirstName("first4")
//				.WithLastName("last4")
//				.WithCompany("company4")
//				.WithTitle("title4")
//				.WithLanguage("language4")
//				.WithPhoneNumber("phoneNumber4")
//				.Build());
		}

        override public void Execute()
        {
			eslClient.PackageService.NotifySigner( new PackageId( "d97d29e5-b00b-4b75-bdd4-d52de54813d7"), new GroupId("f21b5204-61db-4e2b-aee5-80cc2da45e04") );
			/*
			displayAccountGroupsAndMembers();
			Group emptyGroup = GroupBuilder.NewGroup(Guid.NewGuid().ToString())
				.WithId(new GroupId(Guid.NewGuid().ToString()))
				.WithEmail("emptyGroup@email.com")
				.WithoutIndividualMemberEmailing()
				.Build();
			Group createdEmptyGroup = eslClient.GroupService.CreateGroup(emptyGroup);
			List<GroupMember> retrievedEmptyGroup = eslClient.GroupService.GetGroupMembers(createdEmptyGroup.Id);
			eslClient.GroupService.InviteMember(createdEmptyGroup.Id,
				GroupMemberBuilder.NewGroupMember(email1)
				.AsMemberType(GroupMemberType.MANAGER)
				.Build());
			eslClient.GroupService.InviteMember(createdEmptyGroup.Id,
				GroupMemberBuilder.NewGroupMember(email3)
				.AsMemberType(GroupMemberType.MANAGER)
				.Build());
			Console.Out.WriteLine("GroupId: " + createdEmptyGroup.Id.Id);
			retrievedEmptyGroup = eslClient.GroupService.GetGroupMembers(createdEmptyGroup.Id);

			Group group1 = GroupBuilder.NewGroup(Guid.NewGuid().ToString())
                    .WithId(new GroupId(Guid.NewGuid().ToString()))
					.WithMember(GroupMemberBuilder.NewGroupMember(email1)
						.AsMemberType(GroupMemberType.MANAGER))
					.WithMember(GroupMemberBuilder.NewGroupMember(email3)
						.AsMemberType(GroupMemberType.MANAGER))
                    .WithEmail("bob@aol.com")
                    .WithIndividualMemberEmailing()
                    .Build();
            Group createdGroup1 = eslClient.GroupService.CreateGroup(group1);
			Console.Out.WriteLine("GroupId #1: " + createdGroup1.Id.Id);

			eslClient.GroupService.InviteMember( createdGroup1.Id,
                                                GroupMemberBuilder.NewGroupMember( email3 )
                                                .AsMemberType( GroupMemberType.MANAGER )
                                                .Build() );

            eslClient.GroupService.InviteMember(createdGroup1.Id,
                GroupMemberBuilder.NewGroupMember(email4)
                                                .AsMemberType(GroupMemberType.REGULAR)
                                                .Build());

            Group retrievedGroup1 = eslClient.GroupService.GetGroup(createdGroup1.Id);
            Group group2 = GroupBuilder.NewGroup(Guid.NewGuid().ToString())
                .WithMember(GroupMemberBuilder.NewGroupMember(email2)
					.AsMemberType(GroupMemberType.MANAGER) )
                    .WithEmail("bob@aol.com")
                    .WithIndividualMemberEmailing()
                    .Build();
            Group createdGroup2 = eslClient.GroupService.CreateGroup(group2);
			Console.Out.WriteLine("GroupId #2: " + createdGroup2.Id.Id);
            Group retrievedGroup2 = eslClient.GroupService.GetGroup(createdGroup2.Id);
            
			List<Group> allGroupsBeforeDelete = eslClient.GroupService.GetMyGroups();

			DocumentPackage superDuperPackage = PackageBuilder.NewPackageNamed("GroupManagementExmaple " + DateTime.Now.ToString())
			    .WithSigner(SignerBuilder.NewSignerFromGroup(createdGroup1.Id)
			                .CanChangeSigner()
			                .DeliverSignedDocumentsByEmail())
			        .WithDocument(DocumentBuilder.NewDocumentNamed("First Document")
			                      .FromStream(fileStream1, DocumentType.PDF)
			                      .WithSignature(SignatureBuilder.SignatureFor(createdGroup1.Id)
			                       .OnPage(0)
			                       .AtPosition(100, 100)))
			        .Build();

			PackageId packageId = eslClient.CreatePackage(superDuperPackage);
			eslClient.SendPackage(packageId);

			eslClient.PackageService.NotifySigner(packageId, createdGroup1.Id);

			DocumentPackage result = eslClient.GetPackage(packageId);
			*/
        }
    }
}

