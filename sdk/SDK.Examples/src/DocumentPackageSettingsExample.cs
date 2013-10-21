using System;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;
using System.IO;

namespace SDK.Examples
{
    public class DocumentPackageSettingsExample : SDKSample
    {
        public static void Main (string[] args)
        {
            new DocumentPackageSettingsExample(Props.GetInstance()).Run();
        }

        private string email1;
        private Stream fileStream1;

        public DocumentPackageSettingsExample( Props props ) : this(props.Get("api.url"), props.Get("api.key"), props.Get("1.email")) {
        }

        public DocumentPackageSettingsExample( String apiKey, String apiUrl, String email1 ) : base( apiKey, apiUrl ) {
            this.email1 = email1;
            this.fileStream1 = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/src/document.pdf").FullName);
        }

        override public void Execute() {
            DocumentPackage superDuperPackage = PackageBuilder.NewPackageNamed("DocumentPackageSettingsExample " + DateTime.Now)
				.WithSettings(DocumentPackageSettingsBuilder.NewDocumentPackageSettings()
				              .WithInPerson()
				              .WithoutDecline()
							  .WithOptOut()
				              .WithOptOutReason("Reason One")
				              .WithOptOutReason("Reason Two")
				              .WithOptOutReason("Reason Three")
				              .WithHandOverLinkHref("http://www.google.ca")
				              .WithHandOverLinkText("click here")
				              .WithHandOverLinkTooltip("link tooltip")
				              .WithCeremonyLayoutSettings(CeremonyLayoutSettingsBuilder.NewCeremonyLayoutSettings()
                                            .WithoutGlobalConfirmButton()
                                            .WithoutGlobalDownloadButton()
                                            .WithoutGlobalSaveAsLayoutButton() ) )
                    .WithSigner(SignerBuilder.NewSignerWithEmail(email1)
					            .WithFirstName("John")
					            .WithLastName("Smith"))
					.WithDocument(DocumentBuilder.NewDocumentNamed("First Document")
                                  .FromStream(fileStream1, DocumentType.PDF)
                                   .WithSignature(SignatureBuilder.SignatureFor(email1)
					               .OnPage(0)
					               .AtPosition(100, 100)))
					.Build();

            eslClient.CreateAndSendPackage(superDuperPackage);
        }
    }
}

