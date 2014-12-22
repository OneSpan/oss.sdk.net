using System;
using System.Collections.Generic;
using System.Reflection;
using log4net;

namespace Silanis.ESL.SDK
{
    public class RequirementStatus : EslEnumeration
	{
        private static ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static RequirementStatus INCOMPLETE = new RequirementStatus("INCOMPLETE", "INCOMPLETE");
        public static RequirementStatus REJECTED = new RequirementStatus("REJECTED", "REJECTED");
        public static RequirementStatus COMPLETE = new RequirementStatus("COMPLETE", "COMPLETE");
        private static Dictionary<string,RequirementStatus> allRequirementStatus = new Dictionary<string,RequirementStatus>();

        static RequirementStatus(){
            allRequirementStatus.Add(INCOMPLETE.getApiValue(), INCOMPLETE);
            allRequirementStatus.Add(REJECTED.getApiValue(), REJECTED);
            allRequirementStatus.Add(COMPLETE.getApiValue(), COMPLETE);
        }

        
        private RequirementStatus(string apiValue, string sdkValue):base(apiValue,sdkValue) {           
        }

        internal static RequirementStatus valueOf (String apiValue){

            if (!String.IsNullOrEmpty(apiValue) && allRequirementStatus.ContainsKey(apiValue))
            {
                return allRequirementStatus[apiValue];
            }
            log.WarnFormat("Unknown API RequirementStatus {0}. The upgrade is required.", apiValue);
            return new RequirementStatus(apiValue, "UNRECOGNIZED");
        }

        public static string[] GetNames(){
            string[] names = new string[allRequirementStatus.Count];
            int i = 0;
            foreach(RequirementStatus requirementStatus in allRequirementStatus.Values){
                names[i] = requirementStatus.GetName();
                i++;
            }
            return names;
        }

        public static RequirementStatus parse(string value){

            if (null == value)
            {
                throw new ArgumentNullException("value is null");
            }

            if (value.Length == 0 || value.Trim().Length==0)
            {
                throw new ArgumentException("value is either an empty string or only contains white space");
            }
            foreach(RequirementStatus requirementStatus in allRequirementStatus.Values){
                if (String.Equals(requirementStatus.GetName(), value))
                {
                    return requirementStatus;
                }
            }
            throw new ArgumentException("value is a name, but not one of the named constants defined for the RequirementStatus");
        }
	}
}

