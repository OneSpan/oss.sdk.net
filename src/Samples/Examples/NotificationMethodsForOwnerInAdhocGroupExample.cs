using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Builder;
using OneSpanSign.Sdk.Internal;

namespace SDK.Examples
{
    public class NotificationMethodsForOwnerInAdhocGroupExample : SDKSample
    {
        public static readonly string SIGNER_PHONE = "+12042345678";
        public static readonly string OWNER_PHONE = "+18765432100";
        
        private static readonly string AD_HOC_GROUP_NAME = "Loan Borrower";
        private static readonly string AD_HOC_GROUP_SIGNER_ID = "loanBorrowerGroupId";
        private static readonly string AD_HOC_GROUP_MEMBER_SIGNER_ID_1 = "loanBorrowerId1";
        private static readonly string AD_HOC_GROUP_MEMBER_SIGNER_ID_2 = "loanBorrowerId2";
        private static readonly string SIGNER_TYPE_ACCOUNT_SENDER = "ACCOUNT_SENDER";
        private static readonly string SIGNER_TYPE_ADHOC_GROUP_MEMBER= "AD_HOC_GROUP_MEMBER";
        
        public DocumentPackage updatedPackage;
        public string ownerSignerEmail;
        
        public static void Main(string[] args)
        {
            new NotificationMethodsForOwnerInAdhocGroupExample().Run();
        }
        
        override public void Execute()
        {
            DocumentPackage package = PackageBuilder.NewPackageNamed(PackageName)
                .WithSigner(SignerBuilder.NewSignerWithEmail(email1)
                    .WithFirstName("John")
                    .WithLastName("Smith")
                    .WithCustomId(AD_HOC_GROUP_MEMBER_SIGNER_ID_1)
                    .WithNotificationMethods(NotificationMethodsBuilder.NewNotificationMethods()
                        .WithPrimaryMethods(NotificationMethod.EMAIL)
                    ))
                .WithSigner(SignerBuilder.NewSignerWithEmail(email2)
                    .WithFirstName("Jane")
                    .WithLastName("Cooked")
                    .WithCustomId(AD_HOC_GROUP_MEMBER_SIGNER_ID_2)
                    .WithNotificationMethods(NotificationMethodsBuilder.NewNotificationMethods()
                        .WithPrimaryMethods(NotificationMethod.EMAIL, NotificationMethod.SMS)
                        .WithPhoneNumber(SIGNER_PHONE)
                    ))
                .WithSigner(SignerBuilder.NewAdHocGroupSigner(AD_HOC_GROUP_NAME, AD_HOC_GROUP_SIGNER_ID)
                    .WithGroupMember(GroupMemberBuilder.NewAdHocGroupMember(AD_HOC_GROUP_MEMBER_SIGNER_ID_1, GroupMemberType.AD_HOC_GROUP_MEMBER).Build())
                    .WithGroupMember(GroupMemberBuilder.NewAdHocGroupMember(AD_HOC_GROUP_MEMBER_SIGNER_ID_2, GroupMemberType.AD_HOC_GROUP_MEMBER).Build())
                )
                .Build();

            packageId = ossClient.CreatePackage(package);
            retrievedPackage = ossClient.GetPackage(packageId);

            // retrieve owner and group signers from a package and update them accordingly
            DocumentPackage aPackage = ossClient.GetPackage(packageId);
            Signer ownerSigner = GetSignerWithType(aPackage, SIGNER_TYPE_ACCOUNT_SENDER);
            Asserts.NotNull(ownerSigner, "Owner signer not found");
            Signer groupSigner = GetSignerWithType(aPackage, SIGNER_TYPE_ADHOC_GROUP_MEMBER);
            Asserts.NotNull(groupSigner, "Ad-hoc group signer not found");

            // update notification methods for owner signer
            ownerSigner.NotificationMethods = NotificationMethodsBuilder.NewNotificationMethods()
                .WithPrimaryMethods(NotificationMethod.EMAIL, NotificationMethod.SMS)
                .WithPhoneNumber(OWNER_PHONE).Build();
            ossClient.PackageService.UpdateSigner(PackageId, ownerSigner);
            
            // add owner signer as adhoc group member
            groupSigner.Group.Members.Add(GroupMemberBuilder.NewAdHocGroupMember(ownerSigner.Id, GroupMemberType.AD_HOC_GROUP_MEMBER).Build());
            ossClient.PackageService.UpdateSigner(PackageId, groupSigner);

            updatedPackage = ossClient.GetPackage(packageId);
            ownerSignerEmail = ossClient.PackageService.GetSigner(packageId, ownerSigner.Id).Email;
        }
        
        private static Signer GetSignerWithType(DocumentPackage package, string signerType)
        {
            foreach(Signer signer in package.Signers)
            {
                if (signer.SignerType == signerType)
                {
                    return signer;
                }
                // a workaround for the SDK bug when adhoc signers do not have SignerType set 
                if(signerType == SIGNER_TYPE_ADHOC_GROUP_MEMBER && signer.IsAdHocGroupSigner()) {
                    return signer;
                }
            }
            return null;
        }
    }
}
