using System;
using System.IO;
using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Builder;
using System.Globalization;
using OneSpanSign.Sdk.Models;

namespace SDK.Examples
{
    public class LocalizeConsentPackageExample : SDKSample
    {
        public static void Main(string[] args)
        {
            new UpdatePackageExample().Run();
        }

        public readonly string INITIAL_PACKAGE_NAME = "Package Name";
        public readonly string OLD_LANGUAGE = "en";
        public readonly string NEW_LANGUAGE = "fr";
        

        public DocumentPackage packageToCreate;
        public ConsentLocalizationData result;

        override public void Execute()
        {
            packageToCreate = PackageBuilder.NewPackageNamed(INITIAL_PACKAGE_NAME)
                                          .WithLanguage(CultureInfo.GetCultureInfo("en"))
                                          .Build();

            packageId = ossClient.CreatePackage(packageToCreate);
            result = ossClient.LocalizeConsent(NEW_LANGUAGE, packageId);
        }
    }
}
