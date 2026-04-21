using System;
using OneSpanSign.Sdk.Internal;
using System.Collections.Generic;

namespace OneSpanSign.Sdk
{
	/// <summary>
	/// The AttachmentRequirementBuilder class is a convenient class used to create and customize a AttachmentRequirement
	/// associated with a signer.
	/// </summary>
	public class AttachmentRequirementBuilder
    {
		private string desciption;
		private string name;
		private bool isRequired;
        private IList<OneSpanSign.Sdk.AttachmentFile> files = new List<OneSpanSign.Sdk.AttachmentFile>();
		private string attachmentType;
		private bool extractionEnabled;

        /// <summary>
        /// Initializes a new instance of the <see cref="OneSpanSign.Sdk.AttachmentRequirementBuilder"/> class.
        /// </summary>
        /// <param name="name">The attachment name.</param>
        public AttachmentRequirementBuilder(string name)
        {
			this.name = name;
        }

		/// <summary>
		/// Creates a AttachmentRequirementBuilder instance with name.
		/// </summary>
		public static AttachmentRequirementBuilder NewAttachmentRequirementWithName(string name)
		{
			return new AttachmentRequirementBuilder(name);
		}

		/// <summary>
		/// Sets the attachment's desciption.
		/// </summary>
		public AttachmentRequirementBuilder WithDescription(string desciption)
		{
			this.desciption = desciption;
			return this;
		}

		/// <summary>
		/// Sets the isRequired property to true.
		/// </summary>
		public AttachmentRequirementBuilder IsRequiredAttachment()
		{
			this.isRequired = true;
			return this;
		}

        public AttachmentRequirementBuilder WithFiles(IList<AttachmentFile> files)
        {
            this.files = files;
            return this;
        }

		public AttachmentRequirementBuilder WithAttachmentType(string attachmentType)
		{
			this.attachmentType = attachmentType;
			return this;
		}

		public AttachmentRequirementBuilder WithAttachmentType(AttachmentType attachmentType)
		{
			this.attachmentType = attachmentType.ToString();
			return this;
		}

		public AttachmentRequirementBuilder WithExtractionEnabled(bool extractionEnabled)
		{
			this.extractionEnabled = extractionEnabled;
			return this;
		}

        public AttachmentRequirement Build()
		{
			Asserts.NotEmptyOrNull(name, "name");
			AttachmentRequirement attachmentRequirement = new AttachmentRequirement(name);
			attachmentRequirement.Description = desciption;
			attachmentRequirement.Required = isRequired;
			attachmentRequirement.Status = OneSpanSign.Sdk.RequirementStatus.INCOMPLETE;
            attachmentRequirement.Files = files;
			attachmentRequirement.AttachmentType = attachmentType;
			attachmentRequirement.ExtractionEnabled = extractionEnabled;

            return attachmentRequirement;
		}
    }
}

