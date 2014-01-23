using System;
using System.IO;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;

namespace SDK.Examples
{
    public class CustomFieldExample: SDKSample
    {
		private PackageId packageId;
		public PackageId PackageId
		{
			get
			{
				return packageId;
			}
		}

        private string email1;
        private Stream documentInputStream1;

        public static void Main(string[] args)
        {
            new CustomFieldExample(Props.GetInstance()).Run();
        }
    
        public CustomFieldExample(Props props) : this(props.Get("api.url"), props.Get("api.key"), props.Get("1.email"))
        {
        }

        public CustomFieldExample(string apiKey, string apiUrl, string email1): base( apiKey, apiUrl )
        {
            this.email1 = email1;
            this.documentInputStream1 = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/src/document-with-fields.pdf").FullName);
        }

        override public void Execute()
        {

            string customFieldId = Guid.NewGuid().ToString().Replace("-", "");
            Console.WriteLine("customer field ID = " + customFieldId);
            CustomField customField = eslClient.GetCustomFieldService()
                .CreateCustomField(CustomFieldBuilder.CustomFieldWithId(customFieldId)
                        .WithDefaultValue("#12345")
                        .WithTranslation(TranslationBuilder.NewTranslation("en").
						WithName("Player Number").
						WithDescription("The number on your team jersey") )
                        .WithTranslation(TranslationBuilder.NewTranslation("fr").
						WithName("Numéro du Joueur").
						WithDescription("Le numéro dans le dos de votre chandail d'équipe") )
                        .Build() );

            CustomFieldValue customFieldValue = eslClient.GetCustomFieldService()
                .SubmitCustomFieldValue(CustomFieldValueBuilder.CustomFieldValueWithId(customField.Id)
					.WithValue("99")
                        .build() );

            DocumentPackage superDuperPackage = PackageBuilder.NewPackageNamed("Sample Insurance policy")
                .WithSigner(SignerBuilder.NewSignerWithEmail(email1)
                        .WithFirstName("John")
                        .WithLastName("Smith") )
                .WithDocument(DocumentBuilder.NewDocumentNamed("First Document")
                        .FromStream(documentInputStream1, DocumentType.PDF)
                        .WithSignature(SignatureBuilder.SignatureFor(email1)
                                .OnPage(0)
                                .AtPosition(100, 100)
							.WithField(FieldBuilder.CustomField(customFieldValue.Id)
                                        .OnPage(0)
                                        .AtPosition(400, 200) ) ) )
                .Build();
            PackageId packageId = eslClient.CreatePackage(superDuperPackage);
            eslClient.SendPackage(packageId);
        }
    }
}

