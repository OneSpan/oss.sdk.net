using System;
using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Builder;

namespace SDK.Examples
{
    public class NotificationMethodsForAdhocGroupExample : SDKSample
    {
        public static readonly String AD_HOC_GROUP_NAME = "Loan Borrower";
        public static readonly String AD_HOC_GROUP_SIGNER_ID = "loanBorrowerGroupId";
        public static readonly String AD_HOC_GROUP_MEMBER_SIGNER_ID_1 = "loanBorrowerId1";
        public static readonly String AD_HOC_GROUP_MEMBER_SIGNER_ID_2 = "loanBorrowerId2";
        
        public static void Main(string[] args)
        {
            new NotificationMethodsForAdhocGroupExample().Run();
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
                        .WithPhoneNumber("+12042345678")
                    ))
                .WithSigner(SignerBuilder.NewAdHocGroupSigner(AD_HOC_GROUP_NAME, AD_HOC_GROUP_SIGNER_ID)
                    .WithGroupMember(GroupMemberBuilder.NewAdHocGroupMember(AD_HOC_GROUP_MEMBER_SIGNER_ID_1, GroupMemberType.AD_HOC_GROUP_MEMBER).Build())
                    .WithGroupMember(GroupMemberBuilder.NewAdHocGroupMember(AD_HOC_GROUP_MEMBER_SIGNER_ID_2, GroupMemberType.AD_HOC_GROUP_MEMBER).Build())
                )
                .Build();

            packageId = ossClient.CreatePackage(package); 
            retrievedPackage = ossClient.GetPackage(packageId);
        }
    }
}