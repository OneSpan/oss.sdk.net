using System;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;
using System.IO;
using System.Collections.Generic;

namespace SDK.Examples
{
	public class AttachmentRequirementExample : SDKSample
    {
		public static void Main(string[] args)
		{
			new TemplateExample(Props.GetInstance()).Run();
		}

		private string email1;
		private string email2;
		private Signer signer1;
		private string attachment1Id;
		private DocumentPackage retrievedPackage;

		public readonly string name1 = "Driver's license";
		public readonly string description1 = "Please upload a scanned copy of your driver's license.";
		public readonly string name2 = "Medicare card";
		public readonly string description2 = "Optional attachment.";
		public readonly string name3 = "Third Attachment";
		public readonly string description3 = "Third description";
		public readonly string signer1Id = "signer1Id";
		public readonly string rejectionComment = "Reject: uploaded wrong attachment.";

		public AttachmentRequirementExample(Props props) : this(props.Get("api.url"), props.Get("api.key"), props.Get("1.email"), props.Get("2.email"))
		{
		}

		public AttachmentRequirementExample(string apiKey, string apiUrl, string email1, string email2) : base( apiKey, apiUrl )
		{
			this.email1 = email1;
			this.email2 = email2;
		}

		public string Email1
		{
			get { return email1; }
		}

		public string Email2
		{
			get { return email2; }
		}

		public DocumentPackage RetrievedPackage
		{
			get { return retrievedPackage; }
		}

		override public void Execute()
		{
			// Signer1 with 1 attachment requirement
			signer1 = SignerBuilder.NewSignerWithEmail(email1)
				.WithFirstName("John")
				.WithLastName("Smith")
				.WithCustomId(signer1Id)
				.WithAttachmentRequirement(AttachmentRequirementBuilder.NewAttachmentRequirementWithName(name1)
					.WithDescription(description1)
					.IsRequiredAttachment()
					.Build())
				.Build();

			// Signer2 with 2 attachment requirements
			Signer signer2 = SignerBuilder.NewSignerWithEmail(email2)
				.WithFirstName("Patty")
				.WithLastName("Galant")
				.WithAttachmentRequirement(AttachmentRequirementBuilder.NewAttachmentRequirementWithName(name2)
					.WithDescription(description2)
					.Build())
				.WithAttachmentRequirement(AttachmentRequirementBuilder.NewAttachmentRequirementWithName(name3)
					.WithDescription(description3)
					.IsRequiredAttachment()
					.Build())
				.Build();


			Stream file = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/src/document.pdf").FullName);

			DocumentPackage superDuperPackage = PackageBuilder.NewPackageNamed("AttachmentRequirementExample: " + DateTime.Now)
				.DescribedAs("This is a package created using the e-SignLive SDK")
				.WithSigner(signer1)
				.WithSigner(signer2)
				.WithDocument(DocumentBuilder.NewDocumentNamed("test document")
					.FromStream(file, DocumentType.PDF)
					.WithSignature(SignatureBuilder.SignatureFor(email1)
						.Build())
					.Build())
				.Build();

			packageId = eslClient.CreateAndSendPackage(superDuperPackage);

			retrievedPackage = eslClient.GetPackage(packageId);

			attachment1Id = retrievedPackage.Signers[email1].Attachments[name1].Id;
			signer1 = retrievedPackage.Signers[email1];

			// Signer1 uploads required attachment
			// Sender can accept/reject the uploaded attachment

		}

		// Sender rejects Signer1's uploaded attachment
		public void RejectAttachment()
		{
			eslClient.AttachmentRequirementService.RejectAttachment(packageId, signer1, name1, rejectionComment);
			retrievedPackage = eslClient.GetPackage(packageId);
		}

		// Sender accepts Signer1's uploaded attachment
		public void AcceptAttachment()
		{
			eslClient.AttachmentRequirementService.AcceptAttachment(packageId, signer1, name1);
			retrievedPackage = eslClient.GetPackage(packageId);
		}

		// Sender downloads Signer1's attachment
		public byte[] DownloadAttachment()
		{
			return eslClient.AttachmentRequirementService.DownloadAttachment(packageId, attachment1Id);
		}
    }
}

