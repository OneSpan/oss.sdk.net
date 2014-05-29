//
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
namespace Silanis.ESL.API
{
    [JsonConverter(typeof(StringEnumConverter))]
	internal enum FieldSubtype
	{
		FULLNAME,INITIALS,CAPTURE,LABEL,TEXTFIELD,TEXTAREA,CHECKBOX,DATE,RADIO,LIST,QRCODE,NOTARIZE,CUSTOMFIELD
	}
}