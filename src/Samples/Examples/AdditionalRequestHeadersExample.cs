﻿using System;
using System.Collections.Generic;
using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Builder;

namespace SDK.Examples
{
    public class AdditionalRequestHeadersExample : SDKSample
    {
        public readonly string DOCUMENT_NAME = "First Document";

        public static void Main(string[] args)
        {
            new AdditionalRequestHeadersExample().Run();
        }

        override public void Execute()
        {
            IDictionary<string, string> additionalRequestHeaders = new Dictionary<string, string>();
            additionalRequestHeaders.Add("customHeader1", "value1");
            additionalRequestHeaders.Add("customHeader2", "value2");
            additionalRequestHeaders.Add("customHeader3", "value3");
            additionalRequestHeaders.Add("customHeader4", "value4");

            DocumentPackage superDuperPackage = PackageBuilder.NewPackageNamed(PackageName)
                .WithSettings(DocumentPackageSettingsBuilder.NewDocumentPackageSettings().WithInPerson())
                    .WithSigner(SignerBuilder.NewSignerWithEmail(email1)
                                .WithFirstName("John1")
                                .WithLastName("Smith1"))
                    .WithDocument(DocumentBuilder.NewDocumentNamed(DOCUMENT_NAME)
                                  .FromStream(fileStream1, DocumentType.PDF)
                                  .WithSignature(SignatureBuilder.SignatureFor(email1)
                                   .OnPage(0)
                                   .AtPosition(100, 100)))
                    .Build();

            packageId = ossClient.CreatePackage(superDuperPackage);
            ossClient.SendPackage(packageId);
            retrievedPackage = ossClient.GetPackage(packageId);
        }
    }
}
