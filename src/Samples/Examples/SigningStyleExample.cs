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

        public SigningUiOptions defaultSigningUiOptions, patchedSigningUiOptions, deletedSigningUiOptions;
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
                    .WithImage ("data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAHgAAAB4BAMAAADLSivhAAAAMFBMVEVAoaL///9Hpab4/PxUq6zv9/e43NzX6+ul09ORyclisrPj8fFyubrH4+SAwME/oaJIgbRbAAAEIUlEQVR4Xu3Zf2hbVRQH8LO3LG2y19rT7OU1S1/JWiIgKGayAoNJHooIK9A3BdlESGSiKMI6sYIyyGMVAXSmKAXBjXRMRBmYMBjCABJQBQQxyAQKYiqiKlD694AhL0lf7n3318vLn/YLFE7hk3O593BzIYAjZB/v4xN33rpzKxr++g/w8tDV4fHZb0Bb2tn59/ouJP8cDhtbu0v9jub7RUisDIOr2mWiWgOtHRobnUCr218ly2Fx6WkMxKzda4TDF+PIJFVLhMKZRAHZmM3TIbBxo4G8zOTKarzsIj/nEkpsfo6iVLZVeAuFsXQFttZRnDMtOb6EkmQnpdhYQVnqrhCrY8ZHwFgqjIDnbCk2Hal+UIKNj0Fbl+G6I8YlAIgVZOtuCfEseLFRkrwQdwD2Wmdf2fnnA84aXhXhDPSyjmg0AQD0Nxg83RDgeh9PIC5DN7FyEFstPk4VoU8Qa9CLzpxcno8t2Itr5MBfRSCf8PG0j/MDDMFjP8nHv/lgPDXAkwE8W+DiGvgpFEHU2nK5ODcAbgVErQ2bhy0YxJ4CYevPeHiOwGMZAsdp/DIPTxM4aQGRFQqf4uFHCaw5JF6g8CIP14FIu0pgncIzStyiP4rEaR6+QOJDx0l8kMSWEsePgnDdSjw5A6L9VuNYisLHhsLgFEk8Phx2O1Q5HLYrFN5W4DqFF36iSwVepnCeLicVeIrCY14Za/hT2pDjIxQeP+r9GXyiK8cZCutemRhsxAE5NimspT2y4Z92Qo4RqJh0GVPgIoWfBzptOaZn6k0A7nin+Jgei7s5Go/1gcli9qBf8jG9YxkWs2f1bgBDH/zFxybd2VfUmBjHWMxu92tBvNmbQ5fF7HXwugfYW7DiCDB16Z31AHObmDoKsLU7ANp0EGvdVR8UYWwSfeoeYK7Q2roQE2ChCpztngMU4rQP7i0CExuxEw9gdt1Xvry5UmHxJmbAluDeLj3y3up1YONgUytIcBbEyeNJmEAGi+ZEI8c75pgArhSnCfvDYydK5LOmCjpKMZZ8+8W1H6/ii3v2ezwPsKnAJnUHPIxr3Tr2Lb4NkEQFxjNA5nTqhU+fXNp4BtcA4AklNpqU/s7xwKr3z/tZJcYZoBLb+OVKrbdl59QYnwVuxrI5R43xPM/qTgnaITD+zlqt/Dg84IbBqQ8ZfDmdA9hmMFevBfp+lK6BaEjYbFFzXV7slnZIjM91/BvtqVtY5T7HAIW5fe3G/Pz8zb9/9h8sB3hYndlRsNHFh6NhLI6Cm6PgSlRs/Hq392w4FAFfAGgvR9xt02NTQw0J/c0bPwLc2VbGW3GiOyWt4XEJALQudrlYfUrHPdyI0rmfJIbG7ONuIgKe28N2BJzqW60QGrNPlcMYBRs1z+qFSBitIoDuYDSMxqV3/r8/v+zj/wCkvAtd4Bni/AAAAABJRU5ErkJggg==")
                    .Build ();
            signingLogos.Add(signingLogoEn);
                      
            OssClient.GetSigningStyleService ().SaveSigningLogos (signingLogos);
            createdSigningLogos = OssClient.GetSigningStyleService ().GetSigningLogos ();

            // Update signing logos
            SigningLogo signingLogoFr = SigningLogoBuilder.NewSigningLogo ()
                    .WithLanguage ("fr")
                    .WithImage ("data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAHgAAAB4BAMAAADLSivhAAAAMFBMVEVAoaL///9Hpab4/PxUq6zv9/e43NzX6+ul09ORyclisrPj8fFyubrH4+SAwME/oaJIgbRbAAAEIUlEQVR4Xu3Zf2hbVRQH8LO3LG2y19rT7OU1S1/JWiIgKGayAoNJHooIK9A3BdlESGSiKMI6sYIyyGMVAXSmKAXBjXRMRBmYMBjCABJQBQQxyAQKYiqiKlD694AhL0lf7n3318vLn/YLFE7hk3O593BzIYAjZB/v4xN33rpzKxr++g/w8tDV4fHZb0Bb2tn59/ouJP8cDhtbu0v9jub7RUisDIOr2mWiWgOtHRobnUCr218ly2Fx6WkMxKzda4TDF+PIJFVLhMKZRAHZmM3TIbBxo4G8zOTKarzsIj/nEkpsfo6iVLZVeAuFsXQFttZRnDMtOb6EkmQnpdhYQVnqrhCrY8ZHwFgqjIDnbCk2Hal+UIKNj0Fbl+G6I8YlAIgVZOtuCfEseLFRkrwQdwD2Wmdf2fnnA84aXhXhDPSyjmg0AQD0Nxg83RDgeh9PIC5DN7FyEFstPk4VoU8Qa9CLzpxcno8t2Itr5MBfRSCf8PG0j/MDDMFjP8nHv/lgPDXAkwE8W+DiGvgpFEHU2nK5ODcAbgVErQ2bhy0YxJ4CYevPeHiOwGMZAsdp/DIPTxM4aQGRFQqf4uFHCaw5JF6g8CIP14FIu0pgncIzStyiP4rEaR6+QOJDx0l8kMSWEsePgnDdSjw5A6L9VuNYisLHhsLgFEk8Phx2O1Q5HLYrFN5W4DqFF36iSwVepnCeLicVeIrCY14Za/hT2pDjIxQeP+r9GXyiK8cZCutemRhsxAE5NimspT2y4Z92Qo4RqJh0GVPgIoWfBzptOaZn6k0A7nin+Jgei7s5Go/1gcli9qBf8jG9YxkWs2f1bgBDH/zFxybd2VfUmBjHWMxu92tBvNmbQ5fF7HXwugfYW7DiCDB16Z31AHObmDoKsLU7ANp0EGvdVR8UYWwSfeoeYK7Q2roQE2ChCpztngMU4rQP7i0CExuxEw9gdt1Xvry5UmHxJmbAluDeLj3y3up1YONgUytIcBbEyeNJmEAGi+ZEI8c75pgArhSnCfvDYydK5LOmCjpKMZZ8+8W1H6/ii3v2ezwPsKnAJnUHPIxr3Tr2Lb4NkEQFxjNA5nTqhU+fXNp4BtcA4AklNpqU/s7xwKr3z/tZJcYZoBLb+OVKrbdl59QYnwVuxrI5R43xPM/qTgnaITD+zlqt/Dg84IbBqQ8ZfDmdA9hmMFevBfp+lK6BaEjYbFFzXV7slnZIjM91/BvtqVtY5T7HAIW5fe3G/Pz8zb9/9h8sB3hYndlRsNHFh6NhLI6Cm6PgSlRs/Hq392w4FAFfAGgvR9xt02NTQw0J/c0bPwLc2VbGW3GiOyWt4XEJALQudrlYfUrHPdyI0rmfJIbG7ONuIgKe28N2BJzqW60QGrNPlcMYBRs1z+qFSBitIoDuYDSMxqV3/r8/v+zj/wCkvAtd4Bni/AAAAABJRU5ErkJggg==")
                    .Build ();
            signingLogos.Add (signingLogoFr);           
             
            OssClient.GetSigningStyleService ().SaveSigningLogos (signingLogos);
            updatedSigningLogos = OssClient.GetSigningStyleService ().GetSigningLogos ();

            // Delete signing logos
            OssClient.GetSigningStyleService ().SaveSigningLogos (new List<SigningLogo> ());
            removedSigningLogos = OssClient.GetSigningStyleService ().GetSigningLogos ();
            
            //Get signing ui options
            defaultSigningUiOptions = OssClient.GetSigningStyleService().GetSigningUiOptions();

            SigningUiOptions signingUiOptions = SigningUiOptionsBuilder.NewSigningUiOptions()
                .WithOverviewOptions(OverviewOptionsBuilder.NewOverviewOptions()
                    .WithoutTitle()
                    .WithoutBody()
                    .WithoutDocumentSection()
                    .WithoutUploadSection()
                    .Build())
                .WithCompleteSummaryOptions(CompleteSummaryOptionsBuilder.NewCompleteSummaryOptions()
                    .WithoutFrom()
                    .WithoutTitle()
                    .WithoutMessage()
                    .WithoutDownload()
                    .WithoutReview()
                    .WithoutContinue()
                    .Build())
                .Build();
            //Save signing ui options
            OssClient.GetSigningStyleService().SaveSigningUiOptions(signingUiOptions);
            patchedSigningUiOptions = OssClient.GetSigningStyleService().GetSigningUiOptions();
            
            //Delete signing ui options
            OssClient.GetSigningStyleService().DeleteSigningUiOptions();
            deletedSigningUiOptions = OssClient.GetSigningStyleService().GetSigningUiOptions();

        } 
    }
}