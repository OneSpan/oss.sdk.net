using System;
using System.Collections.Generic;
using System.Reflection;
using log4net;

namespace Silanis.ESL.SDK
{
    public class UsageReportCategory : EslEnumeration
    {
        private static ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static UsageReportCategory ACTIVE = new UsageReportCategory("ACTIVE", "ACTIVE");
        public static UsageReportCategory DRAFT = new UsageReportCategory("DRAFT", "DRAFT");
        public static UsageReportCategory SENT = new UsageReportCategory("SENT", "SENT");
        public static UsageReportCategory COMPLETED = new UsageReportCategory("COMPLETED", "COMPLETED");
        public static UsageReportCategory ARCHIVED = new UsageReportCategory("ARCHIVED", "ARCHIVED");
        public static UsageReportCategory DECLINED = new UsageReportCategory("DECLINED", "DECLINED");
        public static UsageReportCategory OPTED_OUT = new UsageReportCategory("OPTED_OUT", "OPTED_OUT");
        public static UsageReportCategory EXPIRED = new UsageReportCategory("EXPIRED", "EXPIRED");
        public static UsageReportCategory TRASHED = new UsageReportCategory("TRASHED", "TRASHED");

        private static Dictionary<string,UsageReportCategory> allUsageReportCategorys = new Dictionary<string,UsageReportCategory>();

        static UsageReportCategory(){
            allUsageReportCategorys.Add(ACTIVE.getApiValue(), UsageReportCategory.ACTIVE);
            allUsageReportCategorys.Add(DRAFT.getApiValue(), UsageReportCategory.DRAFT);
            allUsageReportCategorys.Add(SENT.getApiValue(), UsageReportCategory.SENT);
            allUsageReportCategorys.Add(COMPLETED.getApiValue(), UsageReportCategory.COMPLETED);
            allUsageReportCategorys.Add(ARCHIVED.getApiValue(), UsageReportCategory.ARCHIVED);
            allUsageReportCategorys.Add(DECLINED.getApiValue(), UsageReportCategory.DECLINED);
            allUsageReportCategorys.Add(OPTED_OUT.getApiValue(), UsageReportCategory.OPTED_OUT);
            allUsageReportCategorys.Add(EXPIRED.getApiValue(), UsageReportCategory.EXPIRED);
            allUsageReportCategorys.Add(TRASHED.getApiValue(), UsageReportCategory.TRASHED);
        }

        
        private UsageReportCategory(string apiValue, string sdkValue):base(apiValue,sdkValue) {           
        }

        internal static UsageReportCategory valueOf (String apiValue){

            if (!String.IsNullOrEmpty(apiValue) && allUsageReportCategorys.ContainsKey(apiValue))
            {
                return allUsageReportCategorys[apiValue];
            }
            log.WarnFormat("Unknown API UsageReportCategory {0}. The upgrade is required.", apiValue);
            return new UsageReportCategory(apiValue, "UNRECOGNIZED");
        }

        public static string[] GetNames(){
            string[] names = new string[allUsageReportCategorys.Count];
            int i = 0;
            foreach(UsageReportCategory usageReportCategory in allUsageReportCategorys.Values){
                names[i] = usageReportCategory.GetName();
                i++;
            }
            return names;
        }

        public static UsageReportCategory parse(string value){

            if (null == value)
            {
                throw new ArgumentNullException("value is null");
            }

            if (value.Length == 0 || value.Trim().Length==0)
            {
                throw new ArgumentException("value is either an empty string or only contains white space");
            }
            foreach(UsageReportCategory usageReportCategory in allUsageReportCategorys.Values){
                if (String.Equals(usageReportCategory.GetName(), value))
                {
                    return usageReportCategory;
                }
            }
            throw new ArgumentException("value is a name, but not one of the named constants defined for the UsageReportCategory");
        }

    }
}

