using System.Collections.Generic;
using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Builder;
using OneSpanSign.Sdk.Services;

namespace SDK.Examples
{
    public class IdvAuthExample : SDKSample
    {
        public static void Main(string[] args)
        {
            new IdvAuthExample().Run();
        }

        public static readonly string PHONE_NUMBER = "+15555555555";
        public static readonly string IDV_WORKFLOW_ID1 = "00000000-0000-0001-0000-200000000055";
        public static readonly string TYPE1 = "DV";
        public static readonly string TENANT = "oss";
        public static readonly string DESC1 = "This is Mitek Document verification only Mock workflow.";

        public static readonly string IDV_WORKFLOW_ID2 = "00000000-0001-0001-0000-200000000055";
        public static readonly string TYPE2 = "DVF";
        public static readonly string DESC2 = "This is Mitek Document Verification with Facial Comparison Mock workflow.";

        public readonly string DOCUMENT_NAME = "First Document";

        public IList<IdvWorkflowConfig> idvWorkflowConfigsBeforeCreating,
            idvWorkflowConfigsAfterCreating,
            idvWorkflowConfigsAfterUpdating,
            idvWorkflowConfigsAfterDeleting;
        
        public readonly IList<IdvWorkflowConfig> idvWorkflowConfigsToBeCreated = new List<IdvWorkflowConfig>
            {IdvWorkflowConfigBuilder.NewIdvWorkflowConfig(IDV_WORKFLOW_ID1)
                .WithType(TYPE1)
                .WithTenant(TENANT)
                .WithDesc(DESC1)
                .EnableSkipWhenAccessingSignedDocuments()
                .Build()};

        public readonly IList<IdvWorkflowConfig> idvWorkflowConfigsToBeUpdated = new List<IdvWorkflowConfig>
            {IdvWorkflowConfigBuilder.NewIdvWorkflowConfig(IDV_WORKFLOW_ID1)
                .WithType(TYPE1)
                .WithTenant(TENANT)
                .WithDesc(DESC1)
                .EnableSkipWhenAccessingSignedDocuments()
                .Build(), IdvWorkflowConfigBuilder.NewIdvWorkflowConfig(IDV_WORKFLOW_ID2)
                .WithType(TYPE2)
                .WithTenant(TENANT)
                .WithDesc(DESC2)
                .EnableSkipWhenAccessingSignedDocuments()
                .Build()};

        override public void Execute()
        {
            AccountConfigService accountConfigService = ossClient.AccountConfigService;
            idvWorkflowConfigsBeforeCreating = accountConfigService.GetIdvWorkflowConfigs();

            if (idvWorkflowConfigsBeforeCreating.Count != 0)
            {
                accountConfigService.DeleteIdvWorkflowConfigs();
                idvWorkflowConfigsBeforeCreating = accountConfigService.GetIdvWorkflowConfigs();
            }

            idvWorkflowConfigsAfterCreating = accountConfigService
                .CreateIdvWorkflowConfigs(idvWorkflowConfigsToBeCreated);

            idvWorkflowConfigsAfterUpdating = accountConfigService
                .UpdateIdvWorkflowConfigs(idvWorkflowConfigsToBeUpdated);

            DocumentPackage superDuperPackage = PackageBuilder.NewPackageNamed(PackageName)
                .DescribedAs("This is a Signer IDV authentication example")
                .WithSigner(SignerBuilder.NewSignerWithEmail(email1)
                    .WithFirstName("Jamie Anne")
                    .WithLastName("Kurtz")
                    .WithIDVAuthentication(PHONE_NUMBER,
                        IdvWorkflowBuilder.NewIdvWorkflow(IDV_WORKFLOW_ID1)
                            .WithType(TYPE1)
                            .WithTenant(TENANT)
                            .Build()))
                .WithDocument(DocumentBuilder.NewDocumentNamed(DOCUMENT_NAME)
                    .FromStream(fileStream1, DocumentType.PDF)
                    .WithSignature(SignatureBuilder.SignatureFor(email1)
                        .OnPage(0)
                        .AtPosition(100, 100)))
                .Build();

            packageId = ossClient.CreatePackage(superDuperPackage);
            ossClient.SendPackage(packageId);
            retrievedPackage = ossClient.GetPackage(packageId);
            
            accountConfigService.DeleteIdvWorkflowConfigs();
            idvWorkflowConfigsAfterDeleting = accountConfigService.GetIdvWorkflowConfigs();
        }
    }
}