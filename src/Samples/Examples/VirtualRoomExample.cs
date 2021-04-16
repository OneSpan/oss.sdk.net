using System;
using OneSpanSign.Sdk.Builder;
using OneSpanSign.Sdk;
namespace SDK.Examples
{

    public class VirtualRoomExample : SDKSample
    {

        public VirtualRoom VirtualRoomAfterUpdate;
        public string HostUid;
        public DateTime StartDateTime;

        public static void Main(string[] args)
        {
            new VirtualRoomExample().Run();
        }

        override public void Execute()
        {
            Signer signer = SignerBuilder.NewSignerWithEmail(email1)
                .WithFirstName("Patty")
                .WithLastName("Galant")
                .Build();

            HostUid = signer.Id;
            StartDateTime = DateTime.UtcNow.AddDays(7);

            VirtualRoom VirtualRoom = VirtualRoomBuilder.NewVirtualRoom()
                .WithHostUid(HostUid)
                .WithVideo(true)
                .WithVideoRecording(true)
                .WithStartDateTime(StartDateTime)
                .Build();

            DocumentPackage superDuperPackage = PackageBuilder.NewPackageNamed(PackageName)
                .DescribedAs("Description")
                .WithSigner(signer)
                .WithDocument(DocumentBuilder.NewDocumentNamed("Document")
                        .WithId("DocumentId")
                        .FromStream(fileStream1, DocumentType.PDF))
                .Build();

            packageId = ossClient.CreatePackageOneStep(superDuperPackage);
            retrievedPackage = ossClient.GetPackage(packageId);

            OssClient.VirtualRoomService.SetVirtualRoom(packageId, VirtualRoom);
            VirtualRoomAfterUpdate = OssClient.VirtualRoomService.GetVirtualRoom(packageId);
        }
    }
}
