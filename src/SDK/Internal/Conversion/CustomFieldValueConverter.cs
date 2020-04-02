using System;
using OneSpanSign.Sdk.Builder;

namespace OneSpanSign.Sdk
{
    internal class CustomFieldValueConverter
    {
        private OneSpanSign.Sdk.CustomFieldValue sdkCustomFieldValue = null;
        private OneSpanSign.API.UserCustomField apiUserCustomField = null;

        public CustomFieldValueConverter(OneSpanSign.Sdk.CustomFieldValue sdkCustomFieldValue)
        {
            this.sdkCustomFieldValue = sdkCustomFieldValue;
        }

        internal CustomFieldValueConverter(OneSpanSign.API.UserCustomField apiUserCustomField)
        {
            this.apiUserCustomField = apiUserCustomField;
        }

        internal OneSpanSign.API.UserCustomField ToAPIUserCustomField()
        {
            if (sdkCustomFieldValue == null)
            {
                return apiUserCustomField;
            }

            OneSpanSign.API.UserCustomField result = new OneSpanSign.API.UserCustomField();

            result.Id = sdkCustomFieldValue.Id;
            result.Value = sdkCustomFieldValue.Value;
            result.Name = "";

            return result;
        }

        public OneSpanSign.Sdk.CustomFieldValue ToSDKCustomFieldValue()
        {
            if (apiUserCustomField == null)
            {
                return sdkCustomFieldValue;
            }

            CustomFieldValueBuilder result = new CustomFieldValueBuilder();
            result.WithId(apiUserCustomField.Id)
                    .WithValue(apiUserCustomField.Value);

            return result.build();
        }
    }
}

