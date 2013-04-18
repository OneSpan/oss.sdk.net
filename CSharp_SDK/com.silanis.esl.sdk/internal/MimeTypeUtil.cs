using System;
using System.Collections.Generic;
using System.IO;

namespace CSharp_SDK
{
	public class MimeTypeUtil
	{

		private static readonly Dictionary<string, string> FileExtensionMap = new Dictionary<string, string>
		{
			// PDF
			{"pdf", "application/pdf"},

			// MS Office
			{"doc", "application/msword"},
			{"dot", "application/msword"},
			{"docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document"},
			{"dotx", "application/vnd.openxmlformats-officedocument.wordprocessingml.template"},
			{"docm", "application/vnd.ms-word.document.macroEnabled.12"},
			{"dotm", "application/vnd.ms-word.template.macroEnabled.12"},
			{"xls", "application/vnd.ms-excel"},
			{"xlt", "application/vnd.ms-excel"},
			{"xla", "application/vnd.ms-excel"},
			{"xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
			{"xltx", "application/vnd.openxmlformats-officedocument.spreadsheetml.template"},
			{"xlsm", "application/vnd.ms-excel.sheet.macroEnabled.12"},
			{"xltm", "application/vnd.ms-excel.template.macroEnabled.12"},
			{"xlam", "application/vnd.ms-excel.addin.macroEnabled.12"},
			{"xlsb", "application/vnd.ms-excel.sheet.binary.macroEnabled.12"},
			{"ppt", "application/vnd.ms-powerpoint"},
			{"pot", "application/vnd.ms-powerpoint"},
			{"pps", "application/vnd.ms-powerpoint"},
			{"ppa", "application/vnd.ms-powerpoint"},
			{"pptx", "application/vnd.openxmlformats-officedocument.presentationml.presentation"},
			{"potx", "application/vnd.openxmlformats-officedocument.presentationml.template"},
			{"ppsx", "application/vnd.openxmlformats-officedocument.presentationml.slideshow"},
			{"ppam", "application/vnd.ms-powerpoint.addin.macroEnabled.12"},
			{"pptm", "application/vnd.ms-powerpoint.presentation.macroEnabled.12"},
			{"potm", "application/vnd.ms-powerpoint.presentation.macroEnabled.12"},
			{"ppsm", "application/vnd.ms-powerpoint.slideshow.macroEnabled.12"},
			// Open Office
			{"odt", "application/vnd.oasis.opendocument.text"},
			{"ott", "application/vnd.oasis.opendocument.text-template"},
			{"oth", "application/vnd.oasis.opendocument.text-web"},
			{"odm", "application/vnd.oasis.opendocument.text-master"},
			{"odg", "application/vnd.oasis.opendocument.graphics"},
			{"otg", "application/vnd.oasis.opendocument.graphics-template"},
			{"odp", "application/vnd.oasis.opendocument.presentation"},
			{"otp", "application/vnd.oasis.opendocument.presentation-template"},
			{"ods", "application/vnd.oasis.opendocument.spreadsheet"},
			{"ots", "application/vnd.oasis.opendocument.spreadsheet-template"},
			{"odc", "application/vnd.oasis.opendocument.chart"},
			{"odf", "application/vnd.oasis.opendocument.formula"},
			{"odb", "application/vnd.oasis.opendocument.database"},
			{"odi", "application/vnd.oasis.opendocument.image"},
			{"oxt", "application/vnd.openofficeorg.extension"},
		};

		public static string GetMIMEType (string fileName)
		{
			if (FileExtensionMap.ContainsKey (Path.GetExtension (fileName).Remove (0, 1))) {
				return FileExtensionMap [Path.GetExtension (fileName).Remove (0, 1)];
			}
			return "unknown/unknown";
		}
	}
}

