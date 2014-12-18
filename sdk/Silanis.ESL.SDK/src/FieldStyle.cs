using System;
using System.Collections.Generic;
using System.Reflection;
using log4net;

namespace Silanis.ESL.SDK
{
	public class FieldStyle : EslEnumeration
	{
        private static ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static FieldStyle BOUND_DATE = new FieldStyle("LABEL", "BOUND_DATE");
        public static FieldStyle BOUND_NAME = new FieldStyle("LABEL", "BOUND_NAME");
        public static FieldStyle BOUND_TITLE = new FieldStyle("LABEL", "BOUND_TITLE");
        public static FieldStyle BOUND_COMPANY = new FieldStyle("LABEL","BOUND_COMPANY");
        public static FieldStyle BOUND_QRCODE = new FieldStyle("QRCODE", "BOUND_QRCODE");
        public static FieldStyle UNBOUND_TEXT_FIELD = new FieldStyle("TEXTFIELD", "UNBOUND_TEXT_FIELD");
        public static FieldStyle UNBOUND_CUSTOM_FIELD = new FieldStyle("CUSTOMFIELD", "UNBOUND_CUSTOM_FIELD");
        public static FieldStyle UNBOUND_CHECK_BOX = new FieldStyle("CHECKBOX", "UNBOUND_CHECK_BOX");
        public static FieldStyle UNBOUND_RADIO_BUTTON = new FieldStyle("RADIO", "UNBOUND_RADIO_BUTTON");
        public static FieldStyle LABEL = new FieldStyle("LABEL", "LABEL");
        public static FieldStyle TEXT_AREA = new FieldStyle("TEXTAREA", "TEXT_AREA");
        public static FieldStyle DROP_LIST = new FieldStyle("LIST", "DROP_LIST");
        public static FieldStyle SEAL = new FieldStyle("SEAL", "SEAL");
        private static Dictionary<string,FieldStyle> allFieldStyles = new Dictionary<string,FieldStyle>();

        static FieldStyle(){
            allFieldStyles.Add(BOUND_QRCODE.getApiValue(), FieldStyle.BOUND_QRCODE);
            allFieldStyles.Add(UNBOUND_TEXT_FIELD.getApiValue(), FieldStyle.UNBOUND_TEXT_FIELD);
            allFieldStyles.Add(UNBOUND_CUSTOM_FIELD.getApiValue(), FieldStyle.UNBOUND_CUSTOM_FIELD);
            allFieldStyles.Add(UNBOUND_CHECK_BOX.getApiValue(), FieldStyle.UNBOUND_CHECK_BOX);
            allFieldStyles.Add(UNBOUND_RADIO_BUTTON.getApiValue(), FieldStyle.UNBOUND_RADIO_BUTTON);
            allFieldStyles.Add(LABEL.getApiValue(), FieldStyle.LABEL);
            allFieldStyles.Add(TEXT_AREA.getApiValue(), FieldStyle.TEXT_AREA);
            allFieldStyles.Add(DROP_LIST.getApiValue(), FieldStyle.DROP_LIST);
            allFieldStyles.Add(SEAL.getApiValue(), FieldStyle.SEAL);
        }

        private FieldStyle(string apiValue, string sdkValue):base(apiValue, sdkValue) {           
        }
      
        internal static FieldStyle valueOf (String apiValue){

            if (!String.IsNullOrEmpty(apiValue) && allFieldStyles.ContainsKey(apiValue))
            {
                return allFieldStyles[apiValue];
            }
            log.WarnFormat("Unknown API FieldStyle {0}. The upgrade is required.", apiValue);
            return new FieldStyle(apiValue, "UNRECOGNIZED");
        }

        public static string[] GetNames(){
            string[] names = new string[allFieldStyles.Count];
            int i = 0;
            foreach(FieldStyle fieldStyle in allFieldStyles.Values){
                names[i] = fieldStyle.GetName();
                i++;
            }
            return names;
        }

        public static FieldStyle parse(string value){

            if (null == value)
            {
                throw new ArgumentNullException("value is null");
            }

            if (value.Length == 0 || value.Trim().Length==0)
            {
                throw new ArgumentException("value is either an empty string or only contains white space");
            }
            foreach(FieldStyle fieldStyle in allFieldStyles.Values){
                if (String.Equals(fieldStyle.GetName(), value))
                {
                    return fieldStyle;
                }
            }
            throw new ArgumentException("value is a name, but not one of the named constants defined for the FieldStyle");
        }

	}
}