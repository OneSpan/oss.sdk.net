using System;
using System.Collections.Generic;
using OneSpanSign.Sdk.Internal;

namespace OneSpanSign.Sdk
{
    public class KnowledgeBasedAuthenticationStatus : OssEnumeration
    {
        private static readonly ILogger Log = LoggerFactory.get(typeof(KnowledgeBasedAuthenticationStatus));

        public static readonly KnowledgeBasedAuthenticationStatus NOT_YET_ATTEMPTED = new KnowledgeBasedAuthenticationStatus("NOT_YET_ATTEMPTED", "NOT_YET_ATTEMPTED", 0);
        public static readonly KnowledgeBasedAuthenticationStatus PASSED = new KnowledgeBasedAuthenticationStatus("PASSED", "PASSED", 1);
        public static readonly KnowledgeBasedAuthenticationStatus FAILED = new KnowledgeBasedAuthenticationStatus("FAILED", "FAILED", 2);
        public static readonly KnowledgeBasedAuthenticationStatus INVALID_SIGNER = new KnowledgeBasedAuthenticationStatus("INVALID_SIGNER", "INVALID_SIGNER", 3);
        public static readonly KnowledgeBasedAuthenticationStatus UPDATED = new KnowledgeBasedAuthenticationStatus("UPDATED", "UPDATED", 4);
        public static readonly KnowledgeBasedAuthenticationStatus LOCKED = new KnowledgeBasedAuthenticationStatus("LOCKED", "LOCKED", 5);
        private static readonly Dictionary<string,KnowledgeBasedAuthenticationStatus> AllKnowledgeBasedAuthenticationStatus = new Dictionary<string,KnowledgeBasedAuthenticationStatus>();


        static KnowledgeBasedAuthenticationStatus()
        {
            AllKnowledgeBasedAuthenticationStatus.Add(NOT_YET_ATTEMPTED.getApiValue(), NOT_YET_ATTEMPTED);
            AllKnowledgeBasedAuthenticationStatus.Add(PASSED.getApiValue(), PASSED);
            AllKnowledgeBasedAuthenticationStatus.Add(FAILED.getApiValue(), FAILED);
            AllKnowledgeBasedAuthenticationStatus.Add(INVALID_SIGNER.getApiValue(), INVALID_SIGNER);
            AllKnowledgeBasedAuthenticationStatus.Add(UPDATED.getApiValue(), UPDATED);
            AllKnowledgeBasedAuthenticationStatus.Add(LOCKED.getApiValue(), LOCKED);
        }

        private KnowledgeBasedAuthenticationStatus(string apiValue, string sdkValue, int index):base(apiValue, sdkValue,index) 
        {           
        }
       
        internal static KnowledgeBasedAuthenticationStatus valueOf (string apiValue)
        {

            if (!String.IsNullOrEmpty(apiValue) && AllKnowledgeBasedAuthenticationStatus.ContainsKey(apiValue))
            {
                return AllKnowledgeBasedAuthenticationStatus[apiValue];
            }
            Log.Warn("Unknown API KnowledgeBasedAuthenticationStatus {0}. The upgrade is required.", apiValue);
            return new KnowledgeBasedAuthenticationStatus(apiValue, "UNRECOGNIZED", AllKnowledgeBasedAuthenticationStatus.Values.Count);
        }

        public static string[] GetNames()
        {
            string[] names = new string[AllKnowledgeBasedAuthenticationStatus.Count];
            int i = 0;
            foreach(KnowledgeBasedAuthenticationStatus kbaStatus in AllKnowledgeBasedAuthenticationStatus.Values)
            {
                names[i] = kbaStatus.GetName();
                i++;
            }
            return names;
        }

        public static explicit operator KnowledgeBasedAuthenticationStatus(Enum enumType)
        {
            return parse(enumType.ToString());
        }

        public static KnowledgeBasedAuthenticationStatus[] Values()
        {
            return (new List<KnowledgeBasedAuthenticationStatus>(AllKnowledgeBasedAuthenticationStatus.Values)).ToArray();
        }

        public static KnowledgeBasedAuthenticationStatus parse(string value)
        {

            if (null == value)
            {
                throw new ArgumentNullException("value is null");
            }

            if (value.Length == 0 || value.Trim().Length==0)
            {
                throw new ArgumentException("value is either an empty string or only contains white space");
            }
            foreach(KnowledgeBasedAuthenticationStatus kbaStatus in AllKnowledgeBasedAuthenticationStatus.Values)
            {
                if (String.Equals(kbaStatus.GetName(), value))
                {
                    return kbaStatus;
                }
            }
            throw new ArgumentException("value is a name, but not one of the named constants defined for the KnowledgeBasedAuthenticationStatus");
        }
    }
}

