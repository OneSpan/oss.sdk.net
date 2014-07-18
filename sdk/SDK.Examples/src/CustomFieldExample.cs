using System;
using System.IO;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;
using System.Collections.Generic;

namespace SDK.Examples
{
    public class CustomFieldExample: SDKSample
    {
        public readonly string DEFAULT_VALUE = "#12345";
        public readonly string ENGLISH_LANGUAGE = "en";
        public readonly string ENGLISH_NAME = "Player Number";
        public readonly string ENGLISH_DESCRIPTION = "The number on your team jersey";
        public readonly string FRENCH_LANGUAGE = "fr";
        public readonly string FRENCH_NAME = "Numero du Joueur";
        public readonly string FRENCH_DESCRIPTION = "Le numero dans le dos de votre chandail d'equipe";
        public readonly string FIELD_VALUE = "99";

        public string email1;
        private Stream documentInputStream1;
        public string customFieldId1, customFieldId2;
        public CustomField retrievedCustomField;
        public IList<CustomField> retrievedCustomFieldList1, retrievedCustomFieldList2;

        public static void Main(string[] args)
        {
            new CustomFieldExample(Props.GetInstance()).Run();
        }

        public CustomFieldExample(Props props) : this(props.Get("api.url"), props.Get("api.key"), props.Get("1.email"))
        {
        }

        public CustomFieldExample(string apiKey, string apiUrl, string email1) : base(apiKey, apiUrl)
        {
            this.email1 = email1;
            this.documentInputStream1 = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/src/document-with-fields.pdf").FullName);
        }

        override public void Execute()
        {
            // first custom field
            customFieldId1 = Guid.NewGuid().ToString().Replace("-", "");
            Console.WriteLine("customer field ID = " + customFieldId1);
            CustomField customField1 = eslClient.GetCustomFieldService()
                .CreateCustomField(CustomFieldBuilder.CustomFieldWithId(customFieldId1)
                    .WithDefaultValue(DEFAULT_VALUE)
                    .WithTranslation(TranslationBuilder.NewTranslation(ENGLISH_LANGUAGE)
                        .WithName(ENGLISH_NAME)
                        .WithDescription(ENGLISH_DESCRIPTION))
                    .WithTranslation(TranslationBuilder.NewTranslation(FRENCH_LANGUAGE)
                        .WithName(FRENCH_NAME)
                        .WithDescription(FRENCH_DESCRIPTION))
                        .Build());

            CustomFieldValue customFieldValue = eslClient.GetCustomFieldService()
                .SubmitCustomFieldValue(CustomFieldValueBuilder.CustomFieldValueWithId(customField1.Id)
                        .WithValue(FIELD_VALUE)
                        .build());

            // Second custom field
            customFieldId2 = Guid.NewGuid().ToString().Replace("-", "");
            Console.WriteLine("customer field ID = " + customFieldId1);
            CustomField customField2 = eslClient.GetCustomFieldService()
				.CreateCustomField(CustomFieldBuilder.CustomFieldWithId(customFieldId2)
					.WithDefaultValue("Red")
					.WithTranslation(TranslationBuilder.NewTranslation("en").
						WithName("Jersey color").
						WithDescription("The color of your team jersey"))
					.Build());

            DocumentPackage superDuperPackage = PackageBuilder.NewPackageNamed("Sample Insurance policy")
                .WithSigner(SignerBuilder.NewSignerWithEmail(email1)
                        .WithFirstName("John")
                        .WithLastName("Smith"))
                .WithDocument(DocumentBuilder.NewDocumentNamed("First Document")
                        .FromStream(documentInputStream1, DocumentType.PDF)
                        .WithSignature(SignatureBuilder.SignatureFor(email1)
                                .OnPage(0)
                                .AtPosition(100, 100)
							.WithField(FieldBuilder.CustomField(customFieldValue.Id)
                                        .OnPage(0)
                                        .AtPosition(400, 200))
						.WithField(FieldBuilder.CustomField(customField2.Id)
							.OnPage(0)
							.AtPosition(400, 400))))
                .Build();

            packageId = eslClient.CreatePackage(superDuperPackage);
            eslClient.SendPackage(packageId);

            // Get the entire list of custom field from account
            retrievedCustomFieldList1 = eslClient.GetCustomFieldService().GetCustomFields(Direction.ASCENDING);

            // Get a list of custom fields from index [1, 2] sorted by its id (first two custom fields)
            retrievedCustomFieldList2 = eslClient.GetCustomFieldService().GetCustomFields(Direction.DESCENDING, 1, 2);

            // Get the first custom field from account
            retrievedCustomField = eslClient.GetCustomFieldService().GetCustomField(customFieldId1);

            // Delete the second custom field from account
            eslClient.GetCustomFieldService().DeleteCustomField(customFieldId2);
        }
    }
}

