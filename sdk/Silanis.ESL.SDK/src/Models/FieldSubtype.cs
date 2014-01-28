//
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
namespace Silanis.ESL.API
{
    [JsonConverter(typeof(StringEnumConverter))]
	internal enum FieldSubtype
	{
		FULLNAME,INITIALS,CAPTURE,NOTARIZE,LABEL,TEXTFIELD,CUSTOMFIELD,TEXTAREA,CHECKBOX,DATE,RADIO,LIST,QRCODE
	}
}