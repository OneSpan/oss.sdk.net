using System;
using System.IO;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;

namespace SDK.Examples
{
	public class FieldValidationExample
	{
		public static string apiToken = "Q2xubnp5Y2dIQ3lROnNlY3JldA==";
		public static string baseUrl = "http://localhost:8080";

		public static void Main (string[] args)
		{
			// Create new esl client with api token and base url
			EslClient client = new EslClient (apiToken, baseUrl);
			FileInfo file = new FileInfo (Directory.GetCurrentDirectory() + "/src/document.pdf");

			//IP address
			//regex
			DocumentPackage package = PackageBuilder.NewPackageNamed ("Fields example " + DateTime.Now)
					.DescribedAs ("This is a new package")
					.WithSigner(SignerBuilder.NewSignerWithEmail("etienne_hardy@silanis.com")
					            .WithFirstName("John")
					            .WithLastName("Smith"))
					.WithDocument(DocumentBuilder.NewDocumentNamed("My Document")
					              .FromFile(file.FullName)
					              .WithSignature(SignatureBuilder.SignatureFor("etienne_hardy@silanis.com")
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

			PackageId id = client.CreatePackage (package);

			client.SendPackage(id);

			Console.WriteLine ("Package {0} was sent", id.Id);
		}
	}
}