using System;
using NUnit.Framework;
using Silanis.ESL.SDK;
using System.Globalization;

namespace SDK.Examples
{
    public class UpdatePackageExampleTest
    {
        [Test]
        public void verify() {
            // Asserts that are commented out are so because updating them is not currently supported by the esl server.
        
            UpdatePackageExample example = new UpdatePackageExample(Props.GetInstance());
            example.Run();

            // compare the package itself
            Assert.AreEqual( example.UpdatedPackage.Name, example.RetrievedPackage.Name );
            Assert.AreEqual( example.UpdatedPackage.Description, example.RetrievedPackage.Description );
            Assert.AreEqual( example.UpdatedPackage.EmailMessage, example.RetrievedPackage.EmailMessage );
            Assert.AreEqual( example.UpdatedPackage.Autocomplete, example.RetrievedPackage.Autocomplete );
            Assert.LessOrEqual( (example.UpdatedPackage.ExpiryDate-example.RetrievedPackage.ExpiryDate).Value.Seconds, 10 );
//            Assert.AreEqual( example.SentSettings.Language, example.RetrievedPackage.Language );
                                                
            // compare the package settings
//            Assert.AreEqual( example.UpdatedSettings.HideCaptureText, example.RetrievedSettings.HideCaptureText );
//            Assert.AreEqual( example.UpdatedSettings.EnableDecline, example.RetrievedSettings.EnableDecline );
//            Assert.AreEqual( example.UpdatedSettings.ShowDialogOnComplete, example.RetrievedSettings.ShowDialogOnComplete );
//            Assert.AreEqual( example.UpdatedSettings.ShowDownloadButton, example.RetrievedSettings.ShowDownloadButton );
//            Assert.AreEqual( example.UpdatedSettings.LinkHref, example.RetrievedSettings.LinkHref );
//            Assert.AreEqual( example.UpdatedSettings.LinkText, example.RetrievedSettings.LinkText );
//            Assert.AreEqual( example.UpdatedSettings.LinkTooltip, example.RetrievedSettings.LinkTooltip );
            Assert.AreEqual( example.UpdatedSettings.EnableInPerson, example.RetrievedSettings.EnableInPerson );
//            Assert.AreEqual( example.UpdatedSettings.EnableOptOut, example.RetrievedSettings.EnableOptOut );

/// Opt Out Reasons are a special case.  Any information passed is added to the list of opt out reasons.  There's no way to get rid of old ones, I don't think.

//            Assert.AreEqual( example.UpdatedSettings.OptOutReasons.Count, example.RetrievedSettings.OptOutReasons.Count );
//            foreach( string reason in example.UpdatedSettings.OptOutReasons )
//            {
//                Assert.Contains(reason, example.RetrievedSettings.OptOutReasons);
//            }
//            Assert.AreEqual( example.UpdatedSettings.HideWatermark, example.RetrievedSettings.HideWatermark );

            // compare the layout settings
//            Assert.AreEqual( example.UpdatedLayoutSettings.BreadCrumbs, example.RetrievedLayoutSettings.BreadCrumbs );
//            Assert.AreEqual( example.UpdatedLayoutSettings.ShowGlobalConfirmButton, example.RetrievedLayoutSettings.ShowGlobalConfirmButton );
//            Assert.AreEqual( example.UpdatedLayoutSettings.ShowGlobalDownloadButton, example.RetrievedLayoutSettings.ShowGlobalDownloadButton );
//            Assert.AreEqual( example.UpdatedLayoutSettings.GlobalNavigation, example.RetrievedLayoutSettings.GlobalNavigation );
//            Assert.AreEqual( example.UpdatedLayoutSettings.ShowGlobalSaveAsLayoutButton, example.RetrievedLayoutSettings.ShowGlobalSaveAsLayoutButton );
//            Assert.AreEqual( example.UpdatedLayoutSettings.IFrame, example.RetrievedLayoutSettings.IFrame );
//            Assert.AreEqual( example.UpdatedLayoutSettings.LogoImageLink, example.RetrievedLayoutSettings.LogoImageLink );
//            Assert.AreEqual( example.UpdatedLayoutSettings.LogoImageSource, example.RetrievedLayoutSettings.LogoImageSource );
//            Assert.AreEqual( example.UpdatedLayoutSettings.Navigator, example.RetrievedLayoutSettings.Navigator );
//            Assert.AreEqual( example.UpdatedLayoutSettings.ProgressBar, example.RetrievedLayoutSettings.ProgressBar );
//            Assert.AreEqual( example.UpdatedLayoutSettings.SessionBar, example.RetrievedLayoutSettings.SessionBar );
//            Assert.AreEqual( example.UpdatedLayoutSettings.ShowTitle, example.RetrievedLayoutSettings.ShowTitle );
        }
        
//        [Test]
//        public void verifyCultureInfoDeserialisation()
//        {
//            JsonSerializerSettings settings = new JsonSerializerSettings ();
//            settings.NullValueHandling = NullValueHandling.Ignore;
//            settings.DateTimeZoneHandling = DateTimeZoneHandling.Local;
//            settings.Converters.Add( new CultureInfoJsonCreationConverter() );
//
//            string json = "{ \"cultureInfo\":\"en\" }";
//            
//            Bob bob = JsonConvert.DeserializeObject<Bob> (json, settings);
//            
//            Console.Out.WriteLine( "Blah" );
//        }
    }
}

