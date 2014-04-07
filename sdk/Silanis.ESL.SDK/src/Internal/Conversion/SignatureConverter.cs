using System;
using Silanis.ESL.SDK.Builder;

namespace Silanis.ESL.SDK
{
    internal class SignatureConverter
    {
        private Silanis.ESL.API.Approval apiApproval;
        private Silanis.ESL.API.Package package;
        
        public SignatureConverter(Silanis.ESL.API.Approval apiApproval, Silanis.ESL.API.Package package)
        {
            this.apiApproval = apiApproval;
            this.package = package;
        }
        
        private static bool isPlaceHolder( Silanis.ESL.API.Role role ) {
            return role.Signers.Count == 0;
        }
        
        private static bool isGroupRole(Silanis.ESL.API.Role role)
        {
            return role.Signers.Count == 1 && role.Signers[0].Group != null;
        }
        
        public Signature ToSDKSignature() {

            SignatureBuilder signatureBuilder = null;
            foreach ( Silanis.ESL.API.Role role in package.Roles ) {
                if ( role.Id.Equals( apiApproval.Role ) ) {
                    if ( isPlaceHolder( role ) )
                    {
                        signatureBuilder = SignatureBuilder.SignatureFor(new RoleId(role.Id));
                    }
                    else if ( isGroupRole( role ) )
                    {
                        signatureBuilder = SignatureBuilder.SignatureFor(new GroupId(role.Signers [0].Group.Id));
                    }
                    else
                    {
                        signatureBuilder = SignatureBuilder.SignatureFor(role.Signers [0].Email);
                    }
                }
            }

            Silanis.ESL.API.Field apiSignatureField = null;
            foreach ( Silanis.ESL.API.Field apiField in apiApproval.Fields ) {
                if ( apiField.Type == Silanis.ESL.API.FieldType.SIGNATURE ) {
                    apiSignatureField = apiField;
                } else {
                    FieldBuilder fieldBuilder = FieldBuilder.NewFieldFromAPIField( apiField );
                    signatureBuilder.WithField( fieldBuilder );
                }

            }

            if ( apiSignatureField == null ) {
                signatureBuilder.WithStyle( SignatureStyle.ACCEPTANCE );
                signatureBuilder.WithSize( 0, 0 );
            }
            else
            {
                signatureBuilder.WithStyle( new SignatureStyleConverter(apiSignatureField.Subtype).ToSDKSignatureStyle() )
                    .OnPage( apiSignatureField.Page )
                        .AtPosition( apiSignatureField.Left, apiSignatureField.Top )
                        .WithSize( apiSignatureField.Width, apiSignatureField.Height );

                if ( apiSignatureField.Extract ) {
                    signatureBuilder.EnableExtraction ();
                }                   
            }
            
            return signatureBuilder.Build();
        }
    }
}

