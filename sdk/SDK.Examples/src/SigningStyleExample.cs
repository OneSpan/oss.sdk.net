using System;
using System.IO;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.Builder;
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

        override public void Execute ()
        {
            // Create default signing theme
            createdSigningThemes = eslClient.GetSigningStyleService ().CreateSigningThemes (signingThemesStringToCreate);

            // Get default signing theme
            retrievedSigningThemes = eslClient.GetSigningStyleService ().GetSigningThemes ();

            // Update the default signing theme
            updatedSigningThemes = eslClient.GetSigningStyleService ().UpdateSigningThemes (signingThemesStringToUpdate);

            // Delete default signing theme
            eslClient.GetSigningStyleService ().DeleteSigningThemes ();

            removedSigningThemes = eslClient.GetSigningStyleService ().GetSigningThemes ();
        }
    }
}