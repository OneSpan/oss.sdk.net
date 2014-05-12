using System;
using Silanis.ESL.API;
using Silanis.ESL.SDK;
using Silanis.ESL.SDK.src.Internal.Conversion;
using Silanis.ESL.SDK.Builder;

namespace Silanis.ESL.SDK
{
    internal class FieldConverter
    {
        private Silanis.ESL.SDK.Field sdkField = null;
        private Silanis.ESL.API.Field apiField = null;

        public FieldConverter(Silanis.ESL.SDK.Field sdkField)
        {
            this.sdkField = sdkField;
        }

        public FieldConverter(Silanis.ESL.API.Field apiField)
        {
            this.apiField = apiField;
        }

        public Silanis.ESL.API.Field ToAPIField()
        {
            if (sdkField == null)
            {
                return apiField;
            }

            Silanis.ESL.API.Field result = new Silanis.ESL.API.Field();

            result.Name = sdkField.Name;
            result.Extract = sdkField.Extract;
            result.Page = sdkField.Page;
            result.Id = sdkField.Id;

            if (!sdkField.Extract)
            {
                result.Left = sdkField.X;
                result.Top = sdkField.Y;
                result.Width = sdkField.Width;
                result.Height = sdkField.Height;
            }

            if (sdkField.TextAnchor != null)
            {
                result.ExtractAnchor = new TextAnchorConverter(sdkField.TextAnchor).ToAPIExtractAnchor();
            }

            result.Value = sdkField.Value;
            result.Type = Silanis.ESL.API.FieldType.INPUT;
            result.Subtype = new FieldStyleAndSubTypeConverter(sdkField.Style).ToAPIFieldSubtype();
            result.Binding = sdkField.Binding;

            if ( sdkField.Validator != null ) {
                result.Validation = new FieldValidatorConverter(sdkField.Validator).ToAPIFieldValidation();
            }

            return result;
        }

        public Silanis.ESL.SDK.Field ToSDKField()
        {
            if (apiField == null)
            {
                return sdkField;
            }

            FieldBuilder fieldBuilder = FieldBuilder.NewField()
                .OnPage( apiField.Page )
                    .AtPosition( apiField.Left, apiField.Top )
                    .WithSize( apiField.Width, apiField.Height )
                    .WithStyle( new FieldStyleAndSubTypeConverter( apiField.Subtype, apiField.Binding ).ToSDKFieldStyle() )
                    .WithName( apiField.Name );

            if ( apiField.Id != null ) {
                fieldBuilder.WithId( apiField.Id );
            }

            if ( apiField.Extract ) {
                fieldBuilder.WithPositionExtracted();
            }

            fieldBuilder.WithValue( apiField.Value );
            return fieldBuilder.Build();

        }
    }
}

