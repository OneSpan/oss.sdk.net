using System;
using System.IO;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;
using System.Globalization;

namespace SDK.Examples
{
    public class UpdatePackageExample : SDKSample
    {
        public static void Main(string[] args)
        {
            new UpdatePackageExample(Props.GetInstance()).Run();
        }

        private PackageId packageId;
        
        private DocumentPackage sentPackage;
        public DocumentPackage SentPackage
        {
            get{ return sentPackage; }
        }

        private DocumentPackageSettings sentSettings;
        public DocumentPackageSettings SentSettings
        {
            get{ return sentSettings; }
        }
        
        private CeremonyLayoutSettings sentLayoutSettings;
        public CeremonyLayoutSettings SentLayoutSettings
        {
            get{ return sentLayoutSettings; }
        }

        private DocumentPackage updatedPackage;        
        public DocumentPackage UpdatedPackage
        {
            get{ return updatedPackage;}
        }
        
        private DocumentPackageSettings updatedSettings;
        public DocumentPackageSettings UpdatedSettings
        {
            get{ return updatedSettings; }
        }
        
        private CeremonyLayoutSettings updatedLayoutSettings;
        public CeremonyLayoutSettings UpdatedLayoutSettings
        {
            get{ return updatedLayoutSettings; }
        }

        private DocumentPackage retrievedPackage;
        public DocumentPackage RetrievedPackage
        {
            get{ return retrievedPackage; }
        }
        
        private DocumentPackageSettings retrievedSettings;
        public DocumentPackageSettings RetrievedSettings
        {
            get{ return retrievedSettings; }
        }

        private CeremonyLayoutSettings retrievedLayoutSettings;
        public CeremonyLayoutSettings RetrievedLayoutSettings
        {
            get{ return retrievedLayoutSettings; }
        }
                
        public PackageId PackageId
        {
            get{ return packageId; }
        }
        
        public UpdatePackageExample(Props props) : this(props.Get("api.url"), props.Get("api.key"))
        {
        }

        public UpdatePackageExample(string apiKey, string apiUrl) : base( apiKey, apiUrl )
        {
        }

        override public void Execute()
        {
            sentLayoutSettings = CeremonyLayoutSettingsBuilder.NewCeremonyLayoutSettings()
                                .WithBreadCrumbs()
                                .WithGlobalConfirmButton()
                                .WithGlobalDownloadButton()
                                .WithGlobalNavigation()
                                .WithGlobalSaveAsLayoutButton()
                                .WithIFrame()
                                .WithLogoImageLink( "old logo image link" )
                                .WithLogoImageSource( "old logo image source" )
                                .WithNavigator()
                                .WithProgressBar()
                                .WithSessionBar()
                                .WithTitle()
                                .Build();
                                
            sentSettings = DocumentPackageSettingsBuilder.NewDocumentPackageSettings()
                    .WithCaptureText()
                    .WithDecline()
                    .WithDialogOnComplete()
                    .WithDocumentToolbarDownloadButton()
                    .WithHandOverLinkHref("http://www.old.ca")
                    .WithHandOverLinkText("old hand over link text")
                    .WithHandOverLinkTooltip("old hand over link tool tip")
                    .WithInPerson()
                    .WithOptOut()
                    .WithOptOutReason("old opt out reason #1")
                    .WithOptOutReason("old opt out reason #2")
                    .WithOptOutReason("old opt out reason #3")
                    .WithWatermark()
                    .WithCeremonyLayoutSettings(sentLayoutSettings)
                    .Build();
                    
            sentPackage = PackageBuilder.NewPackageNamed("Old Package Name")
                                          .DescribedAs("Old Description")
                                          .WithEmailMessage("Old Email Message")
                                          .ExpiresOn(DateTime.Now.AddMonths(1))
                                          .WithLanguage( CultureInfo.GetCultureInfo("en"))
                                          .WithAutomaticCompletion()
                                          .WithSettings( sentSettings )
                                          .Build();

            packageId = eslClient.CreatePackage(sentPackage);

            updatedLayoutSettings = CeremonyLayoutSettingsBuilder.NewCeremonyLayoutSettings()
                                .WithoutBreadCrumbs()
                                .WithoutGlobalConfirmButton()
                                .WithoutGlobalDownloadButton()
                                .WithoutGlobalNavigation()
                                .WithoutGlobalSaveAsLayoutButton()
                                .WithoutIFrame()
                                .WithLogoImageLink( "new logo image link" )
                                .WithLogoImageSource( "new logo image source" )
                                .WithoutNavigator()
                                .WithoutProgressBar()
                                .WithoutSessionBar()
                                .WithoutTitle()
                                .Build();

            updatedSettings = DocumentPackageSettingsBuilder.NewDocumentPackageSettings()
                    .WithoutCaptureText()
                    .WithoutDecline()
                    .WithoutDialogOnComplete()
                    .WithoutDocumentToolbarDownloadButton()
                    .WithHandOverLinkHref("http://www.new.ca")
                    .WithHandOverLinkText("new hand over link text")
                    .WithHandOverLinkTooltip("new hand over link tool tip")
                    .WithoutInPerson()
                    .WithoutOptOut()
                    .WithOptOutReason("new opt out reason #1")
                    .WithOptOutReason("new opt out reason #2")
                    .WithOptOutReason("new opt out reason #3")
                    .WithoutWatermark()
                    .WithCeremonyLayoutSettings(updatedLayoutSettings)
                    .Build();

            updatedPackage = PackageBuilder.NewPackageNamed( "New Package Name" )
                               .WithEmailMessage( "New Email Message" )
                               .ExpiresOn(DateTime.Now.AddMonths(2))
                               .WithLanguage( CultureInfo.GetCultureInfo("fr"))
                               .WithoutAutomaticCompletion()
                               .WithSettings( updatedSettings )
                               .Build();

            eslClient.UpdatePackage(packageId, updatedPackage);

            retrievedPackage = eslClient.GetPackage( packageId );
            retrievedSettings = retrievedPackage.Settings;
            retrievedLayoutSettings = retrievedSettings.CeremonyLayoutSettings;
        }
    }
}
