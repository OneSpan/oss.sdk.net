using System;

namespace OneSpanSign.Sdk
{
    internal class FieldTypeConverter
    {
        private OneSpanSign.Sdk.FieldType sdkFieldType;
        private string apiFieldType;

        /// <summary>
        /// Construct with API FieldType object involved in conversion.
        /// </summary>
        /// <param name="apiFieldType">API sender type.</param>
        public FieldTypeConverter(string apiFieldType)
        {
            this.apiFieldType = apiFieldType;
        }

        /// <summary>
        /// Construct with SDK FieldType object involved in conversion.
        /// </summary>
        /// <param name="sdkFieldType">SDK sender type.</param>
        public FieldTypeConverter(OneSpanSign.Sdk.FieldType sdkFieldType)
        {
            this.sdkFieldType = sdkFieldType;
        }

        /// <summary>
        /// Convert from SDK FieldType to API FieldType.
        /// </summary>
        /// <returns>The API sender type.</returns>
        public string ToAPIFieldType()
        {
            if (null == sdkFieldType)
            {
                return apiFieldType;
            }
            return sdkFieldType.getApiValue();
        }

        /// <summary>
        /// Convert from API FieldType to SDK FieldType.
        /// </summary>
        /// <returns>The SDK sender type.</returns>
        public OneSpanSign.Sdk.FieldType ToSDKFieldType()
        {
            return OneSpanSign.Sdk.FieldType.valueOf(apiFieldType);
        }
    }
}

