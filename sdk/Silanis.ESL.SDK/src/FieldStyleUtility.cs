using System;

namespace Silanis.ESL.SDK
{
	internal static class FieldStyleUtility
	{
		public static string Binding(this FieldStyle style)
		{
			switch (style)
			{
			case FieldStyle.BOUND_DATE:
				return "{approval.signed}";
			case FieldStyle.BOUND_NAME:
				return "{signer.name}";
			case FieldStyle.BOUND_TITLE:
				return "{signer.title}";
			case FieldStyle.BOUND_COMPANY:
				return "{signer.company}";
			case FieldStyle.UNBOUND_TEXT_FIELD:
			case FieldStyle.UNBOUND_CHECK_BOX:
			case FieldStyle.LABEL:
				return null;
			default:
				throw new EslException(String.Format ("Unknown FieldStyle value {0}", style));
			}
		}
	}
}