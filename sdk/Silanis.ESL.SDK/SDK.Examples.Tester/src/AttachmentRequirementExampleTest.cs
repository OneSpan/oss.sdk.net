using System;
using NUnit.Framework;
using Silanis.ESL.SDK;
using System.Collections.Generic;
using Silanis.ESL.API;
using Silanis.ESL.SDK.Internal;
using System.IO;
using Silanis.ESL.SDK.Builder;
using Silanis.ESL.SDK.Builder.Internal;
using System.Text;

namespace SDK.Examples
{
	[TestFixture]
	public class AttachmentRequirementExampleTest
    {
		private RestClient client;
		private UrlTemplate template;
		private AttachmentRequirementExample example;

		[Test]
		public void VerifyResult()
		{
			example = new AttachmentRequirementExample( Props.GetInstance() );
			example.Run();

			// Asserts the attachment requirements for each signer is set correctly.
			DocumentPackage retrievedPackage = example.RetrievedPackage;
			IDictionary<string, AttachmentRequirement> signer1Attachments = retrievedPackage.Signers[example.Email1].Attachments;
			IDictionary<string, AttachmentRequirement> signer2Attachments = retrievedPackage.Signers[example.Email2].Attachments;

			Assert.AreEqual(signer1Attachments.Count, 1);
			AttachmentRequirement signer1Att1 = signer1Attachments[example.name1];
			Assert.AreEqual(signer1Att1.Name, example.name1);
			Assert.AreEqual(signer1Att1.Description, example.description1);
			Assert.AreEqual(signer1Att1.Required, true);
			Assert.AreEqual(signer1Att1.Status.ToString(), Silanis.ESL.API.RequirementStatus.INCOMPLETE.ToString());

			Assert.AreEqual(signer2Attachments.Count, 2);
			AttachmentRequirement signer2Att1 = signer2Attachments[example.name2];
			AttachmentRequirement signer2Att2 = signer2Attachments[example.name3];
			Assert.AreEqual(signer2Att1.Name, example.name2);
			Assert.AreEqual(signer2Att1.Description, example.description2);
			Assert.AreEqual(signer2Att1.Required, false);
			Assert.AreEqual(signer2Att1.Status.ToString(), Silanis.ESL.API.RequirementStatus.INCOMPLETE.ToString());
			Assert.AreEqual(signer2Att2.Name, example.name3);
			Assert.AreEqual(signer2Att2.Description, example.description3);
			Assert.AreEqual(signer2Att2.Required, true);
			Assert.AreEqual(signer2Att2.Status.ToString(), Silanis.ESL.API.RequirementStatus.INCOMPLETE.ToString());

			// Upload attachment for signer1
			string signerAuthenticationToken = example.EslClient.AuthenticationTokenService.CreateSignerAuthenticationToken(example.PackageId, example.signer1Id);
			AuthenticationClient authenticationClient = new AuthenticationClient(Props.GetInstance().Get("webpage.url"));
			String sessionIdForSigner = authenticationClient.GetSessionIdForSignerAuthenticationToken(signerAuthenticationToken);

			client = new RestClient("");
			template = new UrlTemplate(Props.GetInstance().Get("api.url"));

			Stream fileStream1 = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/src/document.pdf").FullName);
			uploadAttachment(example.PackageId, signer1Att1.Id, "Test Attachment", fileStream1, sessionIdForSigner);

			// Reject signer1's attachment
			example.RejectAttachment();
			signer1Att1 = retrievedPackage.Signers[example.Email1].Attachments[example.name1];
			Assert.AreEqual(signer1Att1.Status.ToString(), Silanis.ESL.API.RequirementStatus.REJECTED.ToString());
			Assert.AreEqual(signer1Att1.SenderComment, example.rejectionComment);

			// Accept signer1's attachment
			example.AcceptAttachment();
			Assert.AreEqual(signer1Att1.Status.ToString(), Silanis.ESL.API.RequirementStatus.COMPLETE.ToString());
			Assert.AreEqual(signer1Att1.SenderComment, "");

			// Download signer1's attachment
			byte[] downloadedAttachment = example.DownloadAttachment();
			System.IO.File.WriteAllBytes("/dev/null", downloadedAttachment);
		}

		private void uploadAttachment(PackageId packageId, string attachmentId, string filename, Stream fileStream, string sessionId)
		{
			string path = template.UrlFor(UrlTemplate.ATTACHMENT_REQUIREMENT_PATH)
				.Replace("{packageId}", packageId.Id)
				.Replace("{attachmentId}", attachmentId)
				.Build();
			byte[] fileBytes = new StreamDocumentSource(fileStream).Content();
			string fileName = DocumentTypeUtility.NormalizeName (DocumentType.PDF, filename);
			string boundary = GenerateBoundary();

			byte[] bytes = new byte[fileName.Length * sizeof(char)];
			System.Buffer.BlockCopy(fileName.ToCharArray(), 0, bytes, 0, bytes.Length);

			byte[] content = CreateMultipartContent(fileName, fileBytes, bytes, boundary);

			try {
				client.PostMultipartFile(path, content, boundary, sessionId);
			} catch (Exception e) {
				throw new EslException ("Could not upload attachment for signer." + " Exception: " + e.Message, e);
			}
		}

		private string GenerateBoundary ()
		{
			var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
			var stringChars = new char[16];
			var random = new Random ();

			for (int i = 0; i < stringChars.Length; i++) {
				stringChars [i] = chars [random.Next (chars.Length)];
			}

			return new String (stringChars);
		}  

		private byte[] CreateMultipartContent (string fileName, byte[] fileBytes, byte[] payloadBytes, string boundary)
		{

			Encoding encoding = Encoding.UTF8;
			Stream formDataStream = new MemoryStream ();

			string header = string.Format ("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"; filename=\"{2}\"\r\n\r\n",
				boundary, "payload", "paylaod");
			formDataStream.Write (encoding.GetBytes (header), 0, encoding.GetByteCount (header));
			formDataStream.Write (payloadBytes, 0, payloadBytes.Length);

			formDataStream.Write (encoding.GetBytes ("\r\n"), 0, encoding.GetByteCount ("\r\n"));

			string data = string.Format ("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"; filename=\"{2}\"\r\nContent-Type: {3}\r\n\r\n",
				boundary, "file", fileName, MimeTypeUtil.GetMIMEType (fileName));
			formDataStream.Write (encoding.GetBytes (data), 0, encoding.GetByteCount (data));
			formDataStream.Write (fileBytes, 0, fileBytes.Length);

			string footer = "\r\n--" + boundary + "--\r\n";
			formDataStream.Write (encoding.GetBytes (footer), 0, encoding.GetByteCount (footer));

			//Dump the stream
			formDataStream.Position = 0;
			byte[] formData = new byte[formDataStream.Length];
			formDataStream.Read (formData, 0, formData.Length);
			formDataStream.Close ();

			return formData;
		}
    }
}

