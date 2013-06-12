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

			foreach ( Field field in signature.Fields ) {
				result.AddField( ToAPIField( field ) );
			}

			return result;
		}

		private Silanis.ESL.API.Field ToAPIField(Field field) {
			Silanis.ESL.API.Field result = new Silanis.ESL.API.Field();

			result.Page = field.Page;
			result.Left = field.X;
			result.Top = field.Y;
			result.Width = field.Width;
			result.Height = field.Height;

//			result.Value = field.Value;
			result.Type = Silanis.ESL.API.FieldType.INPUT;
			result.Subtype = GetFieldSubtype(field);
			result.Binding = field.Binding;

//			if ( field.getFieldValidator() != null ) {
//				result.setValidation( new FieldValidatorConverter( field.getFieldValidator() ).getESLFieldValidation() );
//			}

			return result;
		}

		private Silanis.ESL.API.FieldSubtype GetFieldSubtype (Field field)
		{
			switch (field.Style) 
			{
			case FieldStyle.UNBOUND_TEXT_FIELD:
				return Silanis.ESL.API.FieldSubtype.TEXTFIELD;
			case FieldStyle.BOUND_DATE:
			case FieldStyle.BOUND_NAME:
			case FieldStyle.BOUND_TITLE:
			case FieldStyle.BOUND_COMPANY:
				return Silanis.ESL.API.FieldSubtype.LABEL;
			case FieldStyle.UNBOUND_CHECK_BOX:
				return Silanis.ESL.API.FieldSubtype.CHECKBOX;
			default:
				throw new EslException(String.Format ("Unable to decode the field subtype from style {0}", field.Style) );
			}
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

