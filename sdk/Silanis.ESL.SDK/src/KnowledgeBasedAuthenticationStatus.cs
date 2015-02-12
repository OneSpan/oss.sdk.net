using System;
using System.Collections.Generic;
using System.Reflection;
using log4net;

namespace Silanis.ESL.SDK
{
    public class KnowledgeBasedAuthenticationStatus : EslEnumeration
    {
        private static ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static KnowledgeBasedAuthenticationStatus NOT_YET_ATTEMPTED = new KnowledgeBasedAuthenticationStatus("NOT_YET_ATTEMPTED", "NOT_YET_ATTEMPTED");
        public static KnowledgeBasedAuthenticationStatus PASSED = new KnowledgeBasedAuthenticationStatus("PASSED", "PASSED");
        public static KnowledgeBasedAuthenticationStatus FAILED = new KnowledgeBasedAuthenticationStatus("FAILED", "FAILED");
        private static Dictionary<string,KnowledgeBasedAuthenticationStatus> allKnowledgeBasedAuthenticationStatus = new Dictionary<string,KnowledgeBasedAuthenticationStatus>();


        static KnowledgeBasedAuthenticationStatus(){
            allKnowledgeBasedAuthenticationStatus.Add(NOT_YET_ATTEMPTED.getApiValue(), KnowledgeBasedAuthenticationStatus.NOT_YET_ATTEMPTED);
            allKnowledgeBasedAuthenticationStatus.Add(PASSED.getApiValue(), KnowledgeBasedAuthenticationStatus.PASSED);
            allKnowledgeBasedAuthenticationStatus.Add(FAILED.getApiValue(), KnowledgeBasedAuthenticationStatus.FAILED);
        }

        private KnowledgeBasedAuthenticationStatus(string apiValue, string sdkValue):base(apiValue, sdkValue) {           
        }
       
        internal static KnowledgeBasedAuthenticationStatus valueOf (String apiValue){

            if (!String.IsNullOrEmpty(apiValue) && allKnowledgeBasedAuthenticationStatus.ContainsKey(apiValue))
            {
                return allKnowledgeBasedAuthenticationStatus[apiValue];
            }
            log.WarnFormat("Unknown API KnowledgeBasedAuthenticationStatus {0}. The upgrade is required.", apiValue);
            return new KnowledgeBasedAuthenticationStatus(apiValue, "UNRECOGNIZED");
        }

        public static string[] GetNames(){
            string[] names = new string[allKnowledgeBasedAuthenticationStatus.Count];
            int i = 0;
            foreach(KnowledgeBasedAuthenticationStatus kbaStatus in allKnowledgeBasedAuthenticationStatus.Values){
                names[i] = kbaStatus.GetName();
                i++;
            }
            return names;
        }

        public static KnowledgeBasedAuthenticationStatus parse(string value){

            if (null == value)
            {
                throw new ArgumentNullException("value is null");
            }

            if (value.Length == 0 || value.Trim().Length==0)
            {
                throw new ArgumentException("value is either an empty string or only contains white space");
            }
            foreach(KnowledgeBasedAuthenticationStatus kbaStatus in allKnowledgeBasedAuthenticationStatus.Values){
                if (String.Equals(kbaStatus.GetName(), value))
                {
                    return kbaStatus;
                }
            }
            throw new ArgumentException("value is a name, but not one of the named constants defined for the KnowledgeBasedAuthenticationStatus");
        }

    }
}

