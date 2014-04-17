using System;
using System.IO;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;

namespace SDK.Examples
{
    public class SignerManipulationExample : SDKSample
    {
        private string email1;
        private string email2;
        private string email3;
        private Stream fileStream1;
        private Stream fileStream2;
        private PackageId packageId;

        public PackageId PackageId
        {
            get
            {
                return packageId;
            }
        }
        
        public SignerManipulationExample(Props props) : this(props.Get("api.url"), props.Get("api.key"), props.Get("1.email"), props.Get("2.email"), props.Get("3.email"))
        {
        }

        public SignerManipulationExample(string apiKey, string apiUrl, string email1, string email2, string email3) : base( apiKey, apiUrl )
        {
            this.email1 = email1;
            this.email2 = email2;
            this.email3 = email3;
            this.fileStream1 = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/src/document.pdf").FullName);
            this.fileStream2 = File.OpenRead(new FileInfo(Directory.GetCurrentDirectory() + "/src/document.pdf").FullName);
        }

        override public void Execute()
        {
            Placeholder signer1Id = new Placeholder( Guid.NewGuid().ToString() );
            DocumentPackage superDuperPackage = 
                PackageBuilder.NewPackageNamed("SignerManipulationExample: " + DateTime.Now)
                .DescribedAs("This is a package created using the e-SignLive SDK")
                .WithSigner(SignerBuilder.NewSignerWithEmail(email1)
                            .WithFirstName("firstName1")
                            .WithLastName("lastName1")
                            .WithTitle("Title1")
            )
                .WithSigner(SignerBuilder.NewSignerWithEmail(email2)
                            .WithFirstName("firstName2")
                            .WithLastName("lastName2")
                            .WithTitle("Title2")
            )
                .Build();

            packageId = eslClient.CreatePackage(superDuperPackage);
            
            DocumentPackage createdPackage = eslClient.GetPackage(packageId);
            
            string signerId = createdPackage.Signers[email1].RoleId;
            string signer2Id = createdPackage.Signers[email2].RoleId;
            
//            eslClient.SignerService.UpdateSigner( packageId, signerId, SignerBuilder.NewSignerWithEmail(email1)
//                                                                    .WithFirstName("firstName1b")
//                                                                    .WithLastName("lastName1b")
//                                                                    .WithTitle("title1b") );

            string addedSignerId = eslClient.SignerService.AddSigner(packageId, 
                                                                      SignerBuilder.NewSignerWithEmail(email3)
                                                                              .WithFirstName("firstName3")
                                                                              .WithLastName("lastName3")
                                                                              .WithTitle("Title3")
                                                                              .Build()
            );
                                                                              
            string placeHolderId = eslClient.SignerService.AddSigner(packageId,
                                                                      SignerBuilder.NewSignerPlaceholder(new Placeholder("placeHolderRoleId"))
                                                                                .Build()
            );                                                                                    

            Group avengers = eslClient.GroupService.CreateGroup( GroupBuilder.NewGroup(  Guid.NewGuid().ToString() ).WithEmail("bob@aol.com").Build() );                                                                                
            string addedGroupSignerId = eslClient.SignerService.AddSigner( packageId,
                                                                           SignerBuilder.NewSignerFromGroup( avengers.Id )
                                                                                .Build() );
                                                                                                                                                                
            eslClient.SignerService.RemoveSigner( packageId, placeHolderId );                                                                                
            eslClient.SignerService.RemoveSigner( packageId, signerId );
            
            eslClient.SignerService.UpdateSigner( packageId, SignerBuilder.NewSignerWithEmail("timbob@aol.com")
                            .WithRoleId( signer2Id )
                            .WithFirstName("updateFirstName1")
                            .WithLastName("updateLastName1")
                            .WithTitle("UpdatedTitle1")
                            .Build() );

            Signer retrievedSigner = eslClient.SignerService.GetSigner(packageId, addedSignerId);
        }
    }
}

