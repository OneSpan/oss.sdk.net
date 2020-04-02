using System;
using OneSpanSign.Sdk.Builder;

namespace OneSpanSign.Sdk
{
	internal class CustomFieldConverter
    {
		private OneSpanSign.Sdk.CustomField sdkCustomField = null;
		private OneSpanSign.API.CustomField apiCustomField = null;

		public CustomFieldConverter(OneSpanSign.Sdk.CustomField sdkCustomField)
        {
			this.sdkCustomField = sdkCustomField;
        }

		public CustomFieldConverter(OneSpanSign.API.CustomField apiCustomField)
		{
			this.apiCustomField = apiCustomField;
		}

		public OneSpanSign.API.CustomField ToAPICustomField()
		{
			if (sdkCustomField == null)
			{
				return apiCustomField;
			}

			OneSpanSign.API.CustomField result = new OneSpanSign.API.CustomField();

			result.Id = sdkCustomField.Id;
			result.Value = sdkCustomField.Value;
			result.Name = "";
			result.Required = sdkCustomField.Required;

			foreach (Translation translation in sdkCustomField.Translations) 
			{
				result.AddTranslation (translation.toAPITranslation());
			}

			return result;
		}

		public OneSpanSign.Sdk.CustomField ToSDKCustomField()
		{
			if (apiCustomField == null)
			{
				return sdkCustomField;
			}

			CustomFieldBuilder result = new CustomFieldBuilder();
			result.WithId(apiCustomField.Id)
				.WithDefaultValue(apiCustomField.Value)
				.IsRequired(apiCustomField.Required.Value);

			foreach(OneSpanSign.API.Translation translation in apiCustomField.Translations)
			{
				result.WithTranslation(TranslationBuilder.NewTranslation(translation));
			}

			return result.Build();
		}
    }
}

