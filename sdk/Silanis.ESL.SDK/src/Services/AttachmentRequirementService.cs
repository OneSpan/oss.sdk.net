using System;
using Silanis.ESL.SDK.Internal;
using Newtonsoft.Json;
using Silanis.ESL.API;
using System.IO;
using Silanis.ESL.SDK.Services;

namespace Silanis.ESL.SDK
{
	/// <summary>
	/// The AttachmentRequirementService class provides methods to help create attachments for signers.
	/// </summary>
    public class AttachmentRequirementService
    {
        private AttachmentRequirementApiClient apiClient;
        private PackageService packageService;

        internal AttachmentRequirementService(RestClient restClient, string baseUrl, JsonSerializerSettings settings)
        {
            packageService = new PackageService(restClient, baseUrl, settings);
            apiClient = new AttachmentRequirementApiClient(restClient, baseUrl, settings);
        }

		/// <summary>
		/// Sender accepts signer's attachment requirement.
		/// </summary>
		/// <param name="packageId">Package identifier.</param>
		/// <param name="signer">Signer.</param>
		/// <param name="attachmentId">Attachment identifier.</param>
		public void AcceptAttachment(PackageId packageId, Signer signer, String attachmentName)
        {
            signer.GetAttachmentRequirement(attachmentName).SenderComment = "";
            signer.GetAttachmentRequirement(attachmentName).Status = Silanis.ESL.SDK.RequirementStatus.COMPLETE;
            
            packageService.UpdateSigner(packageId, signer);
        }

		/// <summary>
		/// Sender rejects signer's attachment requirement with a comment.
		/// </summary>
		/// <param name="packageId">Package identifier.</param>
		/// <param name="signer">Signer.</param>
		/// <param name="attachmentId">Attachment identifier.</param>
		/// <param name="senderComment">Sender comment.</param>
        public void RejectAttachment(PackageId packageId, Signer signer, String attachmentName, String senderComment)
        {
            signer.GetAttachmentRequirement(attachmentName).SenderComment = senderComment;
            signer.GetAttachmentRequirement(attachmentName).Status = RequirementStatus.REJECTED;
            
            packageService.UpdateSigner(packageId, signer);
        }

		/// <summary>
		/// Sender downloads the attachment.
		/// </summary>
		/// <returns>The attachment.</returns>
		/// <param name="packageId">Package identifier.</param>
		/// <param name="attachmentId">Attachment identifier.</param>
		public byte[] DownloadAttachment(PackageId packageId, String attachmentId)
		{
            return apiClient.DownloadAttachment(packageId.Id, attachmentId);
		}

        /// <summary>
        /// Sender downloads the attachment.
        /// </summary>
        /// <returns>The attachment.</returns>
        /// <param name="packageId">Package identifier.</param>
        /// <param name="attachmentId">Attachment identifier.</param>
        public byte[] DownloadAllAttachmentsForPackage(PackageId packageId)
        {
            return apiClient.DownloadAllAttachmentsForPackage(packageId.Id);
        }

        /// <summary>
        /// Sender downloads the attachment.
        /// </summary>
        /// <returns>The attachment.</returns>
        /// <param name="packageId">Package identifier.</param>
        /// <param name="attachmentId">Attachment identifier.</param>
        public byte[] DownloadAllAttachmentsForSignerInPackage(DocumentPackage sdkPackage, Signer signer)
        {
            return apiClient.DownloadAllAttachmentsForSignerInPackage(sdkPackage, signer);
        }

        public void UploadAttachment(PackageId packageId, string attachmentId, string fileName, byte[] fileBytes, string signerSessionId)
        {
            apiClient.UploadAttachment(packageId, attachmentId, fileName, fileBytes, signerSessionId);
        }
    }
}

