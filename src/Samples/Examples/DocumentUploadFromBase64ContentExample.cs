using System;
using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Builder;
using System.Collections.Generic;

namespace SDK.Examples
{
    public class DocumentUploadFromBase64ContentExample : SDKSample
    {
        public static void Main(string[] args)
        {
            new DocumentUploadFromBase64ContentExample().Run();
        }

        public readonly string DOCUMENT1_NAME = "First Document";
        public readonly string DOCUMENT2_NAME = "Second Document";
        public readonly string DOCUMENT1_ID = "documentId1";
        public readonly string DOCUMENT2_ID = "documentId2";
        public readonly string BASE64_CONTENT = "JVBERi0xLjUKJcOkw7zDtsOfCjIgMCBvYmoKPDwvTGVuZ3RoIDMgMCBSL0ZpbHRlci9GbGF0ZURlY29kZT4+CnN0cmVhbQp4nDPQM1Qo5ypUMFAw0DMwslAwMzTUszQ3VDC3hNBFqVzhWgp5XIEKALasCKwKZW5kc3RyZWFtCmVuZG9iagoKMyAwIG9iago0NQplbmRvYmoKCjUgMCBvYmoKPDwKPj4KZW5kb2JqCgo2IDAgb2JqCjw8L0ZvbnQgNSAwIFIKL1Byb2NTZXRbL1BERi9UZXh0XQo+PgplbmRvYmoKCjEgMCBvYmoKPDwvVHlwZS9QYWdlL1BhcmVudCA0IDAgUi9SZXNvdXJjZXMgNiAwIFIvTWVkaWFCb3hbMCAwIDYxMiA3OTJdL0dyb3VwPDwvUy9UcmFuc3BhcmVuY3kvQ1MvRGV2aWNlUkdCL0kgdHJ1ZT4+L0NvbnRlbnRzIDIgMCBSPj4KZW5kb2JqCgo0IDAgb2JqCjw8L1R5cGUvUGFnZXMKL1Jlc291cmNlcyA2IDAgUgovTWVkaWFCb3hbIDAgMCA2MTIgNzkyIF0KL0tpZHNbIDEgMCBSIF0KL0NvdW50IDE+PgplbmRvYmoKCjcgMCBvYmoKPDwvVHlwZS9DYXRhbG9nL1BhZ2VzIDQgMCBSCi9PcGVuQWN0aW9uWzEgMCBSIC9YWVogbnVsbCBudWxsIDBdCi9MYW5nKGVuLUNBKQo+PgplbmRvYmoKCjggMCBvYmoKPDwvQ3JlYXRvcjxGRUZGMDA1NzAwNzIwMDY5MDA3NDAwNjUwMDcyPgovUHJvZHVjZXI8RkVGRjAwNEMwMDY5MDA2MjAwNzIwMDY1MDA0RjAwNjYwMDY2MDA2OTAwNjMwMDY1MDAyMDAwMzYwMDJFMDAzND4KL0NyZWF0aW9uRGF0ZShEOjIwMjEwODI2MTM0NjU3LTA0JzAwJyk+PgplbmRvYmoKCnhyZWYKMCA5CjAwMDAwMDAwMDAgNjU1MzUgZiAKMDAwMDAwMDIyOSAwMDAwMCBuIAowMDAwMDAwMDE5IDAwMDAwIG4gCjAwMDAwMDAxMzUgMDAwMDAgbiAKMDAwMDAwMDM3MSAwMDAwMCBuIAowMDAwMDAwMTU0IDAwMDAwIG4gCjAwMDAwMDAxNzYgMDAwMDAgbiAKMDAwMDAwMDQ2OSAwMDAwMCBuIAowMDAwMDAwNTY1IDAwMDAwIG4gCnRyYWlsZXIKPDwvU2l6ZSA5L1Jvb3QgNyAwIFIKL0luZm8gOCAwIFIKL0lEIFsgPDk1NDhDMUE4RTFCQ0RCRkQ0ODAxMzQyOEIzNEEyQ0E5Pgo8OTU0OEMxQThFMUJDREJGRDQ4MDEzNDI4QjM0QTJDQTk+IF0KL0RvY0NoZWNrc3VtIC85QUJEOEQ0NUEzMDgzMEMzMzQ5MUYzOTYwNkY0MkEyOQo+PgpzdGFydHhyZWYKNzM5CiUlRU9GCg==";

        public Document document1, document2;
        public IList<Document> uploadedDocuments;

        override public void Execute()
        {
            // 1. Create a package
            DocumentPackage superDuperPackage = PackageBuilder.NewPackageNamed(PackageName)
                .DescribedAs("This is a package created using OneSpan Sign SDK")
                    .ExpiresOn(DateTime.Now.AddMonths(1))
                    .WithEmailMessage("This message should be delivered to all signers")
                    .WithSigner(SignerBuilder.NewSignerWithEmail(email1)
                                .WithCustomId("Client1")
                                .WithFirstName("John")
                                .WithLastName("Smith")
                                .WithTitle("Managing Director")
                                .WithCompany("Acme Inc."))
                    .Build();
            packageId = ossClient.CreatePackage(superDuperPackage);
            superDuperPackage.Id = packageId;

            // 2. Construct documents
            document1 = DocumentBuilder.NewDocumentNamed(DOCUMENT1_NAME)
                                .WithId(DOCUMENT1_ID)
                                .FromBase64Content(BASE64_CONTENT)
                                .WithSignature(SignatureBuilder.SignatureFor(email1)
                                .OnPage(0)
                                .WithField(FieldBuilder.CheckBox()
                                .OnPage(0)
                                .AtPosition(400, 200)
                                .WithValue(FieldBuilder.CHECKBOX_CHECKED))
                                .AtPosition(100, 100))
                    .Build();

            document2 = DocumentBuilder.NewDocumentNamed(DOCUMENT2_NAME)
                                .WithId(DOCUMENT2_ID)
                                .FromBase64Content(BASE64_CONTENT)
                                .WithSignature(SignatureBuilder.SignatureFor(email1)
                                .OnPage(0)
                                .WithField(FieldBuilder.CheckBox()
                                .OnPage(0)
                                .AtPosition(400, 200)
                                .WithValue(FieldBuilder.CHECKBOX_CHECKED))
                                .AtPosition(100, 100))
                    .Build();

            // 3. Upload the documents to the created package by uploading the document.
            uploadedDocuments = ossClient.UploadDocumentsWithBase64Content(packageId, new List<Document>{document1, document2});

            ossClient.SendPackage(packageId);
            retrievedPackage = ossClient.GetPackage(packageId);
        }
    }
}