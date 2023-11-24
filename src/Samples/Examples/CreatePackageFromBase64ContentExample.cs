using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Builder;

namespace SDK.Examples
{
    public class CreatePackageFromBase64ContentExample : SDKSample
    {
        public static void Main (string[] args)
        {
            new CreatePackageFromBase64ContentExample().Run();
        }

        public readonly string DOCUMENT_NAME = "My Document";
        public readonly string DOCUMENT_ID = "documentId";
        public readonly string BASE64_CONTENT = "JVBERi0xLjUKJcOkw7zDtsOfCjIgMCBvYmoKPDwvTGVuZ3RoIDMgMCBSL0ZpbHRlci9GbGF0ZURlY29kZT4+CnN0cmVhbQp4nDPQM1Qo5ypUMFAw0DMwslAwMzTUszQ3VDC3hNBFqVzhWgp5XIEKALasCKwKZW5kc3RyZWFtCmVuZG9iagoKMyAwIG9iago0NQplbmRvYmoKCjUgMCBvYmoKPDwKPj4KZW5kb2JqCgo2IDAgb2JqCjw8L0ZvbnQgNSAwIFIKL1Byb2NTZXRbL1BERi9UZXh0XQo+PgplbmRvYmoKCjEgMCBvYmoKPDwvVHlwZS9QYWdlL1BhcmVudCA0IDAgUi9SZXNvdXJjZXMgNiAwIFIvTWVkaWFCb3hbMCAwIDYxMiA3OTJdL0dyb3VwPDwvUy9UcmFuc3BhcmVuY3kvQ1MvRGV2aWNlUkdCL0kgdHJ1ZT4+L0NvbnRlbnRzIDIgMCBSPj4KZW5kb2JqCgo0IDAgb2JqCjw8L1R5cGUvUGFnZXMKL1Jlc291cmNlcyA2IDAgUgovTWVkaWFCb3hbIDAgMCA2MTIgNzkyIF0KL0tpZHNbIDEgMCBSIF0KL0NvdW50IDE+PgplbmRvYmoKCjcgMCBvYmoKPDwvVHlwZS9DYXRhbG9nL1BhZ2VzIDQgMCBSCi9PcGVuQWN0aW9uWzEgMCBSIC9YWVogbnVsbCBudWxsIDBdCi9MYW5nKGVuLUNBKQo+PgplbmRvYmoKCjggMCBvYmoKPDwvQ3JlYXRvcjxGRUZGMDA1NzAwNzIwMDY5MDA3NDAwNjUwMDcyPgovUHJvZHVjZXI8RkVGRjAwNEMwMDY5MDA2MjAwNzIwMDY1MDA0RjAwNjYwMDY2MDA2OTAwNjMwMDY1MDAyMDAwMzYwMDJFMDAzND4KL0NyZWF0aW9uRGF0ZShEOjIwMjEwODI2MTM0NjU3LTA0JzAwJyk+PgplbmRvYmoKCnhyZWYKMCA5CjAwMDAwMDAwMDAgNjU1MzUgZiAKMDAwMDAwMDIyOSAwMDAwMCBuIAowMDAwMDAwMDE5IDAwMDAwIG4gCjAwMDAwMDAxMzUgMDAwMDAgbiAKMDAwMDAwMDM3MSAwMDAwMCBuIAowMDAwMDAwMTU0IDAwMDAwIG4gCjAwMDAwMDAxNzYgMDAwMDAgbiAKMDAwMDAwMDQ2OSAwMDAwMCBuIAowMDAwMDAwNTY1IDAwMDAwIG4gCnRyYWlsZXIKPDwvU2l6ZSA5L1Jvb3QgNyAwIFIKL0luZm8gOCAwIFIKL0lEIFsgPDk1NDhDMUE4RTFCQ0RCRkQ0ODAxMzQyOEIzNEEyQ0E5Pgo8OTU0OEMxQThFMUJDREJGRDQ4MDEzNDI4QjM0QTJDQTk+IF0KL0RvY0NoZWNrc3VtIC85QUJEOEQ0NUEzMDgzMEMzMzQ5MUYzOTYwNkY0MkEyOQo+PgpzdGFydHhyZWYKNzM5CiUlRU9GCg==";

        override public void Execute()
        {
            DocumentPackage package = PackageBuilder.NewPackageNamed(PackageName)
                    .DescribedAs("This is a new package")
                    .WithSigner(SignerBuilder.NewSignerWithEmail(email1)
                                .WithFirstName("John")
                                .WithLastName("Smith"))
                    .WithDocument(DocumentBuilder.NewDocumentNamed(DOCUMENT_NAME)
                                .WithId(DOCUMENT_ID)
                                .FromBase64Content(BASE64_CONTENT)
                                .WithSignature(SignatureBuilder.SignatureFor(email1)
                                   .OnPage(0)
                                   .AtPosition(100, 100)))
                    .Build();

            packageId = ossClient.createPackageOneStepWithBase64Content(package);
            ossClient.SendPackage(packageId);
            
            retrievedPackage = ossClient.GetPackage(packageId);
        }
    }
}
