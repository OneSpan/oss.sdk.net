//
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
namespace Silanis.ESL.API
{
    [JsonConverter(typeof(StringEnumConverter))]
	public enum RequirementStatus
	{
		INCOMPLETE,REJECTED,COMPLETE
	}
}