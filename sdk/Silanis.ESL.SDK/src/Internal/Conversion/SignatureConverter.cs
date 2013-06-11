using System;
using Silanis.ESL.SDK;

namespace Silanis.ESL.SDK.Internal.Conversion
{
	internal class SignatureConverter
	{

		public Silanis.ESL.API.Approval ConvertToApproval (Signature signature)
		{
			Silanis.ESL.API.Approval result = new Silanis.ESL.API.Approval();

			result.AddField(ToField(signature));

//			for ( com.silanis.esl.sdk.Field field : signature.getFields() ) {
//				result.addField( ConversionService.convert( field ) );
//			}

			return result;
		}

		private Silanis.ESL.API.Field ToField(Signature signature) {
			Silanis.ESL.API.Field result = new Silanis.ESL.API.Field();

			result.Page = signature.Page;
			result.Top = signature.Y;
			result.Left = signature.X;
			result.Width = signature.Width;
			result.Height = signature.Height;
			result.Type = Silanis.ESL.API.FieldType.SIGNATURE;
			result.Subtype = GetSignatureSubtype (signature);

			return result;
		}

		private Silanis.ESL.API.FieldSubtype GetSignatureSubtype(Signature signature) {
			switch (signature.Style) 
			{
			case SignatureStyle.FULL_NAME:
				return Silanis.ESL.API.FieldSubtype.FULLNAME;
			case SignatureStyle.HAND_DRAWN:
				return Silanis.ESL.API.FieldSubtype.CAPTURE;
			case SignatureStyle.INITIALS:
				return Silanis.ESL.API.FieldSubtype.INITIALS;
			default:
				throw new EslException("Unknown SignatureStyle value: " + signature.Style );
			}
		}
	}
}

