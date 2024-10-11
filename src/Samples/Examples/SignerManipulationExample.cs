using System;
using System.IO;
using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Builder;

namespace SDK.Examples
{
    public class SignerManipulationExample : SDKSample
    {

        override public void Execute()
        {
            Placeholder signer1Id = new Placeholder( Guid.NewGuid().ToString() );
            DocumentPackage superDuperPackage = 
                PackageBuilder.NewPackageNamed(PackageName)
                .DescribedAs("This is a package created using OneSpan Sign SDK")
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

            packageId = ossClient.CreatePackage(superDuperPackage);
            
            DocumentPackage createdPackage = ossClient.GetPackage(packageId);
            
            string signerId = createdPackage.GetSigner(email1).Id;
            string signer2Id = createdPackage.GetSigner(email2).Id;
            
//            ossClient.SignerService.UpdateSigner( packageId, signerId, SignerBuilder.NewSignerWithEmail(email1)
//                                                                    .WithFirstName("firstName1b")
//                                                                    .WithLastName("lastName1b")
//                                                                    .WithTitle("title1b") );

			string addedSignerId = ossClient.PackageService.AddSigner(packageId, 
                                                                      SignerBuilder.NewSignerWithEmail(email3)
                                                                              .WithFirstName("firstName3")
                                                                              .WithLastName("lastName3")
                                                                              .WithTitle("Title3")
                                                                              .Build()
            );
                                                                              
			string placeHolderId = ossClient.PackageService.AddSigner(packageId,
                                                                      SignerBuilder.NewSignerPlaceholder(new Placeholder("placeHolderRoleId"))
                                                                                .Build()
            );                                                                                    

            Group avengers = ossClient.GroupService.CreateGroup( GroupBuilder.NewGroup(  Guid.NewGuid().ToString() ).WithEmail("bob@aol.com").Build() );                                                                                
			string addedGroupSignerId = ossClient.PackageService.AddSigner( packageId,
                                                                           SignerBuilder.NewSignerFromGroup( avengers.Id )
                                                                                .Build() );
                                                                                                                                                                
			ossClient.PackageService.RemoveSigner( packageId, placeHolderId );                                                                                
			ossClient.PackageService.RemoveSigner( packageId, signerId );
            
			ossClient.PackageService.UpdateSigner( packageId, SignerBuilder.NewSignerWithEmail("timbob@aol.com")
                            .WithCustomId( signer2Id )
                            .WithFirstName("updateFirstName1")
                            .WithLastName("updateLastName1")
                            .WithTitle("UpdatedTitle1")
                            .Build() );

			Signer retrievedSigner = ossClient.PackageService.GetSigner(packageId, addedSignerId);
            //ossClient.SendPackage(packageId);
            ossClient.PackageService.UnlockSigner(PackageId, addedSignerId);
            ossClient.GroupService.DeleteGroup(avengers.Id);
        }
    }
}

