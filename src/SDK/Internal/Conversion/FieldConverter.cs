using System;
using OneSpanSign.API;
using OneSpanSign.Sdk;
using OneSpanSign.Sdk.src.Internal.Conversion;
using OneSpanSign.Sdk.Builder;

namespace OneSpanSign.Sdk
{
    internal class FieldConverter
    {
        private OneSpanSign.Sdk.Field sdkField = null;
        private OneSpanSign.API.Field apiField = null;

        public FieldConverter (OneSpanSign.Sdk.Field sdkField)
        {
            this.sdkField = sdkField;
        }

        public FieldConverter (OneSpanSign.API.Field apiField)
        {
            this.apiField = apiField;
        }

        public OneSpanSign.API.Field ToAPIField ()
        {
            if (sdkField == null) {
                return apiField;
            }

            OneSpanSign.API.Field result = new OneSpanSign.API.Field ();

            result.Name = sdkField.Name;
            result.Extract = sdkField.Extract;
            result.Page = sdkField.Page;
            result.Id = sdkField.Id;
            result.FontSize = sdkField.FontSize;

            if (!sdkField.Extract) {
                result.Left = sdkField.X;
                result.Top = sdkField.Y;
                result.Width = sdkField.Width;
                result.Height = sdkField.Height;
            }

            if (sdkField.TextAnchor != null) {
                result.ExtractAnchor = new TextAnchorConverter (sdkField.TextAnchor).ToAPIExtractAnchor ();
            }

            result.Value = sdkField.Value;

            if (sdkField.Style == FieldStyle.BOUND_QRCODE) {
                result.Type = new FieldTypeConverter (FieldType.IMAGE).ToAPIFieldType ();
            } else {
                result.Type = new FieldTypeConverter (FieldType.INPUT).ToAPIFieldType ();
            }

            result.Subtype = new FieldStyleAndSubTypeConverter (sdkField.Style).ToAPIFieldSubtype ();
            result.Binding = sdkField.Binding;

            if (sdkField.Validator != null) {
                result.Validation = new FieldValidatorConverter (sdkField.Validator).ToAPIFieldValidation ();
            }

            return result;
        }

        public OneSpanSign.Sdk.Field ToSDKField ()
        {
            if (apiField == null) {
                return sdkField;
            }

            FieldBuilder fieldBuilder = FieldBuilder.NewField ()
                    .OnPage (apiField.Page.Value)
                    .AtPosition (apiField.Left.Value, apiField.Top.Value)
                    .WithSize (apiField.Width.Value, apiField.Height.Value)
                    .WithStyle (new FieldStyleAndSubTypeConverter (apiField.Subtype, apiField.Binding).ToSDKFieldStyle ())
                    .WithName (apiField.Name);

            if (apiField.Id != null) {
                fieldBuilder.WithId (apiField.Id);
            }

            if (apiField.FontSize != null) {
                fieldBuilder.WithFontSize (apiField.FontSize);
            }

            if (apiField.Extract.Value) {
                fieldBuilder.WithPositionExtracted ();
            }

            if (apiField.Validation != null) {
                fieldBuilder.WithValidation (new FieldValidatorConverter (apiField.Validation).ToSDKFieldValidator ());
            }

            fieldBuilder.WithValue (apiField.Value);
            return fieldBuilder.Build ();

        }
    }
}

