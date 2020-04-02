using System;
using System.Collections.Generic;

namespace OneSpanSign.Sdk
{
	internal class AttachmentRequirementConverter
    {
		private OneSpanSign.Sdk.AttachmentRequirement sdkAttachmentRequirement = null;
		private OneSpanSign.API.AttachmentRequirement apiAttachmentRequirement = null;

		/// <summary>
		/// Construct with API AttachmentRequirement object involved in conversion.
		/// </summary>
		/// <param name="apiAttachmentRequirement">API attachment requirement.</param>
		public AttachmentRequirementConverter(OneSpanSign.API.AttachmentRequirement apiAttachmentRequirement)
        {
			this.apiAttachmentRequirement = apiAttachmentRequirement;
        }

		/// <summary>
		/// Construct with SDK AttachmentRequirement object involved in conversion.
		/// </summary>
		/// <param name="sdkAttachmentRequirement">SDK attachment requirement.</param>
		public AttachmentRequirementConverter(OneSpanSign.Sdk.AttachmentRequirement sdkAttachmentRequirement)
		{
			this.sdkAttachmentRequirement = sdkAttachmentRequirement;
		}

		/// <summary>
		/// Convert from SDK AttachmentRequirement to API AttachmentRequirement.
		/// </summary>
		/// <returns>The API attachment requirement.</returns>
		public OneSpanSign.API.AttachmentRequirement ToAPIAttachmentRequirement()
		{
			if (sdkAttachmentRequirement == null)
			{
				return apiAttachmentRequirement;
			}

			OneSpanSign.API.AttachmentRequirement result = new OneSpanSign.API.AttachmentRequirement();

			if (!String.IsNullOrEmpty(sdkAttachmentRequirement.Id))
			{
                result.Id = sdkAttachmentRequirement.Id;
			}
            result.Name = sdkAttachmentRequirement.Name;
			result.Comment = sdkAttachmentRequirement.SenderComment;
			result.Description = sdkAttachmentRequirement.Description;
			result.Required = sdkAttachmentRequirement.Required;
			result.Data = sdkAttachmentRequirement.Data;
            result.Files = GetApiAttachmentFiles ();

            if (sdkAttachmentRequirement.Status.Equals(null))
			{
                result.Status = OneSpanSign.Sdk.RequirementStatus.INCOMPLETE.getApiValue();
			}
			else
			{
				result.Status = new RequirementStatusConverter(sdkAttachmentRequirement.Status).ToAPIRequirementStatus();
			}

			return result;
		}

		/// <summary>
		/// Convert from API AttachmentRequirement to SDK AttachmentRequirement.
		/// </summary>
		/// <returns>The SDK attachment requirement.</returns>
		public OneSpanSign.Sdk.AttachmentRequirement ToSDKAttachmentRequirement()
		{
			if (apiAttachmentRequirement == null)
			{
				return sdkAttachmentRequirement;
			}

			if (apiAttachmentRequirement.Name != null)
			{
				OneSpanSign.Sdk.AttachmentRequirement result = new OneSpanSign.Sdk.AttachmentRequirement(apiAttachmentRequirement.Name);
				result.SenderComment = apiAttachmentRequirement.Comment;
				result.Description = apiAttachmentRequirement.Description;
				result.Id = apiAttachmentRequirement.Id;
				result.Required = apiAttachmentRequirement.Required.Value;
				result.Data = apiAttachmentRequirement.Data;
				result.Status = new RequirementStatusConverter(apiAttachmentRequirement.Status).ToSDKRequirementStatus();
                result.Files = GetSDKAttachmentFiles ();

                return result;
			}

			return sdkAttachmentRequirement;
		}

        public IList<OneSpanSign.API.AttachmentFile> GetApiAttachmentFiles()
        {
            IList <OneSpanSign.API.AttachmentFile> files = new List<OneSpanSign.API.AttachmentFile> ();
            foreach (var file in sdkAttachmentRequirement.Files) 
            {
                files.Add (new AttachmentFileConverter (file).ToApiAttachmentFile ());
            }
            return files;
        }

        public IList<OneSpanSign.Sdk.AttachmentFile> GetSDKAttachmentFiles()
        {
            IList<OneSpanSign.Sdk.AttachmentFile> files = new List<OneSpanSign.Sdk.AttachmentFile> ();
            foreach (var file in apiAttachmentRequirement.Files) 
            {
                files.Add (new AttachmentFileConverter (file).ToSDKAttachmentFile ());
            }
            return files;
        }


    }
}

