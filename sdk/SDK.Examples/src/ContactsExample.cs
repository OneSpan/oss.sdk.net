using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;

namespace SDK.Examples
{
    public class ContactsExample : SDKSample
    {
        public static void Main(string[] args)
        {
            new ContactsExample(Props.GetInstance()).Run();
        }

        public string email1;
        public string email2;
        private Stream fileStream1;
        public Sender signerForPackage;
        public IDictionary<string, Sender> beforeContacts;
        public IDictionary<string, Sender> afterContacts;

        public ContactsExample(Props props) : this(props.Get("api.url"), props.Get("api.key"), props.Get("1.email"))
        {
        }

        public ContactsExample(string apiKey, string apiUrl, string email1) : base(apiKey, apiUrl)
        {
            this.email1 = email1;
            this.email2 = Guid.NewGuid().ToString().Replace("-", "") + "@e-signlive.com";
            this.fileStream1 = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/src/document.pdf").FullName);
        }

        override public void Execute()
        {
            // Get the account contacts (Senders)
            beforeContacts = eslClient.AccountService.GetContacts();
            signerForPackage = beforeContacts[email1];

            // Create package with signer using information from the account contacts
            DocumentPackage superDuperPackage = PackageBuilder.NewPackageNamed("ContactsExample: " + DateTime.Now)
                    .DescribedAs("This is a package created using the e-SignLive SDK")
                    .ExpiresOn(DateTime.Now.AddMonths(100))
                    .WithEmailMessage("This message should be delivered to all signers")
                    .WithSigner(SignerBuilder.NewSignerWithEmail(email1)
                        .WithFirstName(signerForPackage.FirstName)
                        .WithLastName(signerForPackage.LastName)
                        .WithTitle(signerForPackage.Title)
                        .WithCompany(signerForPackage.Company))
                    .WithSigner(SignerBuilder.NewSignerWithEmail(email2)
                        .WithFirstName("John")
                        .WithLastName("Smith"))
                    .WithDocument(DocumentBuilder.NewDocumentNamed("First Document")
                        .FromStream(fileStream1, DocumentType.PDF)
                        .WithSignature(SignatureBuilder.SignatureFor(email1)
                            .OnPage(0)
                            .AtPosition(100, 100)))
                    .Build();

            packageId = eslClient.CreatePackageOneStep(superDuperPackage);
            eslClient.SendPackage(packageId);

            afterContacts = eslClient.AccountService.GetContacts();
        }
    }
}

