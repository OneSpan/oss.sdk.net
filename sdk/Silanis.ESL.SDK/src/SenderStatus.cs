using System;
using System.Collections.Generic;
using System.Reflection;
using log4net;

namespace Silanis.ESL.SDK
{
    public class SenderStatus : EslEnumeration
    {
        private static ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static SenderStatus INVITED = new SenderStatus("INVITED","INVITED", 0);
        public static SenderStatus ACTIVE = new SenderStatus("ACTIVE","ACTIVE", 1);
        public static SenderStatus LOCKED = new SenderStatus("LOCKED","LOCKED", 2);
        private static Dictionary<string,SenderStatus> allSenderStatus = new Dictionary<string,SenderStatus>();

        static SenderStatus(){
            allSenderStatus.Add(INVITED.getApiValue(), SenderStatus.INVITED);
            allSenderStatus.Add(ACTIVE.getApiValue(), SenderStatus.ACTIVE);
            allSenderStatus.Add(LOCKED.getApiValue(), SenderStatus.LOCKED);
        }

        
        private SenderStatus(string apiValue, string sdkValue, int index):base(apiValue,sdkValue,index) {           
        }

        internal static SenderStatus valueOf (String apiValue){

            if (!String.IsNullOrEmpty(apiValue) && allSenderStatus.ContainsKey(apiValue))
            {
                return allSenderStatus[apiValue];
            }
            log.WarnFormat("Unknown API SenderStatus {1}. The upgrade is required.", apiValue);
            return new SenderStatus(apiValue, "UNRECOGNIZED", allSenderStatus.Values.Count);
        }

        public static string[] GetNames(){
            string[] names = new string[allSenderStatus.Count];
            int i = 0;
            foreach(SenderStatus senderStatus in allSenderStatus.Values){
                names[i] = senderStatus.GetName();
                i++;
            }
            return names;
        }

        public static explicit operator SenderStatus(Enum enumType)
        {
            return parse(enumType.ToString());
        }

        public static SenderStatus[] Values(){
            return (new List<SenderStatus>(allSenderStatus.Values)).ToArray();
        }

        public static SenderStatus parse(string value){

            if (null == value)
            {
                throw new ArgumentNullException("value is null");
            }

            if (value.Length == 0 || value.Trim().Length==0)
            {
                throw new ArgumentException("value is either an empty string or only contains white space");
            }
            foreach(SenderStatus senderStatus in allSenderStatus.Values){
                if (String.Equals(senderStatus.GetName(), value))
                {
                    return senderStatus;
                }
            }
            throw new ArgumentException("value is a name, but not one of the named constants defined for the SenderStatus");
        }

    }
}

