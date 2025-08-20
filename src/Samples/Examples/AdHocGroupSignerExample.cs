using System;
using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Builder;

namespace SDK.Examples
{
    public class AdHocGroupSignerExample : SDKSample
    {
        public static void Main (string[] args)
        {
            new AdHocGroupSignerExample().Run();
        }

        public static readonly String SIGNER_ID = "loanMangerId";
        public static readonly String AD_HOC_GROUP_NAME = "Loan Borrower";
        public static readonly String AD_HOC_GROUP_SIGNER_ID = "loanBorrowerGroupId";
        public static readonly String AD_HOC_GROUP_MEMBER_SIGNER_ID_1 = "loanBorrowerId1";
        public static readonly String AD_HOC_GROUP_MEMBER_SIGNER_ID_2 = "loanBorrowerId2";
        public String signerEmail1;
        public String groupMemberEmail1;
        public String groupMemberEmail2;
        
        public AdHocGroupSignerExample()
        {
	        this.signerEmail1 = GetRandomEmail();
	        this.groupMemberEmail1 = GetRandomEmail();
            this.groupMemberEmail2 = GetRandomEmail();
        }

        override public void Execute()
        {
	        DocumentPackage superDuperPackage = PackageBuilder.NewPackageNamed(PackageName)
		        .WithSigner(SignerBuilder.NewSignerWithEmail(groupMemberEmail1)
			        .WithFirstName("Wife")
			        .WithLastName("Smith")
			        .WithTitle("Title1")
			        .WithCustomId(AD_HOC_GROUP_MEMBER_SIGNER_ID_1)
			        .WithSMSSentTo("+12042345678"))
		        .WithSigner(SignerBuilder.NewSignerWithEmail(groupMemberEmail2)
			        .WithFirstName("Husband")
			        .WithLastName("Smith")
			        .WithTitle("Title2")
			        .WithCustomId(AD_HOC_GROUP_MEMBER_SIGNER_ID_2))
			    .WithSigner(SignerBuilder.NewAdHocGroupSigner(AD_HOC_GROUP_NAME, AD_HOC_GROUP_SIGNER_ID)
			        .WithGroupMember(GroupMemberBuilder.NewAdHocGroupMember(AD_HOC_GROUP_MEMBER_SIGNER_ID_1, GroupMemberType.AD_HOC_GROUP_MEMBER).Build())
			        .WithGroupMember(GroupMemberBuilder.NewAdHocGroupMember(AD_HOC_GROUP_MEMBER_SIGNER_ID_2, GroupMemberType.AD_HOC_GROUP_MEMBER).Build())
		        )
		        .Build();

			packageId = ossClient.CreatePackage(superDuperPackage);
			
			Document document = DocumentBuilder.NewDocumentNamed("First Document")
				.FromStream(fileStream1, DocumentType.PDF)
				.WithSignature(SignatureBuilder.SignatureFor(new Placeholder(AD_HOC_GROUP_SIGNER_ID))
					.OnPage(0)
					.AtPosition(100, 100)).Build();
			ossClient.UploadDocument(document, packageId);	
			
			ossClient.SendPackage(packageId);

        }
    }
}

