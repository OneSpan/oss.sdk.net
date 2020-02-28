using System;
namespace Silanis.ESL.SDK
{
    public class AttachmentFileConverter
    {

        private ESL.API.AttachmentFile apiAttachmentFile = null;
        private ESL.SDK.AttachmentFile sdkAttachmentFile = null;

        public AttachmentFileConverter (Silanis.ESL.API.AttachmentFile apiAttachmentFile)
        {
            this.apiAttachmentFile = apiAttachmentFile;
        }

        public AttachmentFileConverter (ESL.SDK.AttachmentFile sdkAttachmentFile)
        {
            this.sdkAttachmentFile = sdkAttachmentFile;
        }

        public Silanis.ESL.API.AttachmentFile ToApiAttachmentFile()
        {
            if (sdkAttachmentFile == null) 
            {
                return apiAttachmentFile;
            }

            ESL.API.AttachmentFile result = new API.AttachmentFile ();
            result.Id = sdkAttachmentFile.Id;
            result.SetInsertDate(sdkAttachmentFile.InsertDate.Ticks);
            result.Name = sdkAttachmentFile.Name;
            result.Preview = sdkAttachmentFile.Preview;

            return result;
        }

        public ESL.SDK.AttachmentFile ToSDKAttachmentFile ()
        {
            if (apiAttachmentFile == null) 
            {
                return sdkAttachmentFile;
            }

            ESL.SDK.AttachmentFile result = new SDK.AttachmentFile 
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
