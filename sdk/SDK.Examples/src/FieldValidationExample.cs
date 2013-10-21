using System;
using System.IO;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;

namespace SDK.Examples
{
	public class FieldValidationExample : SDKSample
	{
        public static void Main (string[] args)
        {
            new FieldValidationExample(Props.GetInstance()).Run();
        }

        private string email1;
        private Stream fileStream1;

        public FieldValidationExample( Props props ) : this(props.Get("api.url"), props.Get("api.key"), props.Get("1.email")) {
        }

        public FieldValidationExample( String apiKey, String apiUrl, String email1 ) : base( apiKey, apiUrl ) {
            this.email1 = email1;
            this.fileStream1 = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/src/document.pdf").FullName);
        }

        override public void Execute()
        {
            DocumentPackage package = PackageBuilder.NewPackageNamed ("FieldValidationExample example " + DateTime.Now)
					.DescribedAs ("This is a new package")
					.WithSigner(SignerBuilder.NewSignerWithEmail(email1)
					            .WithFirstName("John")
					            .WithLastName("Smith"))
					.WithDocument(DocumentBuilder.NewDocumentNamed("My Document")
                                  .FromStream(fileStream1, DocumentType.PDF)
					              .WithSignature(SignatureBuilder.SignatureFor(email1)
					              		.OnPage(0)
					               		.AtPosition(500, 100)
					               		.WithField(FieldBuilder.TextField()
					           				.OnPage(0)
					           				.AtPosition(500, 200)
					           				.WithValidation(FieldValidatorBuilder.Alphabetic()
					                			.MaxLength(10)
					                			.MinLength(3)
					                			.Required()
					                			.WithErrorMessage("This field is not alphabetic")))
					               		.WithField(FieldBuilder.TextField()
					           				.OnPage(0)
					           				.AtPosition(500, 300)
					           				.WithValidation(FieldValidatorBuilder.Numeric()					                			
					                			.WithErrorMessage("This field is not numeric")))
					               		.WithField(FieldBuilder.TextField()
					           				.OnPage(0)
					           				.AtPosition(500, 400)
					           				.WithValidation(FieldValidatorBuilder.Alphanumeric()					                			
					                			.WithErrorMessage("This field is not alphanumeric")))
					               		.WithField(FieldBuilder.TextField()
							           		.OnPage(0)
							           		.AtPosition(500, 500)
							           		.WithValidation(FieldValidatorBuilder.Email()					                			
							                	.WithErrorMessage("The value in this field is not an email address")))
					               		.WithField(FieldBuilder.TextField()
					           				.OnPage(0)
					           				.AtPosition(500, 600)
					           				.WithValidation(FieldValidatorBuilder.URL()
					                			.WithErrorMessage("The value in this field is not a valid URL")))
					               		.WithField(FieldBuilder.TextField()
					           				.OnPage(0)
					           				.AtPosition(500, 700)
					           				.WithValidation(FieldValidatorBuilder.Regex("^[0-9a-zA-Z]+$")
					                			.WithErrorMessage("The value in this field does not match the expression")))))
					.Build ();

			PackageId id = eslClient.CreatePackage (package);

			eslClient.SendPackage(id);
		}
	}
}