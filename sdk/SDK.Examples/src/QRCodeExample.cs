using System;
using System.IO;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;
using System.Collections.Generic;

namespace SDK.Examples
{
    public class QRCodeExample : SDKSample
    {
        public static void Main(string[] args)
        {
            new QRCodeExample(Props.GetInstance()).Run();
        }

        private Stream fileStream1;
        public readonly string DOCUMENT_NAME = "First Document";
        public readonly string DOCUMENT_ID = "documentId";
        public string email1;
        public Field addedQRCode1, addedQRCode2;
        public string qrCodeId1 = "QRCode_Id";
        public string qrCodeId2;
        public IList<Silanis.ESL.SDK.Field> modifiedQRCodeList, deletedQRCodeList, updatedQRCodeList;

        public QRCodeExample(Props props) : this(props.Get("api.key"), props.Get("api.url"), props.Get("1.email"))
        {
        }

        public QRCodeExample(string apiKey, string apiUrl, string email1) : base(apiKey, apiUrl)
        {
            this.email1 = email1;
            this.fileStream1 = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/src/document.pdf").FullName);
        }

        override public void Execute()
        {
            DocumentPackage superDuperPackage = PackageBuilder.NewPackageNamed("QRCodeExample: " + DateTime.Now)
                    .DescribedAs("This is a package created using the e-SignLive SDK")
                    .WithEmailMessage("This message should be delivered to all signers")
                    .WithSigner(SignerBuilder.NewSignerWithEmail(email1)
                        .WithCustomId("Client1")
                        .WithFirstName("John")
                        .WithLastName("Smith")
                        .WithTitle("Managing Director")
                        .WithCompany("Acme Inc."))
                    .WithDocument(DocumentBuilder.NewDocumentNamed("First Document")
                        .WithId(DOCUMENT_ID)
                        .FromStream(fileStream1, DocumentType.PDF)
                        .WithSignature(SignatureBuilder.SignatureFor(email1)
                            .OnPage(0)
                            .AtPosition(100, 100))
                    .WithQRCode(FieldBuilder.QRCode()
                        .WithId(qrCodeId1)
                        .OnPage(0)
                        .AtPosition(400, 100)))
                    .Build();

            packageId = eslClient.CreatePackage(superDuperPackage);

            Field qrCode2 = FieldBuilder.QRCode()
                .OnPage(0)
                .AtPosition(500, 100)
                .Build();

            // Add a second QR code to document
            qrCodeId2 = eslClient.QrCodeService.AddQRCode(packageId, DOCUMENT_ID, qrCode2);

            // Get the added QR codes
            addedQRCode1 = eslClient.QrCodeService.GetQRCode(packageId, DOCUMENT_ID, qrCodeId1);
            addedQRCode2 = eslClient.QrCodeService.GetQRCode(packageId, DOCUMENT_ID, qrCodeId2);

            // Modify the first QR code
            Field modifiedQRCode = FieldBuilder.QRCode()
                .WithId(qrCodeId1)
                .OnPage(0)
                .AtPosition(400, 500)
                .Build();

            eslClient.QrCodeService.ModifyQRCode(packageId, DOCUMENT_ID, modifiedQRCode);
            modifiedQRCodeList = eslClient.GetPackage(packageId).GetDocument(DOCUMENT_NAME).QRCodes;

            // Delete the second QR code
            eslClient.QrCodeService.DeleteQRCode(packageId, DOCUMENT_ID, qrCodeId2);
            deletedQRCodeList = eslClient.GetPackage(packageId).GetDocument(DOCUMENT_NAME).QRCodes;

            // Update all the QR codes in the document with the provided list of fields
            Field updatedQRCode1 = FieldBuilder.QRCode()
                .WithId(qrCodeId1)
                .OnPage(0)
                .AtPosition(200, 600)
                .Build();

            Field updatedQRCode2 = FieldBuilder.QRCode()
                .WithId(qrCodeId2)
                .OnPage(0)
                .AtPosition(300, 600)
                .Build();

            IList<Silanis.ESL.SDK.Field> qrCodeList = new List<Silanis.ESL.SDK.Field>();
            qrCodeList.Add(updatedQRCode1);
            qrCodeList.Add(updatedQRCode2);
            eslClient.QrCodeService.UpdateQRCodes(packageId, DOCUMENT_ID, qrCodeList);
            updatedQRCodeList = eslClient.GetPackage(packageId).GetDocument(DOCUMENT_NAME).QRCodes;
        }
    }
}

