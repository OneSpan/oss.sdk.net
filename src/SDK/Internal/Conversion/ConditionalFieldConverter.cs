using System;
using System.Collections.Generic;
using OneSpanSign.Sdk.Builder;
using OneSpanSign.Sdk.src.Internal.Conversion;

namespace OneSpanSign.Sdk
{
    internal class ConditionalFieldConverter
    {

        private OneSpanSign.Sdk.ConditionalField sdkField = null;
        private OneSpanSign.API.ConditionalField apiField = null;

        public ConditionalFieldConverter (OneSpanSign.Sdk.ConditionalField sdkField)
        {
            this.sdkField = sdkField;
        }

        public ConditionalFieldConverter (OneSpanSign.API.ConditionalField apiField)
        {
            this.apiField = apiField;
        }

        public OneSpanSign.API.ConditionalField ToAPIConditionalField ()
        {
            if (sdkField == null) 
            {
                return apiField;
            }

            OneSpanSign.API.ConditionalField result = new OneSpanSign.API.ConditionalField ();

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
                result.ExtractAnchor = new TextAnchorConverter (sdkField.TextAnchor).ToAPIExtractAnchor ();
            }

            result.Value = sdkField.Value;

            if (sdkField.Style == FieldStyle.BOUND_QRCODE) 
            {
                result.Type = new FieldTypeConverter (FieldType.IMAGE).ToAPIFieldType ();
            } 
            else 
            {
                result.Type = new FieldTypeConverter (FieldType.INPUT).ToAPIFieldType ();
            }

            result.Subtype = new FieldStyleAndSubTypeConverter (sdkField.Style).ToAPIFieldSubtype ();
            result.Binding = sdkField.Binding;

            if (sdkField.Validator != null) 
            {
                result.Validation = new FieldValidatorConverter (sdkField.Validator).ToAPIFieldValidation ();
            }
            if(sdkField.Conditions != null) 
            {
                foreach (var condition in sdkField.Conditions) 
                {
                    result.AddCondition (new FieldConditionConverter (condition).ToAPIFieldCondition());
                }
            }

            return result;
        }

        public OneSpanSign.Sdk.ConditionalField ToSDKConditionalField ()
        {
            if (apiField == null) 
            {
                return sdkField;
            }

            OneSpanSign.Sdk.ConditionalField result = new OneSpanSign.Sdk.ConditionalField ();

            result.Name = apiField.Name;
            result.Page = apiField.Page.Value;
            result.Id = apiField.Id;

            result.Extract = apiField.Extract.Value;
            result.X = apiField.Left.Value;
            result.Y = apiField.Top.Value;
            result.Width = apiField.Width.Value;
            result.Height = apiField.Height.Value;


            if (apiField.ExtractAnchor != null) 
            {
                result.TextAnchor = new TextAnchorConverter (apiField.ExtractAnchor).ToSDKTextAnchor ();
            }

            result.Value = apiField.Value;
            result.Style = new FieldStyleAndSubTypeConverter (apiField.Subtype, apiField.Binding).ToSDKFieldStyle ();

            if (apiField.Validation != null) 
            {
                result.Validator = new FieldValidatorConverter (apiField.Validation).ToSDKFieldValidator ();
            }
            if (apiField.Conditions != null) 
            {
                IList<FieldCondition> conditions = new List<FieldCondition> ();
                foreach (var condition in apiField.Conditions) 
                {
                    conditions.Add (new FieldConditionConverter (condition).ToSDKFieldCondition ());
                }
                result.AddConditions (conditions);
            }

            return result;

        }
    }
}
