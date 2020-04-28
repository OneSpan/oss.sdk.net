using System;
using System.IO;
using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Builder;
using System.Collections.Generic;

namespace SDK.Examples
{
    public class SigningStyleExample : SDKSample
    {
        public static void Main (string [] args)
        {
            new SigningStyleExample ().Run ();
        }

        public IDictionary<string, object> createdSigningThemes, retrievedSigningThemes, updatedSigningThemes, removedSigningThemes;
        public string signingThemesStringToCreate = "{\"default\":{\"color\":{\"primary\":\"#5940C3\"}}}";
        public string signingThemesStringToUpdate = "{\"default\":{\"color\":{\"primary\":\"#5940C3\",\"secondary\": \"#F31C8B\"}}}";

        public List<SigningLogo> createdSigningLogos, updatedSigningLogos, removedSigningLogos;

        override public void Execute ()
        {
            // Create default signing theme
            createdSigningThemes = ossClient.GetSigningStyleService ().CreateSigningThemes (signingThemesStringToCreate);

            // Get default signing theme
            retrievedSigningThemes = ossClient.GetSigningStyleService ().GetSigningThemes ();

            // Update the default signing theme
            updatedSigningThemes = ossClient.GetSigningStyleService ().UpdateSigningThemes (signingThemesStringToUpdate);

            // Delete default signing theme
            ossClient.GetSigningStyleService ().DeleteSigningThemes ();

            removedSigningThemes = ossClient.GetSigningStyleService ().GetSigningThemes ();


            // SigningLogo operations
            OssClient.GetSigningStyleService ().SaveSigningLogos (new List<SigningLogo>());

            // Create signing logos
            List<SigningLogo> signingLogos = new List<SigningLogo> ();

            SigningLogo signingLogoEn = SigningLogoBuilder.NewSigningLogo ()
                    .WithLanguage ("en")
                    .WithImage ("data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAEUAAABnCAYAAABW6Y8UAAAABGdBTUEAALGPC")
                    .Build ();
            signingLogos.Add(signingLogoEn);
                      
            OssClient.GetSigningStyleService ().SaveSigningLogos (signingLogos);
            createdSigningLogos = OssClient.GetSigningStyleService ().GetSigningLogos ();

            // Update signing logos
            SigningLogo signingLogoFr = SigningLogoBuilder.NewSigningLogo ()
                    .WithLanguage ("fr")
                    .WithImage ("data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAEUAAABnCAYAAABW6Y8UAAAABGdBTUEAALGPC")
                    .Build ();
            signingLogos.Add (signingLogoFr);           
             
            OssClient.GetSigningStyleService ().SaveSigningLogos (signingLogos);
            updatedSigningLogos = OssClient.GetSigningStyleService ().GetSigningLogos ();

            // Delete signing logos
            OssClient.GetSigningStyleService ().SaveSigningLogos (new List<SigningLogo> ());
            removedSigningLogos = OssClient.GetSigningStyleService ().GetSigningLogos ();
        }
    }
}