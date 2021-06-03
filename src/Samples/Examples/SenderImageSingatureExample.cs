using System;
using OneSpanSign.Sdk;
using System.IO;

namespace SDK.Examples
{

    public class SenderImageSignatureExample : SDKSample
    {

        public string InputFileContentEncoded;
        public SenderImageSignature ResultAfterUpdate;
        public SenderImageSignature ResultAfterDelete;
        public static readonly String FILE_NAME = "exampleFile.jpg";

        public static void Main(string[] args)
        {
            new SenderImageSignatureExample().Run();
        }

        public SenderImageSignatureExample()
        {
            this.email1 = GetRandomEmail();
        }

        override public void Execute()
        {
            AccountMember accountMember1 = AccountMemberBuilder.NewAccountMember(email1)
                .WithFirstName("firstName1")
                .WithLastName("lastName1")
                .WithCompany("company1")
                .WithTitle("title1")
                .WithLanguage("language1")
                .WithPhoneNumber("phoneNumber1")
                .WithTimezoneId("GMT")
                .Build();

            Sender createdSender1 = ossClient.AccountService.InviteUser(accountMember1);
            Stream fileInputStream = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/SampleDocuments/exampleFile.jpg").FullName);
            byte[] fileContent;
            using (var memoryStream = new MemoryStream())
            {
                fileInputStream.CopyTo(memoryStream);
                fileContent = memoryStream.ToArray();
            }
            InputFileContentEncoded = System.Convert.ToBase64String(fileContent);
            ossClient.AccountService.UpdateSenderImageSignature(FILE_NAME, fileContent, createdSender1.Id);
            ResultAfterUpdate = ossClient.AccountService.GetSenderImageSignature(createdSender1.Id);
            ossClient.AccountService.DeleteSenderImageSignature(createdSender1.Id);
            ResultAfterDelete = ossClient.AccountService.GetSenderImageSignature(createdSender1.Id);
        }
    }
}
