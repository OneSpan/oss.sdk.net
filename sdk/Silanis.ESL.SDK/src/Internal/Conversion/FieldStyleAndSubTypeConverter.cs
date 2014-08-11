using System;
using Silanis.ESL.API;

namespace Silanis.ESL.SDK
{
    internal class FieldStyleAndSubTypeConverter
    {
        private static string BINDING_DATE = "{approval.signed}";
        private static string BINDING_TITLE = "{signer.title}";
        private static string BINDING_NAME = "{signer.name}";
        private static string BINDING_COMPANY = "{signer.company}";

        private Nullable<FieldStyle> sdkFieldStyle;
        private Nullable<FieldSubtype> apiFieldSubType;
        private string apiFieldBinding;

        public FieldStyleAndSubTypeConverter(FieldStyle sdkFieldStyle)
        {
            this.sdkFieldStyle = sdkFieldStyle;
            apiFieldSubType = null;
            apiFieldBinding = null;
        }

        public FieldStyleAndSubTypeConverter(FieldSubtype apiFieldSubtype, String apiFieldBinding)
        {
            this.apiFieldSubType = apiFieldSubtype;
            this.apiFieldBinding = apiFieldBinding;
            sdkFieldStyle = null;
        }

        public FieldSubtype ToAPIFieldSubtype()
        {
            if (!sdkFieldStyle.HasValue)
            {
                return apiFieldSubType.Value;
            }

            switch (sdkFieldStyle) 
            {
                case FieldStyle.UNBOUND_TEXT_FIELD:
                    return Silanis.ESL.API.FieldSubtype.TEXTFIELD;
                case FieldStyle.BOUND_DATE:
                case FieldStyle.BOUND_NAME:
                case FieldStyle.BOUND_TITLE:
                case FieldStyle.BOUND_COMPANY:
                case FieldStyle.LABEL:
                    return Silanis.ESL.API.FieldSubtype.LABEL;
                case FieldStyle.UNBOUND_CHECK_BOX:
                    return Silanis.ESL.API.FieldSubtype.CHECKBOX;
                case FieldStyle.UNBOUND_RADIO_BUTTON:
                    return Silanis.ESL.API.FieldSubtype.RADIO;
                case FieldStyle.UNBOUND_CUSTOM_FIELD:
                    return Silanis.ESL.API.FieldSubtype.CUSTOMFIELD;
                case FieldStyle.BOUND_QRCODE:
                    return Silanis.ESL.API.FieldSubtype.QRCODE;
                default:
                    throw new EslException(String.Format ("Unable to decode the field subtype from style {0}", sdkFieldStyle),null );
            }
        }

        public FieldStyle ToSDKFieldStyle()
        {
            if (!apiFieldSubType.HasValue && apiFieldBinding == null) 
            {
                return sdkFieldStyle.Value;
            }

            if (apiFieldBinding == null)
            {
                switch (apiFieldSubType)
                {
                    case FieldSubtype.TEXTFIELD:
                        return FieldStyle.UNBOUND_TEXT_FIELD;
                    case FieldSubtype.CUSTOMFIELD:
                        return FieldStyle.UNBOUND_CUSTOM_FIELD;
                    case FieldSubtype.CHECKBOX:
                        return FieldStyle.UNBOUND_CHECK_BOX;
                    case FieldSubtype.RADIO:
                        return FieldStyle.UNBOUND_RADIO_BUTTON;
                    case FieldSubtype.QRCODE:
                        return FieldStyle.BOUND_QRCODE;
                    default:
                        throw new EslException(String.Format("Unable to decode the style from field subtype {0}", apiFieldSubType),null);
                }
            }
            else
            {
                if (apiFieldBinding == BINDING_DATE)
                {
                    return FieldStyle.BOUND_DATE;
                }
                else if (apiFieldBinding == BINDING_TITLE)
                {
                    return FieldStyle.BOUND_TITLE;
                }
                else if (apiFieldBinding == BINDING_NAME)
                {
                    return FieldStyle.BOUND_NAME;
                }
                else if (apiFieldBinding == BINDING_COMPANY)
                {
                    return FieldStyle.BOUND_COMPANY;
                }
                else
                {
                    throw new EslException(String.Format("Unable to decode the style from field binding {0}", apiFieldBinding),null);
                }
            }
        }
    }
}

