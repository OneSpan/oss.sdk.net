using System;
using System.Collections.Generic;
using OneSpanSign.Sdk.Internal;

namespace OneSpanSign.Sdk
{
    public class SeverityLevel : OssEnumeration
    {
        private static ILogger log = LoggerFactory.get(typeof(SeverityLevel));

        public static SeverityLevel INFO = new SeverityLevel("INFO", "INFO", 0);
        public static SeverityLevel WARNING = new SeverityLevel("WARNING", "WARNING", 1);
        public static SeverityLevel CRITICAL = new SeverityLevel("CRITICAL", "CRITICAL", 2);
        private static Dictionary<string,SeverityLevel> allSeverityLevels = new Dictionary<string,SeverityLevel>();

        static SeverityLevel()
        {
            allSeverityLevels.Add(INFO.getApiValue(), INFO);
            allSeverityLevels.Add(WARNING.getApiValue(), WARNING);
            allSeverityLevels.Add(CRITICAL.getApiValue(), CRITICAL);
        }

        private SeverityLevel(string apiValue, string sdkValue, int index):base(apiValue, sdkValue, index) 
        {           
        }

        internal static SeverityLevel valueOf (string apiValue)
        {

            if (!String.IsNullOrEmpty(apiValue) && allSeverityLevels.ContainsKey(apiValue))
            {
                return allSeverityLevels[apiValue];
            }
            log.Warn("Unknown API SeverityLevel {0}. The upgrade is required.", apiValue);
            return new SeverityLevel(apiValue, "UNRECOGNIZED", allSeverityLevels.Values.Count);
        }

        public static string[] GetNames()
        {
            string[] names = new string[allSeverityLevels.Count];
            int i = 0;
            foreach(SeverityLevel SeverityLevel in allSeverityLevels.Values)
            {
                names[i] = SeverityLevel.GetName();
                i++;
            }
            return names;
        }

        public static explicit operator SeverityLevel(Enum enumType)
        {
            return parse(enumType.ToString());
        }

        public static SeverityLevel[] Values()
        {
            return (new List<SeverityLevel>(allSeverityLevels.Values)).ToArray();
        }

        public static SeverityLevel parse(string value)
        {

            if (null == value)
            {
                throw new ArgumentNullException("value is null");
            }

            if (value.Length == 0 || value.Trim().Length==0)
            {
                throw new ArgumentException("value is either an empty string or only contains white space");
            }
            foreach(SeverityLevel SeverityLevel in allSeverityLevels.Values)
            {
                if (String.Equals(SeverityLevel.GetName(), value))
                {
                    return SeverityLevel;
                }
            }
            throw new ArgumentException("value is a name, but not one of the named constants defined for the SeverityLevel");
        }
    }
}