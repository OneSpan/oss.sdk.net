using System.Collections.Generic;
using OneSpanSign.Sdk;

namespace SDK.Examples
{
    public class ChooseSignatureSettingsExample : SDKSample
    {
        public static void Main (string [] args)
        {
            new SigningStyleExample ().Run ();
        }

        public ChooseSignatureSettings defaultChooseSignatureSettings, chooseSignatureSettingsAfterPatch, chooseSignatureSettingsAfterDelete;
        override public void Execute ()
        {
            IDictionary<string, List<string>> fontsPerWritingSystem =
                new Dictionary<string, List<string>>
                {
                    { "latin", new List<string> { "Allison", "Atma", "Bonheur Royale", "Caveat" } },
                    { "cyrillic", new List<string> { "Bellota", "Didact Gothic" } }
                };
            //Get Choose Signature Settings
            defaultChooseSignatureSettings = OssClient.GetSigningStyleService().GetChooseSignatureSettings();

            ChooseSignatureSettings chooseSignatureSettings = ChooseSignatureSettingsBuilder.NewChooseSignatureSettings()
                .WithCompleteSummaryOptions(ChooseSignatureOptionsBuilder.NewChooseSignatureOptions()
                    .WithAllowStyling()
                    .WithoutAllowDrawing()
                    .WithoutAllowUploading()
                    .WithoutAllowMobileSigning()
                    .WithChooseSignatureStyleType(ChooseSignatureStyleType.DRAW)
                    .WithFontsPerWritingSystem(fontsPerWritingSystem)
                    .Build())
                .Build();
            //Save Choose Signature Settings
            OssClient.GetSigningStyleService().SaveChooseSignatureSettings(chooseSignatureSettings);
            chooseSignatureSettingsAfterPatch = OssClient.GetSigningStyleService().GetChooseSignatureSettings();
            
            //Delete Choose Signature Settings
            OssClient.GetSigningStyleService().DeleteChooseSignatureSettings();
            chooseSignatureSettingsAfterDelete = OssClient.GetSigningStyleService().GetChooseSignatureSettings();

        } 
    }
}