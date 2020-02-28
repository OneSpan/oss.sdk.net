using System;
using Silanis.ESL.SDK.Internal;
using System.Collections.Generic;

namespace Silanis.ESL.SDK
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
        private IList<Silanis.ESL.SDK.AttachmentFile> files = new List<Silanis.ESL.SDK.AttachmentFile>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Silanis.ESL.SDK.AttachmentRequirementBuilder"/> class.
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

        public AttachmentRequirement Build()
		{
			Asserts.NotEmptyOrNull(name, "name");
			AttachmentRequirement attachmentRequirement = new AttachmentRequirement(name);
			attachmentRequirement.Description = desciption;
			attachmentRequirement.Required = isRequired;
			attachmentRequirement.Status = Silanis.ESL.SDK.RequirementStatus.INCOMPLETE;
            attachmentRequirement.Files = files;

            return attachmentRequirement;
		}
    }
}

