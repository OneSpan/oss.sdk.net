using System;
using System.IO;
using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Builder;
using System.Globalization;
using OneSpanSign.Sdk.Models;

namespace SDK.Examples
{
    public class UpdatePackageAndLocalizeConsentExample : SDKSample
    {
        public static void Main(string[] args)
        {
            new UpdatePackageExample().Run();
        }

        public readonly string INITIAL_PACKAGE_NAME = "Package Name";
        public readonly string UPDATED_PACKAGE_NAME = "New Package Name";
        public readonly CultureInfo OLD_LANGUAGE = CultureInfo.GetCultureInfo("en");
        public readonly CultureInfo NEW_LANGUAGE = CultureInfo.GetCultureInfo("fr");
        

        public DocumentPackage packageToCreate, packageToUpdate, createdPackage;
        public PackageUpdateWorkflowResult updateWorkflowResult;

        override public void Execute()
        {
            packageToCreate = PackageBuilder.NewPackageNamed(INITIAL_PACKAGE_NAME)
                                          .WithLanguage(OLD_LANGUAGE)
                                          .Build();

            packageId = ossClient.CreatePackage(packageToCreate);
            createdPackage = ossClient.GetPackage( packageId );

            packageToUpdate = PackageBuilder.NewPackageNamed(UPDATED_PACKAGE_NAME)
                               .WithLanguage(NEW_LANGUAGE)
                               .Build();
            updateWorkflowResult = ossClient.UpdatePackageAndLocalizeConsent(packageId, packageToUpdate);
        }
    }
}
