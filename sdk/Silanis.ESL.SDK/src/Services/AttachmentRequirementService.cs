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
		private UrlTemplate template;
		private JsonSerializerSettings settings;
		private RestClient restClient;

		public AttachmentRequirementService(RestClient restClient, string baseUrl, JsonSerializerSettings settings)
        {
			this.restClient = restClient;
			template = new UrlTemplate (baseUrl);
			this.settings = settings;
        }

		/// <summary>
		/// Sender accepts signer's attachment requirement.
		/// </summary>
		/// <param name="packageId">Package identifier.</param>
		/// <param name="signer">Signer.</param>
		/// <param name="attachmentId">Attachment identifier.</param>
		public void AcceptAttachment(PackageId packageId, Signer signer, String attachmentId)
		{
			string path = template.UrlFor(UrlTemplate.UPDATE_SIGNER_PATH)
				.Replace("{packageId}", packageId.Id)
				.Replace("{roleId}", signer.Id)
				.Build();

			signer.Attachments[attachmentId].SenderComment = "";
			signer.Attachments[attachmentId].Status = Silanis.ESL.SDK.RequirementStatus.COMPLETE;

			Role apiPayload = new SignerConverter(signer).ToAPIRole(System.Guid.NewGuid().ToString());

			try {
				string json = JsonConvert.SerializeObject(apiPayload, settings);
				restClient.Put(path, json);
			} 
            catch (EslServerException e) {
                throw new EslServerException("Could not accept attachment for signer." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e) {
				throw new EslException("Could not accept attachment for signer." + " Exception: " + e.Message,e);
			}
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
			string path = template.UrlFor(UrlTemplate.UPDATE_SIGNER_PATH)
				.Replace("{packageId}", packageId.Id)
				.Replace("{roleId}", signer.Id)
				.Build();

			signer.Attachments[attachmentId].SenderComment = senderComment;
			signer.Attachments[attachmentId].Status = RequirementStatus.REJECTED;

			Role apiPayload = new SignerConverter(signer).ToAPIRole(System.Guid.NewGuid().ToString());

			try {
				string json = JsonConvert.SerializeObject(apiPayload, settings);
				restClient.Put(path, json);              
			} 
            catch (EslServerException e) {
                throw new EslServerException("Could not reject attachment for signer." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e) {
				throw new EslException("Could not reject attachment for signer." + " Exception: " + e.Message,e);
			}
		}

		/// <summary>
		/// Sender downloads the attachment.
		/// </summary>
		/// <returns>The attachment.</returns>
		/// <param name="packageId">Package identifier.</param>
		/// <param name="attachmentId">Attachment identifier.</param>
		public byte[] DownloadAttachment(PackageId packageId, String attachmentId)
		{
			string path = template.UrlFor(UrlTemplate.ATTACHMENT_REQUIREMENT_PATH)
				.Replace("{packageId}", packageId.Id)
				.Replace("{attachmentId}", attachmentId)
				.Build();

			try {
				return restClient.GetBytes(path);
			}
            catch (EslServerException e) {
                throw new EslServerException("Could not download the pdf attachment." + " Exception: " + e.Message, e.ServerError, e);
            }
            catch (Exception e) {
				throw new EslException("Could not download the pdf attachment." + " Exception: " + e.Message,e);
			}
		}
    }
}

