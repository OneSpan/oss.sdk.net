using System;

namespace Silanis.ESL.SDK
{
    internal class SignatureStyleConverter
    {
        private Nullable<Silanis.ESL.API.FieldSubtype> apiSubtype;
        private Nullable<SignatureStyle> sdkSignatureStyle;
        
        public SignatureStyleConverter(Silanis.ESL.API.FieldSubtype apiSubtype)
        {
            this.apiSubtype = apiSubtype;
            this.sdkSignatureStyle = null;
        }
        
        public SignatureStyle ToSDKSignatureStyle()
        {
            if (sdkSignatureStyle.HasValue)
            {
                return sdkSignatureStyle.Value;
            }
            
            switch( apiSubtype ) 
            {
            case Silanis.ESL.API.FieldSubtype.INITIALS:
                return SignatureStyle.INITIALS;
            case Silanis.ESL.API.FieldSubtype.CAPTURE:
                return SignatureStyle.HAND_DRAWN;
            case Silanis.ESL.API.FieldSubtype.FULLNAME:
                return SignatureStyle.FULL_NAME;
            default:
                throw new EslException ("FieldSubtype unknown: " + apiSubtype);
            }
        }
    }
}
