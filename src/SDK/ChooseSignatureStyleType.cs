using System;
using System.Collections.Generic;
using OneSpanSign.Sdk.Internal;

namespace OneSpanSign.Sdk
{
    public class ChooseSignatureStyleType : OssEnumeration
    {
        private static ILogger log = LoggerFactory.get(typeof(ChooseSignatureStyleType));

        public static ChooseSignatureStyleType STYLE = new ChooseSignatureStyleType("STYLE", "STYLE", 0);
        public static ChooseSignatureStyleType DRAW = new ChooseSignatureStyleType("DRAW", "DRAW", 1);
        public static ChooseSignatureStyleType UPLOAD = new ChooseSignatureStyleType("UPLOAD", "UPLOAD", 2);
        public static ChooseSignatureStyleType MOBILE = new ChooseSignatureStyleType("MOBILE", "MOBILE", 3);
        private static Dictionary<string,ChooseSignatureStyleType> allChooseSignatureStyleTypes = new Dictionary<string,ChooseSignatureStyleType>();

        static ChooseSignatureStyleType()
        {
            allChooseSignatureStyleTypes.Add(STYLE.getApiValue(), ChooseSignatureStyleType.STYLE);
            allChooseSignatureStyleTypes.Add(DRAW.getApiValue(), ChooseSignatureStyleType.DRAW);
            allChooseSignatureStyleTypes.Add(UPLOAD.getApiValue(), ChooseSignatureStyleType.UPLOAD);
            allChooseSignatureStyleTypes.Add(MOBILE.getApiValue(), ChooseSignatureStyleType.MOBILE);
        }

        
        private ChooseSignatureStyleType(string apiValue, string sdkValue, int index):base(apiValue,sdkValue,index) 
        {           
        }

        internal static ChooseSignatureStyleType valueOf (string apiValue)
        {

            if (!String.IsNullOrEmpty(apiValue) && allChooseSignatureStyleTypes.ContainsKey(apiValue))
            {
                return allChooseSignatureStyleTypes[apiValue];
            }
            log.Warn("Unknown ChooseSignatureStyleType.", apiValue);
            return new ChooseSignatureStyleType(apiValue, "UNRECOGNIZED", allChooseSignatureStyleTypes.Values.Count);
        }

        public static string[] GetNames()
        {
            string[] names = new string[allChooseSignatureStyleTypes.Count];
            int i = 0;
            foreach(ChooseSignatureStyleType chooseSignatureStyleType in allChooseSignatureStyleTypes.Values)
            {
                names[i] = chooseSignatureStyleType.GetName();
                i++;
            }
            return names;
        }

        public static explicit operator ChooseSignatureStyleType(Enum enumType)
        {
            return parse(enumType.ToString());
        }

        public static ChooseSignatureStyleType[] Values()
        {
            return (new List<ChooseSignatureStyleType>(allChooseSignatureStyleTypes.Values)).ToArray();
        }

        public static ChooseSignatureStyleType parse(string value)
        {

            if (null == value)
            {
                throw new ArgumentNullException("value is null");
            }

            if (value.Length == 0 || value.Trim().Length==0)
            {
                throw new ArgumentException("value is either an empty string or only contains white space");
            }
            foreach(ChooseSignatureStyleType chooseSignatureStyleType in allChooseSignatureStyleTypes.Values)
            {
                if (String.Equals(chooseSignatureStyleType.GetName(), value))
                {
                    return chooseSignatureStyleType;
                }
            }
            throw new ArgumentException("value is a name, but not one of the named constants defined for the ChooseSignatureStyleType");
        }
    }
}

