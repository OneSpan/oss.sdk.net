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
				result.AddField( new FieldConverter( field ).ToAPIField() );
			}

			return result;
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

