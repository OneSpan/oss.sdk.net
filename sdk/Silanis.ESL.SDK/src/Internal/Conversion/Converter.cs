using System;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.src.Internal.Conversion;

namespace Silanis.ESL.SDK.Internal.Conversion
{
	internal class Converter
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

		public Silanis.ESL.API.Field ToAPIField(Field field) {
			Silanis.ESL.API.Field result = new Silanis.ESL.API.Field();

			result.Name = field.Name;
			result.Extract = field.Extract;
			result.Page = field.Page;

			if (!field.Extract)
			{
				result.Left = field.X;
				result.Top = field.Y;
				result.Width = field.Width;
				result.Height = field.Height;
			}

            if (field.TextAnchor != null)
            {
                result.ExtractAnchor = new TextAnchorConverter(field.TextAnchor).ToAPIExtractAnchor();
            }
            
            result.Value = field.Value;
			result.Type = Silanis.ESL.API.FieldType.INPUT;
			result.Subtype = GetFieldSubtype(field);
			result.Binding = field.Binding;

			if ( field.Validator != null ) {
				result.Validation = ConvertValidator (field.Validator);
			}

			return result;
		}

		private Silanis.ESL.API.FieldValidation ConvertValidator (FieldValidator validator)
		{
			Silanis.ESL.API.FieldValidation validation = new Silanis.ESL.API.FieldValidation();

			validation.MaxLength = validator.MaxLength;
			validation.MinLength = validator.MinLength;
			validation.Required = validator.Required;
			validation.ErrorMessage = validator.Message;

			if (!String.IsNullOrEmpty(validator.Regex)) {
				validation.Pattern = validator.Regex;
			}

			return validation;
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
			case FieldStyle.LABEL:
				return Silanis.ESL.API.FieldSubtype.LABEL;
			case FieldStyle.UNBOUND_CHECK_BOX:
				return Silanis.ESL.API.FieldSubtype.CHECKBOX;
			case FieldStyle.UNBOUND_CUSTOM_FIELD:
				return Silanis.ESL.API.FieldSubtype.CUSTOMFIELD;
			default:
				throw new EslException(String.Format ("Unable to decode the field subtype from style {0}", field.Style) );
			}
		}

		private Silanis.ESL.API.Field ToField(Signature signature) {
			Silanis.ESL.API.Field result = new Silanis.ESL.API.Field();

			result.Page = signature.Page;
			result.Name = signature.Name;
			result.Extract = signature.Extract;

			if (!signature.Extract)
			{
				result.Top = signature.Y;
				result.Left = signature.X;
				result.Width = signature.Width;
				result.Height = signature.Height;
			}

            if (signature.TextAnchor != null)
            {
                result.ExtractAnchor = new TextAnchorConverter(signature.TextAnchor).ToAPIExtractAnchor();
            }


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
                case SignatureStyle.ACCEPTANCE:
                    return Silanis.ESL.API.FieldSubtype.FULLNAME;
    			default:
    				throw new EslException("Unknown SignatureStyle value: " + signature.Style );
			}
		}
	}
}

