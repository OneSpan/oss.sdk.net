using System;
using System.IO;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;

namespace SDK.Examples
{
    public class GenericFieldsExample : SDKSample
    {
        public static void Main(string[] args)
        {
            new GenericFieldsExample(Props.GetInstance()).Run();
        }

        public PackageId PackageId
        {
            get;
            set;
        }

        private string email1;
        private Stream fileStream1;
        public static readonly string DOCUMENT_NAME = "My Document";
        public static readonly string TEXTFIELD_ID = "textFieldId";
        public static readonly int TEXTFIELD_PAGE = 0;
        public static readonly string CHECKBOX_ID = "checkboxId";
        public static readonly int CHECKBOX_PAGE = 0;
        public static readonly bool CHECKBOX_VALUE = true;
        public static readonly int RADIO_PAGE = 0;
        public static readonly double RADIO_WIDTH = 20;
        public static readonly double RADIO_HEIGHT = 20;
        public static readonly string RADIO_ID_1 = "radioId1";
        public static readonly string RADIO_ID_2 = "radioId2";
        public static readonly string RADIO_ID_3 = "radioId3";
        public static readonly string RADIO_ID_4 = "radioId4";
        public static readonly string RADIO_GROUP_1 = "group1";
        public static readonly string RADIO_GROUP_2 = "group2";

        public GenericFieldsExample(Props props) : this(props.Get("api.url"), props.Get("api.key"), props.Get("1.email"))
        {
        }

        public GenericFieldsExample(String apiKey, String apiUrl, String email1) : base( apiKey, apiUrl )
        {
            this.email1 = email1;
            this.fileStream1 = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/src/document.pdf").FullName);
        }

        override public void Execute()
        {
            DocumentPackage package = PackageBuilder.NewPackageNamed("GenericFieldsExample " + DateTime.Now)
					.DescribedAs("This is a new package")
					.WithSigner(SignerBuilder.NewSignerWithEmail(email1)
					            .WithFirstName("John")
					            .WithLastName("Smith"))
                    .WithDocument(DocumentBuilder.NewDocumentNamed(DOCUMENT_NAME)
                                  .FromStream(fileStream1, DocumentType.PDF)
					              .WithSignature(SignatureBuilder.SignatureFor(email1)
					              		.OnPage(0)
					               		.AtPosition(500, 100)
			               		.WithField(FieldBuilder.TextField()
                                    .WithId(TEXTFIELD_ID)
                                    .OnPage(TEXTFIELD_PAGE)
                                    .AtPosition(500, 200))
		               		   .WithField(FieldBuilder.CheckBox()
                                   .WithId(CHECKBOX_ID)
                                   .WithValue(CHECKBOX_VALUE)
                                   .OnPage(CHECKBOX_PAGE)
                                   .AtPosition(500, 300))
                               .WithField(FieldBuilder.RadioButton(RADIO_GROUP_1)
                                   .WithId(RADIO_ID_1)
                                   .WithValue(false)
                                   .WithSize(RADIO_WIDTH, RADIO_HEIGHT)  
                                   .OnPage(RADIO_PAGE)
                                   .AtPosition(500, 400))
                               .WithField(FieldBuilder.RadioButton(RADIO_GROUP_1)
                                   .WithId(RADIO_ID_2)
                                   .WithValue(true)
                                   .WithSize(RADIO_WIDTH, RADIO_HEIGHT) 
                                   .OnPage(RADIO_PAGE)
                                   .AtPosition(500, 450))
                               .WithField(FieldBuilder.RadioButton(RADIO_GROUP_2)
                                   .WithId(RADIO_ID_3)
                                   .WithValue(true)
                                   .WithSize(RADIO_WIDTH, RADIO_HEIGHT)
                                   .OnPage(RADIO_PAGE)
                                   .AtPosition(500, 500))
                               .WithField(FieldBuilder.RadioButton(RADIO_GROUP_2)
                                    .WithId(RADIO_ID_4)
                                    .WithValue(false)
                                    .WithSize(RADIO_WIDTH, RADIO_HEIGHT)
                                    .OnPage(RADIO_PAGE)
                                    .AtPosition(500, 550))))
					.Build();

            PackageId = eslClient.CreatePackage(package);

            eslClient.SendPackage(PackageId);
        }
    }
}