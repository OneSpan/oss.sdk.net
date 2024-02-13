using OneSpanSign.Sdk;
using OneSpanSign.Sdk.Builder;

namespace SDK.Examples
{
    public class SignatureLayoutExample : SDKSample
    {
        public static void Main (string [] args)
        {
            new AccountSettingsExample().Run ();
        }
        public string SIGNATURE_LOGO_IMAGE_BASE64 = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAQAAAAEACAMAAABrrFhUAAAAA1BMVEV6y//7EDFUAAABM0lEQVR4Xu3QgQAAAACAoP2pFymECgMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYMCAAQMGDBgwYGABDwABbQVHVQAAAABJRU5ErkJggg==";

        public SignatureLayout PatchedSignatureLayout;
        
        override public void Execute ()
        {
            SignatureLayout signatureLayout = SignatureLayoutBuilder.NewSignatureLayoutBuilder()
                .WithLogo(SignatureLogoBuilder.NewSignatureLogoBuilder().WithImage(SIGNATURE_LOGO_IMAGE_BASE64).Build())
                .Build();
            
            //Save Account Settings
            OssClient.AccountConfigService.PatchSignatureLayout(signatureLayout);
            PatchedSignatureLayout = OssClient.AccountConfigService.GetSignatureLayout();

        } 
    }
}