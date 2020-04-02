using System;
using System.IO;
using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Builder;
using System.Collections.Generic;

namespace SDK.Examples
{
    public class CustomFieldExample: SDKSample
    {
        public readonly string DOCUMENT_NAME = "First Document";
        public readonly string DEFAULT_VALUE = "#12345";
        public readonly string ENGLISH_LANGUAGE = "en";
        public readonly string ENGLISH_NAME = "Player Number";
        public readonly string ENGLISH_DESCRIPTION = "The number on your team jersey";
        public readonly string FRENCH_LANGUAGE = "fr";
        public readonly string FRENCH_NAME = "Numero du Joueur";
        public readonly string FRENCH_DESCRIPTION = "Le numero dans le dos de votre chandail d'equipe";
        public readonly string FIELD_VALUE1 = "11";
        public readonly string FIELD_VALUE2 = "22";

        public string customFieldId1, customFieldId2;
        public CustomField retrievedCustomField;
        public IList<CustomField> retrievedCustomFieldList1, retrievedCustomFieldList2;
        public IList<CustomFieldValue> retrieveCustomFieldValueList1, retrieveCustomFieldValueList2;
        public CustomFieldValue retrieveCustomFieldValue1, retrieveCustomFieldValue2;

        public static void Main(string[] args)
        {
            new CustomFieldExample().Run();
        }

        override public void Execute()
        {
            // first custom field
            customFieldId1 = Guid.NewGuid().ToString().Replace("-", "");
            Console.WriteLine("customer field ID = " + customFieldId1);
            CustomField customField1 = ossClient.GetCustomFieldService()
                .CreateCustomField(CustomFieldBuilder.CustomFieldWithId(customFieldId1)
                    .WithDefaultValue(DEFAULT_VALUE)
                    .WithTranslation(TranslationBuilder.NewTranslation(ENGLISH_LANGUAGE)
                        .WithName(ENGLISH_NAME)
                        .WithDescription(ENGLISH_DESCRIPTION))
                    .WithTranslation(TranslationBuilder.NewTranslation(FRENCH_LANGUAGE)
                        .WithName(FRENCH_NAME)
                        .WithDescription(FRENCH_DESCRIPTION))
                        .Build());

            CustomFieldValue customFieldValue = ossClient.GetCustomFieldService()
                .SubmitCustomFieldValue(CustomFieldValueBuilder.CustomFieldValueWithId(customField1.Id)
                        .WithValue(FIELD_VALUE1)
                        .build());

            // Second custom field
            customFieldId2 = Guid.NewGuid().ToString().Replace("-", "");
            Console.WriteLine("customer field ID = " + customFieldId1);
            CustomField customField2 = ossClient.GetCustomFieldService()
				.CreateCustomField(CustomFieldBuilder.CustomFieldWithId(customFieldId2)
					.WithDefaultValue("Red")
					.WithTranslation(TranslationBuilder.NewTranslation("en").
						WithName("Jersey color").
						WithDescription("The color of your team jersey"))
					.Build());

            CustomFieldValue customFieldValue2 = ossClient.GetCustomFieldService()
                .SubmitCustomFieldValue(CustomFieldValueBuilder.CustomFieldValueWithId(customField2.Id)
                                        .WithValue(FIELD_VALUE2)
                                        .build());

            DocumentPackage superDuperPackage = PackageBuilder.NewPackageNamed(PackageName)
                .WithSigner(SignerBuilder.NewSignerWithEmail(email1)
                        .WithFirstName("John")
                        .WithLastName("Smith"))
                    .WithDocument(DocumentBuilder.NewDocumentNamed(DOCUMENT_NAME)
                        .FromStream(fileStream1, DocumentType.PDF)
                        .WithSignature(SignatureBuilder.SignatureFor(email1)
                                .OnPage(0)
                                .AtPosition(100, 100)
							.WithField(FieldBuilder.CustomField(customFieldValue.Id)
                                        .OnPage(0)
                                        .AtPosition(400, 200))
                            .WithField(FieldBuilder.CustomField(customFieldValue2.Id)
							.OnPage(0)
							.AtPosition(400, 400))))
                .Build();

            packageId = ossClient.CreatePackage(superDuperPackage);
            ossClient.SendPackage(packageId);
            retrievedPackage = ossClient.GetPackage(packageId);

            // Get the entire list of custom field from account
            retrievedCustomFieldList1 = ossClient.GetCustomFieldService().GetCustomFields(Direction.ASCENDING);

            // Get a list of custom fields on page 1 sorted in ascending order by its id
            retrievedCustomFieldList2 = ossClient.GetCustomFieldService().GetCustomFields(Direction.ASCENDING, new PageRequest(1));

            // Get the first custom field from account
            retrievedCustomField = ossClient.GetCustomFieldService().GetCustomField(customFieldId1);

            // Delete the second custom field from account
            ossClient.GetCustomFieldService().DeleteCustomField(customFieldId2);

            // Get the entire list of user custom field from the user
            retrieveCustomFieldValueList1 = ossClient.GetCustomFieldService().GetCustomFieldValues();
            retrieveCustomFieldValue1 = ossClient.GetCustomFieldService().GetCustomFieldValue(customFieldId1);
            retrieveCustomFieldValue2 = ossClient.GetCustomFieldService().GetCustomFieldValue(customFieldId2);

            // Delete the second custom field from the user
            ossClient.GetCustomFieldService().DeleteCustomFieldValue(retrieveCustomFieldValueList1[1].Id);

            // Get the entire list of user custom field from the user
            retrieveCustomFieldValueList2 = ossClient.GetCustomFieldService().GetCustomFieldValues();

        }
    }
}

