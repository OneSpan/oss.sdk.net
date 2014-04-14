using System;
using System.IO;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;
using System.Collections.Generic;

namespace SDK.Examples
{
    public class TemplateExample : SDKSample
    {
        public static void Main(string[] args)
        {
            new TemplateExample(Props.GetInstance()).Run();
        }

        private string email1;
        private string email2;

        public TemplateExample(Props props) : this(props.Get("api.url"), props.Get("api.key"), props.Get("1.email"), props.Get("2.email") )
        {
        }

        public TemplateExample(string apiKey, string apiUrl, string email1, string email2) : base( apiKey, apiUrl )
        {
            this.email1 = email1;
            this.email2 = email2;
        }

        override public void Execute()
        {
            DocumentPackage superDuperPackage =
                PackageBuilder.NewPackageNamed("CreateTemplateFromPackageExample: " + DateTime.Now)
                .DescribedAs("This is a package created using the e-SignLive SDK")
                .WithEmailMessage("This message should be delivered to all signers")
                .WithSigner(SignerBuilder.NewSignerPlaceholder(new Placeholder("PlaceholderId1")))
                .WithSigner(SignerBuilder.NewSignerPlaceholder(new Placeholder("PlaceholderId2")))
                .Build();

			PackageId templateId = eslClient.CreateTemplate(superDuperPackage);

            PackageId instantiatedTemplate = eslClient.CreatePackageFromTemplate(templateId,
                                             PackageBuilder.NewPackageNamed("Package From Template")
                                             .WithSigner( SignerBuilder.NewSignerWithEmail( email1 )
                                                                        .WithCustomId("Client1")
                                                                        .WithFirstName("John1")
                                                                        .WithLastName("Smith1")
                                                                        .WithTitle("Managing Director1")
                                                                        .WithCompany("Acme Inc.1")
                                                                        .WithRoleId( new Placeholder( "PlaceholderId1" ) ) )
                                             .WithSigner( SignerBuilder.NewSignerWithEmail( email2 )
                                                                        .WithCustomId("Client2")
                                                                        .WithFirstName("John2")
                                                                        .WithLastName("Smith2")
                                                                        .WithTitle("Managing Director2")
                                                                        .WithCompany("Acme Inc.2")
                                                                        .WithRoleId( new Placeholder( "PlaceholderId2" ) ) )
                                             .Build() );
                                             
			Console.Out.WriteLine("Package from template = " + instantiatedTemplate.Id);
        }
    }
}

