using System;

namespace Silanis.ESL.SDK
{
	internal static class DocumentTypeUtility
	{
		public static string NormalizeName(this DocumentType type, string name)
		{
			string normalized = name.Replace (' ', '_');

			if (!normalized.EndsWith (type.Extension()))
			{
				if (normalized.EndsWith("."))
				{
					normalized = normalized.Substring (0, normalized.Length - 1);
				}

				normalized += "." + type.Extension();
			}

			return normalized;
		}

		public static string Extension(this DocumentType type)
		{
			switch (type)
			{
			case DocumentType.PDF:
				return "pdf";
			case DocumentType.WORD:
				return "docx";
			default:
				throw new EslException("Unknown DocumentType: " + type);
			}
		}
	}
}