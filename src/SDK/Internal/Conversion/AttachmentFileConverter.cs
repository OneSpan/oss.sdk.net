using System;
namespace OneSpanSign.Sdk
{
    public class AttachmentFileConverter
    {

        private API.AttachmentFile apiAttachmentFile = null;
        private AttachmentFile sdkAttachmentFile = null;

        public AttachmentFileConverter (OneSpanSign.API.AttachmentFile apiAttachmentFile)
        {
            this.apiAttachmentFile = apiAttachmentFile;
        }

        public AttachmentFileConverter (AttachmentFile sdkAttachmentFile)
        {
            this.sdkAttachmentFile = sdkAttachmentFile;
        }

        public OneSpanSign.API.AttachmentFile ToApiAttachmentFile()
        {
            if (sdkAttachmentFile == null) 
            {
                return apiAttachmentFile;
            }

            API.AttachmentFile result = new API.AttachmentFile ();
            result.Id = sdkAttachmentFile.Id;
            result.SetInsertDate(sdkAttachmentFile.InsertDate.Ticks);
            result.Name = sdkAttachmentFile.Name;
            result.Preview = sdkAttachmentFile.Preview;

            return result;
        }

        public AttachmentFile ToSDKAttachmentFile ()
        {
            if (apiAttachmentFile == null) 
            {
                return sdkAttachmentFile;
            }

            AttachmentFile result = new AttachmentFile 
            {
                Id = apiAttachmentFile.Id,
                InsertDate = apiAttachmentFile.GetInsertDate(),
                Name = apiAttachmentFile.Name,
                Preview = apiAttachmentFile.Preview
            };

            return result;
        }
    }
}
