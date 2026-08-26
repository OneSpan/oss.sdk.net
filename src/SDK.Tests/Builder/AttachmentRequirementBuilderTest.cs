using System;
using NUnit.Framework;
using OneSpanSign.Sdk;

namespace SDK.Tests
{
	[TestFixture]
    public class AttachmentRequirementBuilderTest
    {
		[Test]
		public void BuildWithSpecificValues() {
			string name = "Driver's license";
			string description = "Please upload driver's license";
			bool isRequired = true;

			AttachmentRequirement attachmentRequirement = AttachmentRequirementBuilder.NewAttachmentRequirementWithName(name)
				.WithDescription(description)
				.IsRequiredAttachment()
				.Build();

			Assert.AreEqual(name, attachmentRequirement.Name);
			Assert.AreEqual(description, attachmentRequirement.Description);
			Assert.AreEqual(isRequired, attachmentRequirement.Required);
		}

		[Test]
		public void BuildWithAttachmentTypeString()
		{
			string attachmentType = "DRIVERS_LICENSE";

			AttachmentRequirement attachmentRequirement = AttachmentRequirementBuilder.NewAttachmentRequirementWithName("Driver's license")
				.WithAttachmentType(attachmentType)
				.Build();

			Assert.AreEqual(attachmentType, attachmentRequirement.AttachmentType);
		}

		[Test]
		public void BuildWithAttachmentTypeEnum()
		{
			AttachmentRequirement attachmentRequirement = AttachmentRequirementBuilder.NewAttachmentRequirementWithName("Passport")
				.WithAttachmentType(AttachmentType.PASSPORT)
				.Build();

			Assert.AreEqual(AttachmentType.PASSPORT.ToString(), attachmentRequirement.AttachmentType);
		}

		[Test]
		public void BuildWithExtractionEnabled()
		{
			AttachmentRequirement attachmentRequirement = AttachmentRequirementBuilder.NewAttachmentRequirementWithName("Bank Statement")
				.WithExtractionEnabled(true)
				.Build();

			Assert.AreEqual(true, attachmentRequirement.ExtractionEnabled);
		}

		[Test]
		public void AttachmentNameCannotBeNull()
		{
            Assert.Throws<OssException>(()=> AttachmentRequirementBuilder.NewAttachmentRequirementWithName(null).Build());
		}

		[Test]
		public void AttachmentNameCannotBeEmptyString()
		{
            Assert.Throws<OssException>(() => AttachmentRequirementBuilder.NewAttachmentRequirementWithName("").Build());
		}
    }
}

