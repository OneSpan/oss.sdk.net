using System;
using System.Collections.Generic;
using Silanis.ESL.API;

namespace Silanis.ESL.SDK.Builder
{
	public class SignatureBuilder
	{
		public static double DEFAULT_HEIGHT = 50d;
		public static double DEFAULT_WIDTH = 200d;
		public static SignatureStyle DEFAULT_STYLE = SignatureStyle.FULL_NAME;

        private string name;
		private string signerEmail;
		private double width = DEFAULT_WIDTH;
		private double height = DEFAULT_HEIGHT;
		private SignatureStyle style = DEFAULT_STYLE;
		private int page;
		private double x;
		private double y;
		private IList<Field> fields = new List<Field>();
		private bool extract;
        private TextAnchor textAnchor;
        private GroupId groupId;

		private SignatureBuilder (string signerEmail)
		{
			this.signerEmail = signerEmail;
            this.groupId = null;
		}

        private SignatureBuilder (GroupId groupId )
        {
            this.signerEmail = null;
            this.groupId = groupId;
        }

        public static SignatureBuilder AcceptanceFor (string signerEmail)
        {
            return new SignatureBuilder(signerEmail)
                .WithStyle(SignatureStyle.ACCEPTANCE)
                    .WithSize(0,0)
                    .AtPosition(0,0)
                    .OnPage(0);
        }

		public static SignatureBuilder AcceptanceFor (GroupId groupId )
		{
			return new SignatureBuilder(groupId)
					.WithStyle(SignatureStyle.ACCEPTANCE)
					.WithSize(0,0)
					.AtPosition(0,0)
					.OnPage(0);
		}

        public static SignatureBuilder SignatureFor( GroupId groupId )
        {
            return new SignatureBuilder(groupId);
        }

		public static SignatureBuilder SignatureFor (string signerEmail)
		{
			return new SignatureBuilder (signerEmail);
		}

		public static SignatureBuilder InitialsFor (string signerEmail)
		{
			return new SignatureBuilder (signerEmail).WithStyle (SignatureStyle.INITIALS);
		}

		public static SignatureBuilder InitialsFor ( GroupId groupId )
		{
			return  new SignatureBuilder(groupId).WithStyle(SignatureStyle.INITIALS);
		}

		public static SignatureBuilder CaptureFor (string signerEmail)
		{
			return new SignatureBuilder (signerEmail).WithStyle (SignatureStyle.HAND_DRAWN);
		}

		public static SignatureBuilder CaptureFor (GroupId groupId)
		{
			return new SignatureBuilder(groupId).WithStyle(SignatureStyle.HAND_DRAWN);
		}

		internal static SignatureBuilder NewSignatureFromAPIApproval (Silanis.ESL.API.Approval apiApproval, Silanis.ESL.API.Package package)
		{
            SignatureBuilder signatureBuilder = null;
			Silanis.ESL.API.Signer apiSigner = null;
			foreach ( Silanis.ESL.API.Role role in package.Roles ) {
				if ( role.Id.Equals( apiApproval.Role ) ) {
                    if ( !isPlaceHolder( role ) ) {
					    apiSigner = role.Signers [0];
                    }
				}
			}
            if (apiSigner != null)
            {
                if (apiSigner.Group == null)
                {
                    signatureBuilder = new SignatureBuilder(apiSigner.Email);
                }
                else
                {
                    signatureBuilder = new SignatureBuilder(new GroupId(apiSigner.Group.Id));
                }
            }
            else
            {
                signatureBuilder = new SignatureBuilder("");
            }
            signatureBuilder.WithName(apiApproval.Name);

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
				signatureBuilder.WithStyle( FromAPIFieldSubType(apiSignatureField.Subtype) )
					.OnPage( apiSignatureField.Page )
						.AtPosition( apiSignatureField.Left, apiSignatureField.Top )
						.WithSize( apiSignatureField.Width, apiSignatureField.Height );

				if ( apiSignatureField.Extract ) {
					signatureBuilder.EnableExtraction ();
				}					
			}

			return signatureBuilder;
		}

		private static SignatureStyle FromAPIFieldSubType( Silanis.ESL.API.FieldSubtype subtype ) {
			switch( subtype ) 
			{
			case Silanis.ESL.API.FieldSubtype.INITIALS:
				return SignatureStyle.INITIALS;
			case Silanis.ESL.API.FieldSubtype.CAPTURE:
				return SignatureStyle.HAND_DRAWN;
			case Silanis.ESL.API.FieldSubtype.FULLNAME:
				return SignatureStyle.FULL_NAME;
			default:
				throw new EslException ("FieldSubtype unknown: " + subtype);
			}
		}

		public SignatureBuilder WithName (string name)
		{
			this.name = name;
			return this;
		}

		public SignatureBuilder EnableExtraction()
		{
			extract = true;
			return this;
		}

		public SignatureBuilder OnPage (int page)
		{
			this.page = page;
			return this;
		}

		public SignatureBuilder AtPosition (double x, double y)
		{
			this.x = x;
			this.y = y;
			return this;
		}

		public SignatureBuilder WithStyle (SignatureStyle style)
		{
			this.style = style;
			return this;
		}

		public SignatureBuilder WithSize (double width, double height)
		{
			this.width = width;
			this.height = height;
			return this;
		}

		public SignatureBuilder WithField (FieldBuilder builder)
		{
			return WithField (builder.Build());
		}

		public SignatureBuilder WithField (Field field)
		{
			fields.Add (field);
			return this;
		}

        public SignatureBuilder WithPositionAnchor( TextAnchorBuilder builder ) {
            return WithPositionAnchor(builder.Build());
        }

        public SignatureBuilder WithPositionAnchor( TextAnchor textAnchor ) {
            this.textAnchor = textAnchor;
            return this;
        }

		public Signature Build()
		{
			Support.LogMethodEntry();
            Signature signature;
            if (signerEmail != null)
            {
                signature = new Signature(signerEmail, page, x, y);
            }
            else
            {
                signature = new Signature(groupId, page, x, y);
            }

			signature.Height = height;
			signature.Width = width;
			signature.Style = style;
			signature.AddFields (fields);
			signature.Name = name;
			signature.Extract = extract;
            signature.TextAnchor = textAnchor;
			Support.LogMethodExit(signature);
			return signature;
		}

        private static bool isPlaceHolder( Role role ) {
            return role.Signers.Count == 0;
        }
	}
}