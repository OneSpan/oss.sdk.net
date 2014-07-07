using System;
using Silanis.ESL.SDK.Internal;
using Newtonsoft.Json;
using Silanis.ESL.API;

namespace Silanis.ESL.SDK
{
	/// <summary>
	/// The AttachmentRequirementService class provides methods to help create attachments for signers.
	/// </summary>
    public class AttachmentRequirementService
    {
        private AttachmentRequirementApiClient apiClient;

		internal AttachmentRequirementService(AttachmentRequirementApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

		/// <summary>
		/// Sender accepts signer's attachment requirement.
		/// </summary>
		/// <param name="packageId">Package identifier.</param>
		/// <param name="signer">Signer.</param>
		/// <param name="attachmentId">Attachment identifier.</param>
		public void AcceptAttachment(PackageId packageId, Signer signer, String attachmentId)
        {
            signer.Attachments[attachmentId].SenderComment = "";
            signer.Attachments[attachmentId].Status = Silanis.ESL.SDK.RequirementStatus.COMPLETE;
            
            Role apiRole = new SignerConverter(signer).ToAPIRole(System.Guid.NewGuid().ToString());
            
            apiClient.AcceptAttachment(packageId.Id, apiRole);
        }

		/// <summary>
		/// Sender rejects signer's attachment requirement with a comment.
		/// </summary>
		/// <param name="packageId">Package identifier.</param>
		/// <param name="signer">Signer.</param>
		/// <param name="attachmentId">Attachment identifier.</param>
		/// <param name="senderComment">Sender comment.</param>
        public void RejectAttachment(PackageId packageId, Signer signer, String attachmentId, String senderComment)
        {
            signer.Attachments[attachmentId].SenderComment = senderComment;
            signer.Attachments[attachmentId].Status = RequirementStatus.REJECTED;
            Role apiRole = new SignerConverter(signer).ToAPIRole(System.Guid.NewGuid().ToString());
            
            apiClient.RejectAttachment(packageId.Id, apiRole);
        }

		/// <summary>
		/// Sender downloads the attachment.
		/// </summary>
		/// <returns>The attachment.</returns>
		/// <param name="packageId">Package identifier.</param>
		/// <param name="attachmentId">Attachment identifier.</param>
		public byte[] DownloadAttachment(PackageId packageId, String attachmentId)
		{
            return apiClient.DownloadAttachments(packageId.Id, attachmentId);
		}
    }
}

